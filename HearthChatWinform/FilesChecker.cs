using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;

namespace HearthChatWinform
{
	class FilesChecker
	{
		static string user = Environment.UserName;
		static string path = @"C:\Users\" + user + @"\AppData\Local\HearthChat";

		public async static Task<Tuple<bool, string>> IsHsRunning()
		{
			var createLog = Task.Factory.StartNew<bool>(() =>
			{
				string username = Environment.UserName;
				string logPath = @"C:\Users\" + username + @"\AppData\Local\Blizzard\Hearthstone\log.config";

				try
				{
					if (File.Exists(logPath))
					{
						File.Delete(logPath);
					}

					using (FileStream fs = File.Create(logPath))
					{
						Byte[] info = new UTF8Encoding(true).GetBytes(@"[LoadingScreen]
LogLevel = 1
FilePrinting = false
ConsolePrinting = true
ScreenPrinting = false

[Power]
LogLevel = 1
ConsolePrinting = true");

						fs.Write(info, 0, info.Length);
						fs.Close();
					}
				}
				catch (Exception ex)
				{
					return false;
				}
				return true;
			});

			var getUsername = Task.Factory.StartNew<bool>(() =>
			{
				// check if name dir exists
				if (Directory.Exists(path))
				{
					// check if name file exists
					if (File.Exists(path + @"\username.config"))
					{
						using (var stream = new FileStream(path + @"\username.config", FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
						using (var username = new StreamReader(stream))
						{
							// check if name file contains only 1 line of text
							if (username.ReadLine() != null && username.ReadLine() == null)
							{
								return true;
							}
							else
							{
								File.Delete(path + @"\username.config");
								stream.Close();
								username.Close();
								return false;
							}
						}
					}
					// if name file doesnt exist
					else
					{
						return false;
					}
				}
				// if dir doesnt exist
				else
				{
					return false;
				}
			});


			var getHsPath = Task.Factory.StartNew<string>(() =>
		   {
			   Process[] processCheck = Process.GetProcessesByName("Hearthstone");
			   if (processCheck.Length == 0)
			   {
				   var query = new WqlEventQuery(
					  "__InstanceCreationEvent",
					  new TimeSpan(0, 0, 1),
					  "TargetInstance isa \"Win32_Process\" and TargetInstance.Name = \"Hearthstone.exe\""
					);

				   using (var watcher = new ManagementEventWatcher(query))
				   {
					   ManagementBaseObject e = watcher.WaitForNextEvent();

					   var hsProcess = Process.GetProcessesByName("Hearthstone");
					   string filePath = hsProcess[0].MainModule.FileName;
					   int index = filePath.LastIndexOf(@"\");
					   string path = filePath.Remove(index + 1);
					   watcher.Stop();
					   return path;
				   }
			   }
			   else
			   {
				   string filePath = processCheck[0].MainModule.FileName;
				   int index = filePath.LastIndexOf(@"\");
				   string path = filePath.Remove(index + 1);
				   return path;
			   }
		   });

			await createLog;
			bool usernameExist = await getUsername;

			string hsPath = await getHsPath;

			Tuple<bool, string> filesState = new Tuple<bool, string>(usernameExist, hsPath);

			return filesState;
		}
	}
}
