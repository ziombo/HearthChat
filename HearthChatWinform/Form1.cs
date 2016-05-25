using System;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using System.Collections;
using System.IO;
using System.Text;

namespace HearthChatWinform
{
	public partial class Form1 : Form
	{
		public static IHubProxy chat { get; set; }
		const string ServerURI = "http://codetogether.club";
		static string HsPath = "";
		private HubConnection hubConnection { get; set; }
		private static bool isGrouped = true;
		private static string playerName;

		public Form1()
		{
			InitializeComponent();
		}

		private void btnSend_Click(object sender, EventArgs e)
		{
			if(!String.IsNullOrWhiteSpace(txtMsg.Text))
			{
				chat.Invoke("SendGroup", NameGetter.BothNames, playerName, txtMsg.Text);
			}
			txtMsg.Text = String.Empty;
			txtMsg.Focus();
		}

		private async void btnConnect_Click(object sender, EventArgs e)
		{
			await ConnectAsync();
		}

		public static void GroupConnect()
		{	
			string group = NameGetter.BothNames;
			if (!String.IsNullOrWhiteSpace(group))
			{
				chat.Invoke("JoinGroup", group);
			}
		}
		
		public static void GroupDisconnect()
		{
			string group = NameGetter.BothNames;
			if (!String.IsNullOrWhiteSpace(group))
			{
				chat.Invoke("LeaveGroup", group);
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if(hubConnection != null)
			{
				hubConnection.Stop();
				hubConnection.Dispose();
			}
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{
			this.BeginInvoke((MethodInvoker)delegate { this.Close(); });
		}

		private void btnJoin_Click(object sender, EventArgs e)
		{
			btnSend.Enabled = true;
			btnLeave.Enabled = true;
			btnJoin.Enabled = false;
			txtMsg.Enabled = true;
			txtMsg.Focus();
			isGrouped = true;
		}

		private void btnLeave_Click(object sender, EventArgs e)
		{
			btnSend.Enabled = false;
			btnLeave.Enabled = false;
			btnJoin.Enabled = true;
			txtMsg.Enabled = false;
			isGrouped = false;
		}

		private async Task<bool> ConnectAsync()
		{
			hubConnection = new HubConnection(ServerURI);
			chat = hubConnection.CreateHubProxy("ChatHub");

			chat.On<string, string>("joinedGroup", (group, connId) =>
				this.Invoke((MethodInvoker)(()=>
				{
					btnJoin.Enabled = false;
					btnSend.Enabled = true;
					btnLeave.Enabled = true;
					lblWho.Text = NameGetter.CurrentMatchPlayers;
					richMsg.Clear();
					MatchStarted();
					
				}
				))
			);

			chat.On<string, string>("leftGroup", (group, connId) =>
				this.Invoke((MethodInvoker)(() =>
				{
					MatchEnded();
				}
				))
			);

			chat.On<string, string>("broadcastMessage", (name, message) =>
				this.Invoke((MethodInvoker)(() =>
				{
					AppendMsg(name, message);
				}
				))
			);
			try
			{
				await hubConnection.Start();
			}
			catch (HttpRequestException)
			{
				richMsg.AppendText("Failed to connect.");
				//No connection: Don't enable Send button or show chat UIx
				
			}
			richMsg.AppendText("Connected to server" + Environment.NewLine);
			return true;
		}

		private void hubConnection_Closed()
		{
			richMsg.AppendText("Disconnected." + Environment.NewLine);
			btnConnect.Enabled = true;
		}

		private async void Form1_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);
			var hsState = await FilesChecker.IsHsRunning();
			string hsPath = hsState.Item2;
			hsPath += "Hearthstone_Data";
			HsPath = hsPath;

			if(hsState.Item1 == false)
			{
				panelGetName.Visible = true;
				panelRunHs.Visible = false;
				txtProvideName.Focus();
			}
			else
			{
				ReadFromNameFile();
				WaitForMatch();
				await ConnectAsync();
				NameGetter.StartedWatcher(hsPath);
			}
		}
		
		private void AppendMsg(string name, string message)
		{
			if (isGrouped)
			{
				if(name == playerName)
				{
					richMsg.AppendText(String.Format("{0}: {1}\n", name, message));
				}
				else
				{
					richMsg.AppendText(String.Format("{0}: {1}\n", "Opponent", message));
				}
			}
			richMsg.SelectionStart = richMsg.Text.Length;
			richMsg.ScrollToCaret();

		}

		private void WaitForMatch()
		{
			panelRunHs.Visible = false;
			panelWaitMatch.Visible = true;
		}
		private void MatchStarted()
		{
			panelWaitMatch.Visible = false;
			panelChat.Visible = true;
		}
		private void MatchEnded()
		{
			panelChat.Visible = false;
			panelWaitMatch.Visible = true;
		}

		private void txtMsg_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13)
			{
				if (btnSend.Enabled == true)
				{
					btnSend_Click(sender, e);
					e.Handled = true;
				}

			}
		}

		private void richMsg_Enter(object sender, EventArgs e)
		{
			//txtMsg.Focus();
		}

		private async void btnProvideName_Click(object sender, EventArgs e)
		{
			playerName = txtProvideName.Text;
			this.Text += " - " + playerName;
			CreateNameFile(playerName);
			panelGetName.Visible = false;
			panelRunHs.Visible = true;
			WaitForMatch();
			await ConnectAsync();
			NameGetter.StartedWatcher(HsPath);
		}

		private void CreateNameFile(string name)
		{
			string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\HearthChat";
			Directory.CreateDirectory(path);

			using (var stream = new FileStream(path + @"\username.config", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				Byte[] info = new UTF8Encoding(true).GetBytes(name);
				stream.Write(info, 0, info.Length);
			}
		}

		private void ReadFromNameFile()
		{
			string path = @"C:\Users\" + Environment.UserName + @"\AppData\Local\HearthChat";

			using (var stream = new FileStream(path + @"\username.config", FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
			using (var username = new StreamReader(stream))
			{
				playerName = username.ReadLine();
				this.Text += " - " + playerName;
			}
		}

		private void chckTop_CheckedChanged(object sender, EventArgs e)
		{
			if(this.TopMost == true)
			{
				this.TopMost = false;
			}
			else
			{
				this.TopMost = true;
			}
		}
	}
}
