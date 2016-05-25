using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace HearthChatWinform
{
	public class NameGetter
	{
		static FileSystemWatcher watch;
		static int lineCounter = 0;
		static string bothNames = null;
		static string currentMatchPlayers = null;
		static bool gameEnded;

		public static bool GameEnded
		{
			get { return gameEnded; }
		}
		public static string BothNames
		{
			get { return bothNames; }
		}
		public static string CurrentMatchPlayers
		{
			get { return currentMatchPlayers; }
		}

		public static void FindNames(string hsPath)
		{
			int playersRead = 0;
			int counter = lineCounter;
			string line;
			string[] names = new string[2];
			using (var fileStream = new FileStream(hsPath+@"\output_log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var file = new StreamReader(fileStream, Encoding.Default))
			{
				for (var i = 0; i < counter; i++)
				{
					line = file.ReadLine();
				}

				Regex opponent = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=[a-zA-Z0-9]+ tag=TEAM_ID value=\d");
				Regex gameFinished = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=[a-zA-Z0-9]+ tag=PLAYSTATE value=WON");

				if (bothNames == null)
				{
					while ((line = file.ReadLine()) != null && playersRead < 2)
					{
						if (opponent.IsMatch(line))
						{
							names[playersRead] = line.Substring(line.IndexOf(@"Entity") + 7, line.IndexOf(@" tag") - line.IndexOf(@"Entity") - 7);
							playersRead++;
							lineCounter = counter;
							if (names[0] != null && names[1] != null)
							{
								Array.Sort(names);
								string roomFromNames = names[0] + names[1];
								currentMatchPlayers = names[0] + " vs " + names[1];
								bothNames = roomFromNames;
								Form1.GroupConnect();
								names[0] = null;
								names[1] = null;
								playersRead = 0;
								break;
							}
						}
						counter++;
					}
				}
				if (bothNames != null)
				{
					while ((line = file.ReadLine()) != null)
					{
						if (gameFinished.IsMatch(line))
						{
							Form1.GroupDisconnect();
							bothNames = null;
							currentMatchPlayers = null;
							lineCounter = counter;
							gameEnded = true;
							break;
						}
						counter++;
					}
				}
			}
		}

		public static void StartedWatcher(string hsPath)
		{
			TimerExampleState s = new TimerExampleState();
			TimerCallback timerDelegate =(sender) => CheckStatus(sender, hsPath);
			Timer timer = new Timer(timerDelegate, s, 1000, 1000);
			s.tmr = timer;

			watch = new FileSystemWatcher();
			watch.Path = hsPath;
			watch.Filter = "output_log.txt";
			watch.NotifyFilter = NotifyFilters.Attributes |
				NotifyFilters.CreationTime |
				NotifyFilters.FileName |
				NotifyFilters.LastAccess |
				NotifyFilters.LastWrite |
				NotifyFilters.Size |
				NotifyFilters.Security;
			watch.Changed += (sender, args) => IfStarted(sender, args, hsPath);
			watch.Created += (sender, args) => IfStarted(sender, args, hsPath);
			watch.Deleted += (sender, args) => IfStarted(sender, args, hsPath);
			watch.Renamed += (sender, args) => IfStarted(sender, args, hsPath);

			watch.EnableRaisingEvents = true;
			GC.KeepAlive(watch);
		}

		public static void CheckStatus(Object state, string hsPath)
		{
			TimerExampleState s = (TimerExampleState)state;
			s.counter++;
			if(s.counter == 10)
			{
				(s.tmr).Change(2000, 2000);
			}
			using (var fileStream = new FileStream(hsPath+@"\output_log.txt", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
			using (var fileWriter = new StreamWriter(fileStream))
			{
				fileWriter.Write("1");
				fileWriter.Flush();
				fileWriter.Close();
			}
		}

		public static void IfStarted(object source, FileSystemEventArgs e, string hsPath)
		{
			string line;
			using (var fileStream = new FileStream(hsPath+@"\output_log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			using (var file = new StreamReader(fileStream, Encoding.Default))
			{
				Regex gameStarted = new Regex(@"\[LoadingScreen\] Gameplay.Start\(\)");
				while ((line = file.ReadLine()) != null)
				{
					if (gameStarted.IsMatch(line))
					{
						FindNames(hsPath);
						gameEnded = false;
						break;
					}
				}
			}
		}
	}
}

class TimerExampleState
{
	
	public int counter = 0;
	public Timer tmr;
}



