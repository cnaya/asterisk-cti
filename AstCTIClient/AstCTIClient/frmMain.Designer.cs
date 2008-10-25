// Copyright (C) 2007-2008 Bruno Salzano
// http://centralino-voip.brunosalzano.com
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
//
// As a special exception, you may use this file as part of a free software
// library without restriction.  Specifically, if other files instantiate
// templates or use macros or inline functions from this file, or you compile
// this file and link it with other files to produce an executable, this
// file does not by itself cause the resulting executable to be covered by
// the GNU General Public License.  This exception does not however
// invalidate any other reasons why the executable file might be covered by
// the GNU General Public License.
//
// This exception applies only to the code released under the name GNU
// AstCTIClient.  If you copy code from other releases into a copy of GNU
// AstCTIClient, as the General Public License permits, the exception does
// not apply to the code that you add in this way.  To avoid misleading
// anyone as to the status of such modified files, you must delete
// this exception notice from them.
//
// If you write modifications of your own for AstCTIClient, it is your choice
// whether to permit this exception to apply to your modifications.
// If you do not wish that, delete this exception notice.
//
namespace AstCTIClient
{
    partial class frmMain
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            this.notifyIcon1.Visible = false;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuShowHide = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblSecret = new System.Windows.Forms.Label();
            this.pnlExtension = new System.Windows.Forms.Panel();
            this.lblLineState = new System.Windows.Forms.Label();
            this.cboOutboundContextes = new System.Windows.Forms.ComboBox();
            this.btnDial = new System.Windows.Forms.Button();
            this.txtPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.pnlExtension.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipTitle = "Asterisk CTI";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Asterisk CTI Client";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowHide});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            // 
            // mnuShowHide
            // 
            this.mnuShowHide.Name = "mnuShowHide";
            this.mnuShowHide.Size = new System.Drawing.Size(103, 22);
            this.mnuShowHide.Text = "Show";
            this.mnuShowHide.Click += new System.EventHandler(this.mnuShowHide_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.BackColor = System.Drawing.Color.Transparent;
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Image = global::AstCTIClient.Properties.Resources.configure_shortcuts;
            this.btnConfig.Location = new System.Drawing.Point(176, 12);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(24, 24);
            this.btnConfig.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnConfig, "Configuration");
            this.btnConfig.UseVisualStyleBackColor = false;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.Transparent;
            this.btnHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHide.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.Image = global::AstCTIClient.Properties.Resources._14_layer_lowerlayer;
            this.btnHide.Location = new System.Drawing.Point(206, 12);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(24, 24);
            this.btnHide.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnHide, "Minimize to tray");
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(133, 230);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(97, 23);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start";
            this.toolTip1.SetToolTip(this.btnStartStop, "Starts/Stops CTI Client");
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(14, 230);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(97, 23);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "Quit";
            this.toolTip1.SetToolTip(this.btnQuit, "Quit Application");
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
            this.linkLabel1.LinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel1.Location = new System.Drawing.Point(53, 261);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(165, 13);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "centralino-voip.brunosalzano.com";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.MidnightBlue;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Location = new System.Drawing.Point(18, 121);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(57, 13);
            this.lblUserName.TabIndex = 5;
            this.lblUserName.Text = "UserName";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(96, 118);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(122, 20);
            this.txtUsername.TabIndex = 6;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(96, 144);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '°';
            this.txtPassword.Size = new System.Drawing.Size(122, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // lblSecret
            // 
            this.lblSecret.AutoSize = true;
            this.lblSecret.BackColor = System.Drawing.Color.Transparent;
            this.lblSecret.Location = new System.Drawing.Point(18, 147);
            this.lblSecret.Name = "lblSecret";
            this.lblSecret.Size = new System.Drawing.Size(38, 13);
            this.lblSecret.TabIndex = 7;
            this.lblSecret.Text = "Secret";
            // 
            // pnlExtension
            // 
            this.pnlExtension.BackColor = System.Drawing.Color.Transparent;
            this.pnlExtension.Controls.Add(this.lblLineState);
            this.pnlExtension.Controls.Add(this.cboOutboundContextes);
            this.pnlExtension.Controls.Add(this.btnDial);
            this.pnlExtension.Controls.Add(this.txtPhoneNumber);
            this.pnlExtension.Controls.Add(this.lblExtension);
            this.pnlExtension.Location = new System.Drawing.Point(7, 93);
            this.pnlExtension.Name = "pnlExtension";
            this.pnlExtension.Size = new System.Drawing.Size(229, 102);
            this.pnlExtension.TabIndex = 9;
            this.pnlExtension.Visible = false;
            // 
            // lblLineState
            // 
            this.lblLineState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineState.Location = new System.Drawing.Point(5, 79);
            this.lblLineState.Name = "lblLineState";
            this.lblLineState.Size = new System.Drawing.Size(218, 23);
            this.lblLineState.TabIndex = 4;
            this.lblLineState.Text = "Line Status";
            this.lblLineState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboOutboundContextes
            // 
            this.cboOutboundContextes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOutboundContextes.FormattingEnabled = true;
            this.cboOutboundContextes.Location = new System.Drawing.Point(8, 55);
            this.cboOutboundContextes.Name = "cboOutboundContextes";
            this.cboOutboundContextes.Size = new System.Drawing.Size(174, 21);
            this.cboOutboundContextes.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cboOutboundContextes, "Dialing Context");
            // 
            // btnDial
            // 
            this.btnDial.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDial.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnDial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDial.Image = global::AstCTIClient.Properties.Resources.agt_action_success;
            this.btnDial.Location = new System.Drawing.Point(188, 30);
            this.btnDial.Name = "btnDial";
            this.btnDial.Size = new System.Drawing.Size(35, 46);
            this.btnDial.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnDial, "Dial number");
            this.btnDial.UseVisualStyleBackColor = true;
            this.btnDial.Click += new System.EventHandler(this.btnDial_Click);
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.Location = new System.Drawing.Point(8, 30);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.Size = new System.Drawing.Size(174, 20);
            this.txtPhoneNumber.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtPhoneNumber, "Dial number");
            // 
            // lblExtension
            // 
            this.lblExtension.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblExtension.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtension.ForeColor = System.Drawing.Color.DimGray;
            this.lblExtension.Location = new System.Drawing.Point(7, 7);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(216, 20);
            this.lblExtension.TabIndex = 0;
            this.lblExtension.Text = "Exten";
            this.lblExtension.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AstCTIClient.Properties.Resources.mainform;
            this.ClientSize = new System.Drawing.Size(242, 283);
            this.Controls.Add(this.pnlExtension);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblSecret);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnStartStop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asterisk CTI";
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlExtension.ResumeLayout(false);
            this.pnlExtension.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuShowHide;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblSecret;
        private System.Windows.Forms.Panel pnlExtension;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.ComboBox cboOutboundContextes;
        private System.Windows.Forms.Button btnDial;
        private System.Windows.Forms.TextBox txtPhoneNumber;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblLineState;
    }
}

