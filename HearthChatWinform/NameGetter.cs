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
		static FileSystemWatcher fileWatcher;		// watches HS log file
		static int GlobalLineCounter = 0;			// used to omit already read lines
		static string bothNames = null;				// used to create SignalR group
		static string currentMatchPlayers = null;	// used to display in lblWho
		static bool gameEnded = true;               // used to determine if there's ongoing match
		static string[] names = new string[2];

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


		/// <summary>
		///	Searches for players name
		/// </summary>
		public static void FindNames(string hsPath)
		{
			int playersRead = 0;
			int localLineCounter = GlobalLineCounter;
			string currentLine;
			using (var fileStream = new FileStream(hsPath+@"\output_log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var file = new StreamReader(fileStream, Encoding.Default))
			{
				for (int lineToOmit = 0; lineToOmit < localLineCounter; lineToOmit++)
				{
					currentLine = file.ReadLine();
				}
				//.{0,20} still testing
				//[a-zA-Z0-9]+ doesn't work with cyrylic etc.
				Regex player = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=.{0,20} tag=TEAM_ID value=\d");
				Regex gameFinished = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=.{0,20} tag=PLAYSTATE value=WON");

				if (bothNames == null)
				{
					while ((currentLine = file.ReadLine()) != null && playersRead < 2)
					{
						if (player.IsMatch(currentLine))
						{
							// extract player name from currentLine
							names[playersRead] = currentLine.Substring(currentLine.IndexOf(@"Entity") + 7, currentLine.IndexOf(@" tag") - currentLine.IndexOf(@"Entity") - 7);
							playersRead++;
							GlobalLineCounter = localLineCounter;

							if (names[0] != null && names[1] != null)
							{
								// encoding names so that cyrylic and other weirdos show as ? rather than even more weird symbols (than cyrylic itself)
								byte[] bytes = Encoding.Default.GetBytes(names[0]);
								names[0] = Encoding.UTF8.GetString(bytes);
								byte[] bytes2 = Encoding.Default.GetBytes(names[1]);
								names[1] = Encoding.UTF8.GetString(bytes2);

								gameEnded = false;
								Array.Sort(names);
								currentMatchPlayers = names[0] + " vs " + names[1];
								bothNames = names[0] + names[1];
								Form1.GroupConnect();
								names[0] = null;
								names[1] = null;
								playersRead = 0;
								break;
							}
						}
						localLineCounter++;
					}
				}
				if (bothNames != null)
				{
					while ((currentLine = file.ReadLine()) != null)
					{
						if (gameFinished.IsMatch(currentLine))
						{
							Form1.GroupDisconnect();
							bothNames = null;
							currentMatchPlayers = null;
							GlobalLineCounter = localLineCounter;
							gameEnded = true;
							break;
						}
						localLineCounter++;
					}
				}
			}
		}

		/// <summary>
		/// Every 1second(for the first 10, every 2 later) checks if log file changes
		/// </summary>
		public static void StartedWatcher(string hsPath)
		{
			TimerExampleState s = new TimerExampleState();
			TimerCallback timerDelegate = (sender) => CheckStatus(sender, hsPath);
			Timer timer = new Timer(timerDelegate, s, 1000, 1000);
			s.tmr = timer;

			fileWatcher = new FileSystemWatcher();
			fileWatcher.Path = hsPath;
			fileWatcher.Filter = "output_log.txt";
			fileWatcher.NotifyFilter = NotifyFilters.Attributes |
				NotifyFilters.CreationTime |
				NotifyFilters.FileName |
				NotifyFilters.LastAccess |
				NotifyFilters.LastWrite |
				NotifyFilters.Size |
				NotifyFilters.Security;
			fileWatcher.Changed += (sender, args) => FindNames(hsPath);
			fileWatcher.Created += (sender, args) => FindNames(hsPath);
			fileWatcher.Deleted += (sender, args) => FindNames(hsPath);
			fileWatcher.Renamed += (sender, args) => FindNames(hsPath);

			fileWatcher.EnableRaisingEvents = true;
			GC.KeepAlive(fileWatcher);
		}

		/// <summary>
		/// Every 1second(for the first 10, every 2 later) writes into log file to make watcher aknowledge changes
		/// </summary>
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


		/// <summary>
		/// Read through HS log to ignore past matches if there are any
		/// </summary>
		public static void CheckLogFile(string hsPath)
		{
			using (var fileStream = new FileStream(hsPath + @"\output_log.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var file = new StreamReader(fileStream, Encoding.Default))
			{
				Regex opponent = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=.{0,20} tag=TEAM_ID value=\d");
				Regex gameFinished = new Regex(@"\[Power\]\sGameState.DebugPrintPower\(\) - TAG_CHANGE Entity=.{0,20} tag=PLAYSTATE value=WON");
				string line;
				int playersRead = 0;

				while ((line = file.ReadLine()) != null)
				{
					GlobalLineCounter++;

					if (opponent.IsMatch(line))
					{
						names[playersRead] = line.Substring(line.IndexOf(@"Entity") + 7, line.IndexOf(@" tag") - line.IndexOf(@"Entity") - 7);
						playersRead++;
						if(names[0] != null && names[1] != null)
						{
							// encoding names so that cyrylic and other weirdos show as ? rather than even more weird symbols (than cyrylic itself)
							byte[] bytes = Encoding.Default.GetBytes(names[0]);
							names[0] = Encoding.UTF8.GetString(bytes);
							byte[] bytes2 = Encoding.Default.GetBytes(names[1]);
							names[1] = Encoding.UTF8.GetString(bytes2);

							gameEnded = false;				
						}
					}
					if (gameFinished.IsMatch(line))
					{
						playersRead = 0;
						gameEnded = true;
						names[0] = null;
						names[1] = null;
					}
				}

				if(gameEnded == false)
				{
					// safety check
					if (names[0] != null && names[1] != null)
					{		
						gameEnded = false;
						Array.Sort(names);
						currentMatchPlayers = names[0] + " vs " + names[1];
						bothNames = names[0] + names[1];
						Form1.GroupConnect();
						names[0] = null;
						names[1] = null;
						playersRead = 0;
					}
				}
				StartedWatcher(hsPath);
			}
		}
	}
}

class TimerExampleState
{
	
	public int counter = 0;
	public Timer tmr;
}



