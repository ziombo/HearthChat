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
using System.Diagnostics;

namespace HearthChatWinform
{
	public partial class Form1 : Form
	{
		public static IHubProxy chat { get; set; }
		private HubConnection hubConnection { get; set; }

		const string ServerURI = "http://localhost:3333/";
		static string HsPath = "";
		private static string playerName;

		private static bool isGrouped = true; //determines if player wants to recieve msg (join/leave btns)

		
		public Form1()
		{
			InitializeComponent();
		}

		#region Form Triggerred Events

		private async void Form1_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

			await ConnectAsync();


			var hsState = await FilesChecker.IsHsRunning();
			string hsPath = hsState.Item3;
			hsPath += "Hearthstone_Data";
			HsPath = hsPath;

			// no name file or is invalid
			if (hsState.Item1 == false)
			{
				panelGetName.Visible = true;
				panelRunHs.Visible = false;
				txtProvideName.Focus();
			}
			// name file exists and is valid
			else
			{
				ReadFromNameFile();
				WaitForMatch();
				NameGetter.CheckLogFile(hsPath);
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

		
		/* panelChat */
		private void btnSend_Click(object sender, EventArgs e)
		{
			if(!String.IsNullOrWhiteSpace(txtMsg.Text))
			{
				chat.Invoke("SendGroup", NameGetter.BothNames, playerName, txtMsg.Text);
			}
			txtMsg.Text = String.Empty;
			txtMsg.Focus();
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

		/* panelDc */
		private async void btnConnect_Click(object sender, EventArgs e)
		{
			await ConnectAsync();
		}

		/* panelGetName */
		private void btnProvideName_Click(object sender, EventArgs e)
		{
			playerName = txtProvideName.Text;
			this.Text += " - " + playerName;
			CreateNameFile(playerName);
			panelGetName.Visible = false;
			WaitForMatch();
			NameGetter.CheckLogFile(HsPath);
		}

		#endregion

		#region Chat Connection Related

		private async Task<bool> ConnectAsync()
		{
			var connectToChatHub = Task.Run<bool>( async () =>
			{
				hubConnection = new HubConnection(ServerURI);
				chat = hubConnection.CreateHubProxy("ChatHub");

				chat.On<string, string>("joinedGroup", (group, connId) =>
					this.Invoke((MethodInvoker)(() =>
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
					//TODO: show dc panel and give "couldnt connect" msg


				}
				return true;
			});
			bool isConnectedToChatHub = await connectToChatHub;
			return isConnectedToChatHub;

			#region Old way to connect
			//hubConnection = new HubConnection(ServerURI);
			//chat = hubConnection.CreateHubProxy("ChatHub");

			//chat.On<string, string>("joinedGroup", (group, connId) =>
			//	this.Invoke((MethodInvoker)(()=>
			//	{
			//		btnJoin.Enabled = false;
			//		btnSend.Enabled = true;
			//		btnLeave.Enabled = true;
			//		lblWho.Text = NameGetter.CurrentMatchPlayers;
			//		richMsg.Clear();
			//		MatchStarted();

			//	}
			//	))
			//);

			//chat.On<string, string>("leftGroup", (group, connId) =>
			//	this.Invoke((MethodInvoker)(() =>
			//	{
			//		MatchEnded();
			//	}
			//	))
			//);

			//chat.On<string, string>("broadcastMessage", (name, message) =>
			//	this.Invoke((MethodInvoker)(() =>
			//	{
			//		AppendMsg(name, message);
			//	}
			//	))
			//);
			//try
			//{
			//	await hubConnection.Start();
			//}
			//catch (HttpRequestException)
			//{
			//	//TODO: show dc panel and give "couldnt connect" msg
			//	//No connection: Don't enable Send button or show chat UIx

			//}
			//return true;
			#endregion

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

		private void hubConnection_Closed()
		{
			//todo show panel dc
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

		#endregion

		#region Game State Related

		private void WaitForMatch()
		{
			panelRunHs.Visible = false;
			panelWaitMatch.Visible = true;
		}

		private void MatchStarted()
		{
			// show initial state of chat window (in case person left the chatroom last time)
			if(isGrouped == false)
			{
				btnSend.Enabled = true;
				btnLeave.Enabled = true;
				btnJoin.Enabled = false;
				txtMsg.Enabled = true;
				txtMsg.Focus();
				isGrouped = true;
			}

			panelWaitMatch.Visible = false;
			panelChat.Visible = true;
		}

		private void MatchEnded()
		{
			panelChat.Visible = false;
			panelWaitMatch.Visible = true;
		}

		#endregion

		#region Name File related

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

		#endregion

	}
}
