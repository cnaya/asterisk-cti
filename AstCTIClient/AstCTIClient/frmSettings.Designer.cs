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
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboCalleridShow = new System.Windows.Forms.CheckBox();
            this.txtCalleridFadeoutSpeed = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCalleridTimeout = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtClientPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClientUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClientExten = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCtiServerTimeout = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCtiServerPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCtiServerHostname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtMysqlPort = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMysqlPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMysqlUser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMysqlHost = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.propInbound = new System.Windows.Forms.PropertyGrid();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.chkStartWithWindows = new System.Windows.Forms.CheckBox();
            this.chkMinimizeOnStart = new System.Windows.Forms.CheckBox();
            this.btnUIFont = new System.Windows.Forms.Button();
            this.txtUIFont = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbLanguage = new System.Windows.Forms.ImageCombo();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkIBCloseChilds = new System.Windows.Forms.CheckBox();
            this.chkIBShowGo = new System.Windows.Forms.CheckBox();
            this.chkIBAllowUrls = new System.Windows.Forms.CheckBox();
            this.chkIBAddrBar = new System.Windows.Forms.CheckBox();
            this.chkIBStatusBar = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(491, 346);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(483, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Tag = "8000";
            this.tabPage1.Text = "Cti Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cboCalleridShow);
            this.groupBox2.Controls.Add(this.txtCalleridFadeoutSpeed);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtCalleridTimeout);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtClientPassword);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtClientUsername);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtClientExten);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 190);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "8006";
            this.groupBox2.Text = "Informazioni Client";
            // 
            // cboCalleridShow
            // 
            this.cboCalleridShow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboCalleridShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCalleridShow.Location = new System.Drawing.Point(5, 111);
            this.cboCalleridShow.Name = "cboCalleridShow";
            this.cboCalleridShow.Size = new System.Drawing.Size(219, 18);
            this.cboCalleridShow.TabIndex = 7;
            this.cboCalleridShow.Tag = "8019";
            this.cboCalleridShow.Text = "Visualizza Popup Callerid";
            this.cboCalleridShow.UseVisualStyleBackColor = true;
            // 
            // txtCalleridFadeoutSpeed
            // 
            this.txtCalleridFadeoutSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalleridFadeoutSpeed.Location = new System.Drawing.Point(210, 160);
            this.txtCalleridFadeoutSpeed.Name = "txtCalleridFadeoutSpeed";
            this.txtCalleridFadeoutSpeed.Size = new System.Drawing.Size(96, 20);
            this.txtCalleridFadeoutSpeed.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(5, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 13);
            this.label12.TabIndex = 12;
            this.label12.Tag = "8021";
            this.label12.Text = "Velocità Fadeout";
            // 
            // txtCalleridTimeout
            // 
            this.txtCalleridTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalleridTimeout.Location = new System.Drawing.Point(210, 134);
            this.txtCalleridTimeout.Name = "txtCalleridTimeout";
            this.txtCalleridTimeout.Size = new System.Drawing.Size(96, 20);
            this.txtCalleridTimeout.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(5, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 10;
            this.label11.Tag = "8020";
            this.label11.Text = "Timeout (sec.)";
            // 
            // txtClientPassword
            // 
            this.txtClientPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientPassword.Location = new System.Drawing.Point(210, 82);
            this.txtClientPassword.Name = "txtClientPassword";
            this.txtClientPassword.PasswordChar = '*';
            this.txtClientPassword.Size = new System.Drawing.Size(198, 20);
            this.txtClientPassword.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(5, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 6;
            this.label6.Tag = "8018";
            this.label6.Text = "Password";
            // 
            // txtClientUsername
            // 
            this.txtClientUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientUsername.Location = new System.Drawing.Point(210, 56);
            this.txtClientUsername.Name = "txtClientUsername";
            this.txtClientUsername.Size = new System.Drawing.Size(198, 20);
            this.txtClientUsername.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 13);
            this.label5.TabIndex = 4;
            this.label5.Tag = "8017";
            this.label5.Text = "Nome utente";
            // 
            // txtClientExten
            // 
            this.txtClientExten.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientExten.Location = new System.Drawing.Point(210, 30);
            this.txtClientExten.Name = "txtClientExten";
            this.txtClientExten.Size = new System.Drawing.Size(198, 20);
            this.txtClientExten.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 2;
            this.label4.Tag = "8016";
            this.label4.Text = "Interno o agente asterisk";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtCtiServerTimeout);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCtiServerPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCtiServerHostname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(471, 112);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "8005";
            this.groupBox1.Text = "Connessione al CTI Server";
            // 
            // txtCtiServerTimeout
            // 
            this.txtCtiServerTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtiServerTimeout.Location = new System.Drawing.Point(211, 76);
            this.txtCtiServerTimeout.Name = "txtCtiServerTimeout";
            this.txtCtiServerTimeout.Size = new System.Drawing.Size(96, 20);
            this.txtCtiServerTimeout.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Tag = "8015";
            this.label3.Text = "Socket Timeout (ms)";
            // 
            // txtCtiServerPort
            // 
            this.txtCtiServerPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtiServerPort.Location = new System.Drawing.Point(211, 50);
            this.txtCtiServerPort.Name = "txtCtiServerPort";
            this.txtCtiServerPort.Size = new System.Drawing.Size(96, 20);
            this.txtCtiServerPort.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 2;
            this.label2.Tag = "8014";
            this.label2.Text = "Porta di connessione";
            // 
            // txtCtiServerHostname
            // 
            this.txtCtiServerHostname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCtiServerHostname.Location = new System.Drawing.Point(211, 24);
            this.txtCtiServerHostname.Name = "txtCtiServerHostname";
            this.txtCtiServerHostname.Size = new System.Drawing.Size(198, 20);
            this.txtCtiServerHostname.TabIndex = 1;
            this.txtCtiServerHostname.Tag = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Tag = "8013";
            this.label1.Text = "Hostname o IP";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(483, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "8001";
            this.tabPage2.Text = "Database";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtMysqlPort);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtMysqlPassword);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.txtMysqlUser);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtMysqlHost);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(469, 139);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "8007";
            this.groupBox3.Text = "Connessione al Database";
            // 
            // txtMysqlPort
            // 
            this.txtMysqlPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMysqlPort.Location = new System.Drawing.Point(211, 102);
            this.txtMysqlPort.Name = "txtMysqlPort";
            this.txtMysqlPort.Size = new System.Drawing.Size(96, 20);
            this.txtMysqlPort.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 6;
            this.label13.Tag = "8025";
            this.label13.Text = "Porta";
            // 
            // txtMysqlPassword
            // 
            this.txtMysqlPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMysqlPassword.Location = new System.Drawing.Point(211, 76);
            this.txtMysqlPassword.Name = "txtMysqlPassword";
            this.txtMysqlPassword.PasswordChar = '*';
            this.txtMysqlPassword.Size = new System.Drawing.Size(198, 20);
            this.txtMysqlPassword.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 4;
            this.label7.Tag = "8024";
            this.label7.Text = "Password";
            // 
            // txtMysqlUser
            // 
            this.txtMysqlUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMysqlUser.Location = new System.Drawing.Point(211, 50);
            this.txtMysqlUser.Name = "txtMysqlUser";
            this.txtMysqlUser.Size = new System.Drawing.Size(198, 20);
            this.txtMysqlUser.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 2;
            this.label8.Tag = "8023";
            this.label8.Text = "Nome utente";
            // 
            // txtMysqlHost
            // 
            this.txtMysqlHost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMysqlHost.Location = new System.Drawing.Point(211, 24);
            this.txtMysqlHost.Name = "txtMysqlHost";
            this.txtMysqlHost.Size = new System.Drawing.Size(198, 20);
            this.txtMysqlHost.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 13);
            this.label9.TabIndex = 0;
            this.label9.Tag = "8022";
            this.label9.Text = "Hostname o IP";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(483, 320);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Tag = "8002";
            this.tabPage3.Text = "Inbound";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.propInbound);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(473, 309);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Tag = "8009";
            this.groupBox4.Text = "Contesti Inbound / Outbound";
            // 
            // propInbound
            // 
            this.propInbound.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.propInbound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.propInbound.HelpVisible = false;
            this.propInbound.Location = new System.Drawing.Point(6, 19);
            this.propInbound.Name = "propInbound";
            this.propInbound.Size = new System.Drawing.Size(456, 284);
            this.propInbound.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox7);
            this.tabPage4.Controls.Add(this.groupBox6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(483, 320);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Tag = "8003";
            this.tabPage4.Text = "Altro";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.chkStartWithWindows);
            this.groupBox7.Controls.Add(this.chkMinimizeOnStart);
            this.groupBox7.Controls.Add(this.btnUIFont);
            this.groupBox7.Controls.Add(this.txtUIFont);
            this.groupBox7.Controls.Add(this.label15);
            this.groupBox7.Controls.Add(this.cbLanguage);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(3, 96);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(473, 217);
            this.groupBox7.TabIndex = 3;
            this.groupBox7.TabStop = false;
            this.groupBox7.Tag = "8012";
            this.groupBox7.Text = "Altro";
            // 
            // chkStartWithWindows
            // 
            this.chkStartWithWindows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkStartWithWindows.Enabled = false;
            this.chkStartWithWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStartWithWindows.Location = new System.Drawing.Point(5, 101);
            this.chkStartWithWindows.Name = "chkStartWithWindows";
            this.chkStartWithWindows.Size = new System.Drawing.Size(219, 17);
            this.chkStartWithWindows.TabIndex = 9;
            this.chkStartWithWindows.Tag = "8034";
            this.chkStartWithWindows.Text = "Avvia con Windows";
            this.chkStartWithWindows.UseVisualStyleBackColor = true;
            // 
            // chkMinimizeOnStart
            // 
            this.chkMinimizeOnStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMinimizeOnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMinimizeOnStart.Location = new System.Drawing.Point(5, 78);
            this.chkMinimizeOnStart.Name = "chkMinimizeOnStart";
            this.chkMinimizeOnStart.Size = new System.Drawing.Size(219, 17);
            this.chkMinimizeOnStart.TabIndex = 8;
            this.chkMinimizeOnStart.Tag = "8033";
            this.chkMinimizeOnStart.Text = "Minimizza all\'avvio";
            this.chkMinimizeOnStart.UseVisualStyleBackColor = true;
            // 
            // btnUIFont
            // 
            this.btnUIFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUIFont.Location = new System.Drawing.Point(415, 49);
            this.btnUIFont.Name = "btnUIFont";
            this.btnUIFont.Size = new System.Drawing.Size(36, 20);
            this.btnUIFont.TabIndex = 7;
            this.btnUIFont.Text = "...";
            this.btnUIFont.UseVisualStyleBackColor = true;
            this.btnUIFont.Click += new System.EventHandler(this.btnUIFont_Click);
            // 
            // txtUIFont
            // 
            this.txtUIFont.BackColor = System.Drawing.SystemColors.Window;
            this.txtUIFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUIFont.Location = new System.Drawing.Point(211, 49);
            this.txtUIFont.Name = "txtUIFont";
            this.txtUIFont.ReadOnly = true;
            this.txtUIFont.Size = new System.Drawing.Size(198, 20);
            this.txtUIFont.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 52);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 5;
            this.label15.Tag = "8032";
            this.label15.Text = "Font Interfaccia";
            // 
            // cbLanguage
            // 
            this.cbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.ImageList = this.imageList1;
            this.cbLanguage.ItemHeight = 13;
            this.cbLanguage.Location = new System.Drawing.Point(211, 22);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(198, 19);
            this.cbLanguage.TabIndex = 4;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ad.png");
            this.imageList1.Images.SetKeyName(1, "ae.png");
            this.imageList1.Images.SetKeyName(2, "af.png");
            this.imageList1.Images.SetKeyName(3, "ag.png");
            this.imageList1.Images.SetKeyName(4, "ai.png");
            this.imageList1.Images.SetKeyName(5, "al.png");
            this.imageList1.Images.SetKeyName(6, "am.png");
            this.imageList1.Images.SetKeyName(7, "an.png");
            this.imageList1.Images.SetKeyName(8, "ao.png");
            this.imageList1.Images.SetKeyName(9, "ar.png");
            this.imageList1.Images.SetKeyName(10, "as.png");
            this.imageList1.Images.SetKeyName(11, "at.png");
            this.imageList1.Images.SetKeyName(12, "au.png");
            this.imageList1.Images.SetKeyName(13, "aw.png");
            this.imageList1.Images.SetKeyName(14, "ax.png");
            this.imageList1.Images.SetKeyName(15, "az.png");
            this.imageList1.Images.SetKeyName(16, "ba.png");
            this.imageList1.Images.SetKeyName(17, "bb.png");
            this.imageList1.Images.SetKeyName(18, "bd.png");
            this.imageList1.Images.SetKeyName(19, "be.png");
            this.imageList1.Images.SetKeyName(20, "bf.png");
            this.imageList1.Images.SetKeyName(21, "bg.png");
            this.imageList1.Images.SetKeyName(22, "bh.png");
            this.imageList1.Images.SetKeyName(23, "bi.png");
            this.imageList1.Images.SetKeyName(24, "bj.png");
            this.imageList1.Images.SetKeyName(25, "bm.png");
            this.imageList1.Images.SetKeyName(26, "bn.png");
            this.imageList1.Images.SetKeyName(27, "bo.png");
            this.imageList1.Images.SetKeyName(28, "br.png");
            this.imageList1.Images.SetKeyName(29, "bs.png");
            this.imageList1.Images.SetKeyName(30, "bt.png");
            this.imageList1.Images.SetKeyName(31, "bv.png");
            this.imageList1.Images.SetKeyName(32, "bw.png");
            this.imageList1.Images.SetKeyName(33, "by.png");
            this.imageList1.Images.SetKeyName(34, "bz.png");
            this.imageList1.Images.SetKeyName(35, "ca.png");
            this.imageList1.Images.SetKeyName(36, "catalonia.png");
            this.imageList1.Images.SetKeyName(37, "cc.png");
            this.imageList1.Images.SetKeyName(38, "cd.png");
            this.imageList1.Images.SetKeyName(39, "cf.png");
            this.imageList1.Images.SetKeyName(40, "cg.png");
            this.imageList1.Images.SetKeyName(41, "ch.png");
            this.imageList1.Images.SetKeyName(42, "ci.png");
            this.imageList1.Images.SetKeyName(43, "ck.png");
            this.imageList1.Images.SetKeyName(44, "cl.png");
            this.imageList1.Images.SetKeyName(45, "cm.png");
            this.imageList1.Images.SetKeyName(46, "cn.png");
            this.imageList1.Images.SetKeyName(47, "co.png");
            this.imageList1.Images.SetKeyName(48, "cr.png");
            this.imageList1.Images.SetKeyName(49, "cs.png");
            this.imageList1.Images.SetKeyName(50, "cu.png");
            this.imageList1.Images.SetKeyName(51, "cv.png");
            this.imageList1.Images.SetKeyName(52, "cx.png");
            this.imageList1.Images.SetKeyName(53, "cy.png");
            this.imageList1.Images.SetKeyName(54, "cz.png");
            this.imageList1.Images.SetKeyName(55, "de.png");
            this.imageList1.Images.SetKeyName(56, "dj.png");
            this.imageList1.Images.SetKeyName(57, "dk.png");
            this.imageList1.Images.SetKeyName(58, "dm.png");
            this.imageList1.Images.SetKeyName(59, "do.png");
            this.imageList1.Images.SetKeyName(60, "dz.png");
            this.imageList1.Images.SetKeyName(61, "ec.png");
            this.imageList1.Images.SetKeyName(62, "ee.png");
            this.imageList1.Images.SetKeyName(63, "eg.png");
            this.imageList1.Images.SetKeyName(64, "eh.png");
            this.imageList1.Images.SetKeyName(65, "england.png");
            this.imageList1.Images.SetKeyName(66, "er.png");
            this.imageList1.Images.SetKeyName(67, "es.png");
            this.imageList1.Images.SetKeyName(68, "et.png");
            this.imageList1.Images.SetKeyName(69, "europeanunion.png");
            this.imageList1.Images.SetKeyName(70, "fam.png");
            this.imageList1.Images.SetKeyName(71, "fi.png");
            this.imageList1.Images.SetKeyName(72, "fj.png");
            this.imageList1.Images.SetKeyName(73, "fk.png");
            this.imageList1.Images.SetKeyName(74, "fm.png");
            this.imageList1.Images.SetKeyName(75, "fo.png");
            this.imageList1.Images.SetKeyName(76, "fr.png");
            this.imageList1.Images.SetKeyName(77, "ga.png");
            this.imageList1.Images.SetKeyName(78, "gb.png");
            this.imageList1.Images.SetKeyName(79, "gd.png");
            this.imageList1.Images.SetKeyName(80, "ge.png");
            this.imageList1.Images.SetKeyName(81, "gf.png");
            this.imageList1.Images.SetKeyName(82, "gh.png");
            this.imageList1.Images.SetKeyName(83, "gi.png");
            this.imageList1.Images.SetKeyName(84, "gl.png");
            this.imageList1.Images.SetKeyName(85, "gm.png");
            this.imageList1.Images.SetKeyName(86, "gn.png");
            this.imageList1.Images.SetKeyName(87, "gp.png");
            this.imageList1.Images.SetKeyName(88, "gq.png");
            this.imageList1.Images.SetKeyName(89, "gr.png");
            this.imageList1.Images.SetKeyName(90, "gs.png");
            this.imageList1.Images.SetKeyName(91, "gt.png");
            this.imageList1.Images.SetKeyName(92, "gu.png");
            this.imageList1.Images.SetKeyName(93, "gw.png");
            this.imageList1.Images.SetKeyName(94, "gy.png");
            this.imageList1.Images.SetKeyName(95, "hk.png");
            this.imageList1.Images.SetKeyName(96, "hm.png");
            this.imageList1.Images.SetKeyName(97, "hn.png");
            this.imageList1.Images.SetKeyName(98, "hr.png");
            this.imageList1.Images.SetKeyName(99, "ht.png");
            this.imageList1.Images.SetKeyName(100, "hu.png");
            this.imageList1.Images.SetKeyName(101, "id.png");
            this.imageList1.Images.SetKeyName(102, "ie.png");
            this.imageList1.Images.SetKeyName(103, "il.png");
            this.imageList1.Images.SetKeyName(104, "in.png");
            this.imageList1.Images.SetKeyName(105, "io.png");
            this.imageList1.Images.SetKeyName(106, "iq.png");
            this.imageList1.Images.SetKeyName(107, "ir.png");
            this.imageList1.Images.SetKeyName(108, "is.png");
            this.imageList1.Images.SetKeyName(109, "it.png");
            this.imageList1.Images.SetKeyName(110, "jm.png");
            this.imageList1.Images.SetKeyName(111, "jo.png");
            this.imageList1.Images.SetKeyName(112, "jp.png");
            this.imageList1.Images.SetKeyName(113, "ke.png");
            this.imageList1.Images.SetKeyName(114, "kg.png");
            this.imageList1.Images.SetKeyName(115, "kh.png");
            this.imageList1.Images.SetKeyName(116, "ki.png");
            this.imageList1.Images.SetKeyName(117, "km.png");
            this.imageList1.Images.SetKeyName(118, "kn.png");
            this.imageList1.Images.SetKeyName(119, "kp.png");
            this.imageList1.Images.SetKeyName(120, "kr.png");
            this.imageList1.Images.SetKeyName(121, "kw.png");
            this.imageList1.Images.SetKeyName(122, "ky.png");
            this.imageList1.Images.SetKeyName(123, "kz.png");
            this.imageList1.Images.SetKeyName(124, "la.png");
            this.imageList1.Images.SetKeyName(125, "lb.png");
            this.imageList1.Images.SetKeyName(126, "lc.png");
            this.imageList1.Images.SetKeyName(127, "li.png");
            this.imageList1.Images.SetKeyName(128, "lk.png");
            this.imageList1.Images.SetKeyName(129, "lr.png");
            this.imageList1.Images.SetKeyName(130, "ls.png");
            this.imageList1.Images.SetKeyName(131, "lt.png");
            this.imageList1.Images.SetKeyName(132, "lu.png");
            this.imageList1.Images.SetKeyName(133, "lv.png");
            this.imageList1.Images.SetKeyName(134, "ly.png");
            this.imageList1.Images.SetKeyName(135, "ma.png");
            this.imageList1.Images.SetKeyName(136, "mc.png");
            this.imageList1.Images.SetKeyName(137, "md.png");
            this.imageList1.Images.SetKeyName(138, "me.png");
            this.imageList1.Images.SetKeyName(139, "mg.png");
            this.imageList1.Images.SetKeyName(140, "mh.png");
            this.imageList1.Images.SetKeyName(141, "mk.png");
            this.imageList1.Images.SetKeyName(142, "ml.png");
            this.imageList1.Images.SetKeyName(143, "mm.png");
            this.imageList1.Images.SetKeyName(144, "mn.png");
            this.imageList1.Images.SetKeyName(145, "mo.png");
            this.imageList1.Images.SetKeyName(146, "mp.png");
            this.imageList1.Images.SetKeyName(147, "mq.png");
            this.imageList1.Images.SetKeyName(148, "mr.png");
            this.imageList1.Images.SetKeyName(149, "ms.png");
            this.imageList1.Images.SetKeyName(150, "mt.png");
            this.imageList1.Images.SetKeyName(151, "mu.png");
            this.imageList1.Images.SetKeyName(152, "mv.png");
            this.imageList1.Images.SetKeyName(153, "mw.png");
            this.imageList1.Images.SetKeyName(154, "mx.png");
            this.imageList1.Images.SetKeyName(155, "my.png");
            this.imageList1.Images.SetKeyName(156, "mz.png");
            this.imageList1.Images.SetKeyName(157, "na.png");
            this.imageList1.Images.SetKeyName(158, "nc.png");
            this.imageList1.Images.SetKeyName(159, "ne.png");
            this.imageList1.Images.SetKeyName(160, "nf.png");
            this.imageList1.Images.SetKeyName(161, "ng.png");
            this.imageList1.Images.SetKeyName(162, "ni.png");
            this.imageList1.Images.SetKeyName(163, "nl.png");
            this.imageList1.Images.SetKeyName(164, "no.png");
            this.imageList1.Images.SetKeyName(165, "np.png");
            this.imageList1.Images.SetKeyName(166, "nr.png");
            this.imageList1.Images.SetKeyName(167, "nu.png");
            this.imageList1.Images.SetKeyName(168, "nz.png");
            this.imageList1.Images.SetKeyName(169, "om.png");
            this.imageList1.Images.SetKeyName(170, "pa.png");
            this.imageList1.Images.SetKeyName(171, "pe.png");
            this.imageList1.Images.SetKeyName(172, "pf.png");
            this.imageList1.Images.SetKeyName(173, "pg.png");
            this.imageList1.Images.SetKeyName(174, "ph.png");
            this.imageList1.Images.SetKeyName(175, "pk.png");
            this.imageList1.Images.SetKeyName(176, "pl.png");
            this.imageList1.Images.SetKeyName(177, "pm.png");
            this.imageList1.Images.SetKeyName(178, "pn.png");
            this.imageList1.Images.SetKeyName(179, "pr.png");
            this.imageList1.Images.SetKeyName(180, "ps.png");
            this.imageList1.Images.SetKeyName(181, "pt.png");
            this.imageList1.Images.SetKeyName(182, "pw.png");
            this.imageList1.Images.SetKeyName(183, "py.png");
            this.imageList1.Images.SetKeyName(184, "qa.png");
            this.imageList1.Images.SetKeyName(185, "re.png");
            this.imageList1.Images.SetKeyName(186, "ro.png");
            this.imageList1.Images.SetKeyName(187, "rs.png");
            this.imageList1.Images.SetKeyName(188, "ru.png");
            this.imageList1.Images.SetKeyName(189, "rw.png");
            this.imageList1.Images.SetKeyName(190, "sa.png");
            this.imageList1.Images.SetKeyName(191, "sb.png");
            this.imageList1.Images.SetKeyName(192, "sc.png");
            this.imageList1.Images.SetKeyName(193, "scotland.png");
            this.imageList1.Images.SetKeyName(194, "sd.png");
            this.imageList1.Images.SetKeyName(195, "se.png");
            this.imageList1.Images.SetKeyName(196, "sg.png");
            this.imageList1.Images.SetKeyName(197, "sh.png");
            this.imageList1.Images.SetKeyName(198, "si.png");
            this.imageList1.Images.SetKeyName(199, "sj.png");
            this.imageList1.Images.SetKeyName(200, "sk.png");
            this.imageList1.Images.SetKeyName(201, "sl.png");
            this.imageList1.Images.SetKeyName(202, "sm.png");
            this.imageList1.Images.SetKeyName(203, "sn.png");
            this.imageList1.Images.SetKeyName(204, "so.png");
            this.imageList1.Images.SetKeyName(205, "sr.png");
            this.imageList1.Images.SetKeyName(206, "st.png");
            this.imageList1.Images.SetKeyName(207, "sv.png");
            this.imageList1.Images.SetKeyName(208, "sy.png");
            this.imageList1.Images.SetKeyName(209, "sz.png");
            this.imageList1.Images.SetKeyName(210, "tc.png");
            this.imageList1.Images.SetKeyName(211, "td.png");
            this.imageList1.Images.SetKeyName(212, "tf.png");
            this.imageList1.Images.SetKeyName(213, "tg.png");
            this.imageList1.Images.SetKeyName(214, "th.png");
            this.imageList1.Images.SetKeyName(215, "tj.png");
            this.imageList1.Images.SetKeyName(216, "tk.png");
            this.imageList1.Images.SetKeyName(217, "tl.png");
            this.imageList1.Images.SetKeyName(218, "tm.png");
            this.imageList1.Images.SetKeyName(219, "tn.png");
            this.imageList1.Images.SetKeyName(220, "to.png");
            this.imageList1.Images.SetKeyName(221, "tr.png");
            this.imageList1.Images.SetKeyName(222, "tt.png");
            this.imageList1.Images.SetKeyName(223, "tv.png");
            this.imageList1.Images.SetKeyName(224, "tw.png");
            this.imageList1.Images.SetKeyName(225, "tz.png");
            this.imageList1.Images.SetKeyName(226, "ua.png");
            this.imageList1.Images.SetKeyName(227, "ug.png");
            this.imageList1.Images.SetKeyName(228, "um.png");
            this.imageList1.Images.SetKeyName(229, "us.png");
            this.imageList1.Images.SetKeyName(230, "uy.png");
            this.imageList1.Images.SetKeyName(231, "uz.png");
            this.imageList1.Images.SetKeyName(232, "va.png");
            this.imageList1.Images.SetKeyName(233, "vc.png");
            this.imageList1.Images.SetKeyName(234, "ve.png");
            this.imageList1.Images.SetKeyName(235, "vg.png");
            this.imageList1.Images.SetKeyName(236, "vi.png");
            this.imageList1.Images.SetKeyName(237, "vn.png");
            this.imageList1.Images.SetKeyName(238, "vu.png");
            this.imageList1.Images.SetKeyName(239, "wales.png");
            this.imageList1.Images.SetKeyName(240, "wf.png");
            this.imageList1.Images.SetKeyName(241, "ws.png");
            this.imageList1.Images.SetKeyName(242, "ye.png");
            this.imageList1.Images.SetKeyName(243, "yt.png");
            this.imageList1.Images.SetKeyName(244, "za.png");
            this.imageList1.Images.SetKeyName(245, "zm.png");
            this.imageList1.Images.SetKeyName(246, "zw.png");
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 26);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(92, 13);
            this.label14.TabIndex = 2;
            this.label14.Tag = "8031";
            this.label14.Text = "Lingua Interfaccia";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.chkIBCloseChilds);
            this.groupBox6.Controls.Add(this.chkIBShowGo);
            this.groupBox6.Controls.Add(this.chkIBAllowUrls);
            this.groupBox6.Controls.Add(this.chkIBAddrBar);
            this.groupBox6.Controls.Add(this.chkIBStatusBar);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(4, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(473, 87);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Tag = "8011";
            this.groupBox6.Text = "Browser Interno";
            // 
            // chkIBCloseChilds
            // 
            this.chkIBCloseChilds.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIBCloseChilds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIBCloseChilds.Location = new System.Drawing.Point(6, 65);
            this.chkIBCloseChilds.Name = "chkIBCloseChilds";
            this.chkIBCloseChilds.Size = new System.Drawing.Size(163, 17);
            this.chkIBCloseChilds.TabIndex = 5;
            this.chkIBCloseChilds.Tag = "8030";
            this.chkIBCloseChilds.Text = "Chiudi Finestre figlie";
            this.chkIBCloseChilds.UseVisualStyleBackColor = true;
            // 
            // chkIBShowGo
            // 
            this.chkIBShowGo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIBShowGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIBShowGo.Location = new System.Drawing.Point(186, 42);
            this.chkIBShowGo.Name = "chkIBShowGo";
            this.chkIBShowGo.Size = new System.Drawing.Size(163, 17);
            this.chkIBShowGo.TabIndex = 4;
            this.chkIBShowGo.Tag = "8029";
            this.chkIBShowGo.Text = "Visualizza Bottone Vai";
            this.chkIBShowGo.UseVisualStyleBackColor = true;
            // 
            // chkIBAllowUrls
            // 
            this.chkIBAllowUrls.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIBAllowUrls.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIBAllowUrls.Location = new System.Drawing.Point(186, 19);
            this.chkIBAllowUrls.Name = "chkIBAllowUrls";
            this.chkIBAllowUrls.Size = new System.Drawing.Size(163, 17);
            this.chkIBAllowUrls.TabIndex = 3;
            this.chkIBAllowUrls.Tag = "8028";
            this.chkIBAllowUrls.Text = "Consenti Inserimento URLs";
            this.chkIBAllowUrls.UseVisualStyleBackColor = true;
            // 
            // chkIBAddrBar
            // 
            this.chkIBAddrBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIBAddrBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIBAddrBar.Location = new System.Drawing.Point(6, 42);
            this.chkIBAddrBar.Name = "chkIBAddrBar";
            this.chkIBAddrBar.Size = new System.Drawing.Size(163, 17);
            this.chkIBAddrBar.TabIndex = 2;
            this.chkIBAddrBar.Tag = "8027";
            this.chkIBAddrBar.Text = "Visualizza Barra Indirizzi";
            this.chkIBAddrBar.UseVisualStyleBackColor = true;
            // 
            // chkIBStatusBar
            // 
            this.chkIBStatusBar.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIBStatusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIBStatusBar.Location = new System.Drawing.Point(6, 19);
            this.chkIBStatusBar.Name = "chkIBStatusBar";
            this.chkIBStatusBar.Size = new System.Drawing.Size(163, 17);
            this.chkIBStatusBar.TabIndex = 1;
            this.chkIBStatusBar.Tag = "8026";
            this.chkIBStatusBar.Text = "Visualizza Statusbar";
            this.chkIBStatusBar.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 346);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 50);
            this.panel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(259, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Tag = "7998";
            this.btnCancel.Text = "Annulla";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(382, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(98, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Tag = "7999";
            this.btnConfirm.Text = "Conferma";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 396);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Impostazioni";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClientExten;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCtiServerTimeout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCtiServerPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCtiServerHostname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClientPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtClientUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtMysqlPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMysqlUser;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMysqlHost;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox cboCalleridShow;
        private System.Windows.Forms.TextBox txtCalleridFadeoutSpeed;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCalleridTimeout;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMysqlPort;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkIBAllowUrls;
        private System.Windows.Forms.CheckBox chkIBAddrBar;
        private System.Windows.Forms.CheckBox chkIBStatusBar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox chkIBCloseChilds;
        private System.Windows.Forms.CheckBox chkIBShowGo;
        private System.Windows.Forms.Button btnUIFont;
        private System.Windows.Forms.TextBox txtUIFont;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ImageCombo  cbLanguage;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox chkStartWithWindows;
        private System.Windows.Forms.CheckBox chkMinimizeOnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.PropertyGrid propInbound;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ImageList imageList1;
    }
}