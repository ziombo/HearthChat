namespace HearthChatWinform
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnSend = new System.Windows.Forms.Button();
			this.txtMsg = new System.Windows.Forms.TextBox();
			this.richMsg = new System.Windows.Forms.RichTextBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.lblWho = new System.Windows.Forms.Label();
			this.btnJoin = new System.Windows.Forms.Button();
			this.btnLeave = new System.Windows.Forms.Button();
			this.lblHsRun = new System.Windows.Forms.Label();
			this.picLoad = new System.Windows.Forms.PictureBox();
			this.panelRunHs = new System.Windows.Forms.Panel();
			this.panelDc = new System.Windows.Forms.Panel();
			this.lblDc = new System.Windows.Forms.Label();
			this.panelChat = new System.Windows.Forms.Panel();
			this.panelWaitMatch = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panelGetName = new System.Windows.Forms.Panel();
			this.btnProvideName = new System.Windows.Forms.Button();
			this.txtProvideName = new System.Windows.Forms.TextBox();
			this.lblProvideName = new System.Windows.Forms.Label();
			this.chckTop = new System.Windows.Forms.CheckBox();
			this.transparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.picLoad)).BeginInit();
			this.panelRunHs.SuspendLayout();
			this.panelDc.SuspendLayout();
			this.panelChat.SuspendLayout();
			this.panelWaitMatch.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panelGetName.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSend
			// 
			this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSend.Enabled = false;
			this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.btnSend.Location = new System.Drawing.Point(276, 365);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(100, 59);
			this.btnSend.TabIndex = 0;
			this.btnSend.TabStop = false;
			this.btnSend.Text = "Send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// txtMsg
			// 
			this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
			this.txtMsg.Location = new System.Drawing.Point(8, 365);
			this.txtMsg.Multiline = true;
			this.txtMsg.Name = "txtMsg";
			this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtMsg.Size = new System.Drawing.Size(267, 59);
			this.txtMsg.TabIndex = 1;
			this.txtMsg.TabStop = false;
			this.txtMsg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMsg_KeyPress);
			// 
			// richMsg
			// 
			this.richMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richMsg.BackColor = System.Drawing.SystemColors.Window;
			this.richMsg.Cursor = System.Windows.Forms.Cursors.Default;
			this.richMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.richMsg.Location = new System.Drawing.Point(8, 74);
			this.richMsg.Name = "richMsg";
			this.richMsg.ReadOnly = true;
			this.richMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.richMsg.Size = new System.Drawing.Size(368, 285);
			this.richMsg.TabIndex = 2;
			this.richMsg.TabStop = false;
			this.richMsg.Text = "";
			this.richMsg.Enter += new System.EventHandler(this.richMsg_Enter);
			// 
			// btnConnect
			// 
			this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConnect.Enabled = false;
			this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
			this.btnConnect.Location = new System.Drawing.Point(28, 219);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(344, 113);
			this.btnConnect.TabIndex = 4;
			this.btnConnect.Text = "Reconnect";
			this.btnConnect.UseVisualStyleBackColor = true;
			this.btnConnect.Visible = false;
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// lblWho
			// 
			this.lblWho.AutoSize = true;
			this.lblWho.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.lblWho.Location = new System.Drawing.Point(3, 10);
			this.lblWho.Name = "lblWho";
			this.lblWho.Size = new System.Drawing.Size(95, 25);
			this.lblWho.TabIndex = 0;
			this.lblWho.Text = "No Game";
			// 
			// btnJoin
			// 
			this.btnJoin.Enabled = false;
			this.btnJoin.Location = new System.Drawing.Point(8, 38);
			this.btnJoin.Name = "btnJoin";
			this.btnJoin.Size = new System.Drawing.Size(182, 30);
			this.btnJoin.TabIndex = 0;
			this.btnJoin.TabStop = false;
			this.btnJoin.Text = "Join";
			this.btnJoin.UseVisualStyleBackColor = true;
			this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
			// 
			// btnLeave
			// 
			this.btnLeave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLeave.Enabled = false;
			this.btnLeave.Location = new System.Drawing.Point(194, 38);
			this.btnLeave.Name = "btnLeave";
			this.btnLeave.Size = new System.Drawing.Size(182, 30);
			this.btnLeave.TabIndex = 0;
			this.btnLeave.TabStop = false;
			this.btnLeave.Text = "Leave";
			this.btnLeave.UseVisualStyleBackColor = true;
			this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
			// 
			// lblHsRun
			// 
			this.lblHsRun.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblHsRun.AutoSize = true;
			this.lblHsRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
			this.lblHsRun.Location = new System.Drawing.Point(3, 100);
			this.lblHsRun.Name = "lblHsRun";
			this.lblHsRun.Size = new System.Drawing.Size(380, 39);
			this.lblHsRun.TabIndex = 9;
			this.lblHsRun.Text = "Please run Hearthstone";
			// 
			// picLoad
			// 
			this.picLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.picLoad.Image = global::HearthChatWinform.Properties.Resources.Loader;
			this.picLoad.Location = new System.Drawing.Point(160, 180);
			this.picLoad.Name = "picLoad";
			this.picLoad.Size = new System.Drawing.Size(64, 68);
			this.picLoad.TabIndex = 10;
			this.picLoad.TabStop = false;
			// 
			// panelRunHs
			// 
			this.panelRunHs.Controls.Add(this.lblHsRun);
			this.panelRunHs.Controls.Add(this.picLoad);
			this.panelRunHs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelRunHs.Location = new System.Drawing.Point(0, 0);
			this.panelRunHs.Name = "panelRunHs";
			this.panelRunHs.Size = new System.Drawing.Size(384, 436);
			this.panelRunHs.TabIndex = 11;
			// 
			// panelDc
			// 
			this.panelDc.Controls.Add(this.lblDc);
			this.panelDc.Controls.Add(this.btnConnect);
			this.panelDc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDc.Location = new System.Drawing.Point(0, 0);
			this.panelDc.Name = "panelDc";
			this.panelDc.Size = new System.Drawing.Size(384, 436);
			this.panelDc.TabIndex = 12;
			this.panelDc.Visible = false;
			// 
			// lblDc
			// 
			this.lblDc.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblDc.AutoSize = true;
			this.lblDc.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F);
			this.lblDc.Location = new System.Drawing.Point(17, 106);
			this.lblDc.Name = "lblDc";
			this.lblDc.Size = new System.Drawing.Size(366, 35);
			this.lblDc.TabIndex = 5;
			this.lblDc.Text = "Disconnected from server";
			// 
			// panelChat
			// 
			this.panelChat.Controls.Add(this.richMsg);
			this.panelChat.Controls.Add(this.btnSend);
			this.panelChat.Controls.Add(this.txtMsg);
			this.panelChat.Controls.Add(this.lblWho);
			this.panelChat.Controls.Add(this.btnLeave);
			this.panelChat.Controls.Add(this.btnJoin);
			this.panelChat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelChat.Location = new System.Drawing.Point(0, 0);
			this.panelChat.Name = "panelChat";
			this.panelChat.Size = new System.Drawing.Size(384, 436);
			this.panelChat.TabIndex = 13;
			this.panelChat.Visible = false;
			// 
			// panelWaitMatch
			// 
			this.panelWaitMatch.Controls.Add(this.label1);
			this.panelWaitMatch.Controls.Add(this.pictureBox1);
			this.panelWaitMatch.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelWaitMatch.Location = new System.Drawing.Point(0, 0);
			this.panelWaitMatch.Name = "panelWaitMatch";
			this.panelWaitMatch.Size = new System.Drawing.Size(384, 436);
			this.panelWaitMatch.TabIndex = 11;
			this.panelWaitMatch.Visible = false;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F);
			this.label1.Location = new System.Drawing.Point(12, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(360, 39);
			this.label1.TabIndex = 0;
			this.label1.Text = "Waiting for new match";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pictureBox1.Image = global::HearthChatWinform.Properties.Resources.hourglass;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(135, 170);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(114, 118);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// panelGetName
			// 
			this.panelGetName.Controls.Add(this.btnProvideName);
			this.panelGetName.Controls.Add(this.txtProvideName);
			this.panelGetName.Controls.Add(this.lblProvideName);
			this.panelGetName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelGetName.Location = new System.Drawing.Point(0, 0);
			this.panelGetName.Name = "panelGetName";
			this.panelGetName.Size = new System.Drawing.Size(384, 436);
			this.panelGetName.TabIndex = 11;
			this.panelGetName.Visible = false;
			// 
			// btnProvideName
			// 
			this.btnProvideName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProvideName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
			this.btnProvideName.Location = new System.Drawing.Point(28, 257);
			this.btnProvideName.Name = "btnProvideName";
			this.btnProvideName.Size = new System.Drawing.Size(321, 50);
			this.btnProvideName.TabIndex = 2;
			this.btnProvideName.Text = "Confirm";
			this.btnProvideName.UseVisualStyleBackColor = true;
			this.btnProvideName.Click += new System.EventHandler(this.btnProvideName_Click);
			// 
			// txtProvideName
			// 
			this.txtProvideName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtProvideName.Font = new System.Drawing.Font("Microsoft Sans Serif", 23F);
			this.txtProvideName.Location = new System.Drawing.Point(28, 175);
			this.txtProvideName.Name = "txtProvideName";
			this.txtProvideName.Size = new System.Drawing.Size(321, 42);
			this.txtProvideName.TabIndex = 1;
			// 
			// lblProvideName
			// 
			this.lblProvideName.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblProvideName.AutoSize = true;
			this.lblProvideName.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
			this.lblProvideName.Location = new System.Drawing.Point(4, 90);
			this.lblProvideName.Name = "lblProvideName";
			this.lblProvideName.Size = new System.Drawing.Size(377, 39);
			this.lblProvideName.TabIndex = 0;
			this.lblProvideName.Text = "Please write Your name";
			// 
			// chckTop
			// 
			this.chckTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chckTop.AutoSize = true;
			this.chckTop.Checked = true;
			this.chckTop.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chckTop.Location = new System.Drawing.Point(310, 15);
			this.chckTop.Name = "chckTop";
			this.chckTop.Size = new System.Drawing.Size(62, 17);
			this.chckTop.TabIndex = 3;
			this.chckTop.TabStop = false;
			this.chckTop.Text = "On Top";
			this.chckTop.UseVisualStyleBackColor = true;
			this.chckTop.CheckedChanged += new System.EventHandler(this.chckTop_CheckedChanged);
			// 
			// transparencyToolStripMenuItem
			// 
			this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
			this.transparencyToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
			this.transparencyToolStripMenuItem.Text = "Transparency";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 436);
			this.Controls.Add(this.chckTop);
			this.Controls.Add(this.panelChat);
			this.Controls.Add(this.panelGetName);
			this.Controls.Add(this.panelWaitMatch);
			this.Controls.Add(this.panelRunHs);
			this.Controls.Add(this.panelDc);
			this.Icon = global::HearthChatWinform.Properties.Resources.BigVer2;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Opacity = 0.95D;
			this.Text = "HearthChat";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			((System.ComponentModel.ISupportInitialize)(this.picLoad)).EndInit();
			this.panelRunHs.ResumeLayout(false);
			this.panelRunHs.PerformLayout();
			this.panelDc.ResumeLayout(false);
			this.panelDc.PerformLayout();
			this.panelChat.ResumeLayout(false);
			this.panelChat.PerformLayout();
			this.panelWaitMatch.ResumeLayout(false);
			this.panelWaitMatch.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panelGetName.ResumeLayout(false);
			this.panelGetName.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.TextBox txtMsg;
		private System.Windows.Forms.RichTextBox richMsg;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Label lblWho;
		private System.Windows.Forms.Button btnJoin;
		private System.Windows.Forms.Button btnLeave;
		private System.Windows.Forms.Label lblHsRun;
		private System.Windows.Forms.PictureBox picLoad;
		private System.Windows.Forms.Panel panelRunHs;
		private System.Windows.Forms.Panel panelDc;
		private System.Windows.Forms.Label lblDc;
		private System.Windows.Forms.Panel panelChat;
		private System.Windows.Forms.Panel panelWaitMatch;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panelGetName;
		private System.Windows.Forms.Button btnProvideName;
		private System.Windows.Forms.TextBox txtProvideName;
		private System.Windows.Forms.Label lblProvideName;
		private System.Windows.Forms.CheckBox chckTop;
		private System.Windows.Forms.ToolStripMenuItem transparencyToolStripMenuItem;
	}
}

