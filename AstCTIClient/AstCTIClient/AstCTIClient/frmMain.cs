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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using SettingsManager;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Xml;
using System.Resources;
using System.Globalization;


namespace AstCTIClient
{
    public partial class frmMain : Form
    {
       
        #region delegates
        delegate void SetPropertyDelegate(object ctl, string objName, object newValue);
        delegate void SetMethodDelegate(object obj, string methodName, object[] parameters);
        delegate DialogResult FormShowdialogDelegate(Form f);
        delegate void FormShowDelegate(Form f);
        #endregion

        private SocketManager socketmanager;
        private ProtocolParser parser;
        private PROTOCOL_STATES protocolStatus;
        private System.Windows.Forms.Timer noOpTimer = null;

        private string strResourcesPath = Application.StartupPath + Path.DirectorySeparatorChar + "lang";
        private string strCulture = "en-US";
        private static ResourceManager rm;

        private bool bHideConfiguration = false;

        private SettingsManager.SettingsManager sm;
        private LocalAppSettings optset = null;
        //const and dll functions for moving form
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
            int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        delegate void GenericAction();
        delegate void GenericStringAction(string param);
        
        public frmMain()
        {
            InitializeComponent();
            Stream s = this.GetType().Assembly.GetManifestResourceStream("AstCTIClient.mainform.gif");
            Bitmap bmp = new Bitmap(s);
            this.DoubleBuffered = true;
            this.Region = BitmapToRegion.Convert(bmp, bmp.GetPixel(0, 0), TransparencyMode.ColorKeyTransparent);
            this.notifyIcon1.Visible = false;
            this.MouseDown += new MouseEventHandler(frmMain_MouseDown);
            this.notifyIcon1.DoubleClick += new EventHandler(notifyIcon1_DoubleClick);
            this.txtPassword.LostFocus += new EventHandler(txtPassword_LostFocus);
            this.txtUsername.LostFocus += new EventHandler(txtUsername_LostFocus);
            this.txtPhoneNumber.TextChanged += new EventHandler(txtPhoneNumber_TextChanged);
            this.cboOutboundContextes.SelectedIndexChanged += new EventHandler(cboOutboundContextes_SelectedIndexChanged);
            this.Load += new EventHandler(frmMain_Load);
            this.socketmanager = null;
            this.parser = null;
            this.protocolStatus = PROTOCOL_STATES.STATUS_UNKNOWN;
            this.lblLineState.Text = "";
            sm = new SettingsManager.SettingsManager();
            

            this.CheckRegistrySettings();

            try
            {
                if (optset == null)
                {
                    sm.AppSettingsObject = new LocalAppSettings();
                    sm.ReadConfig();
                    optset = (LocalAppSettings)sm.AppSettingsObject;
                }

                if (this.parser == null)
                {
                    this.parser = new ProtocolParser();
                    this.parser.Parsed += new ProtocolParser.OnParsed(parser_Parsed);
                }
                if (this.socketmanager == null)
                {
                    this.socketmanager = new SocketManager(this.optset.Host, this.optset.Port);
                    this.socketmanager.Connected += new SocketManager.OnConnected(socketmanager_Connected);
                    this.socketmanager.Disconnected += new SocketManager.OnDisconnected(socketmanager_Disconnected);
                    this.socketmanager.DataArrival += new SocketManager.OnDataArrival(socketmanager_DataArrival);
                    this.socketmanager.SocketError += new SocketManager.OnSocketError(socketmanager_SocketError);
                }
                this.noOpTimer = new System.Windows.Forms.Timer();
                this.noOpTimer.Tick += new EventHandler(noOpTimer_Tick);
                this.noOpTimer.Interval = this.optset.SocketTimeout;
                this.UpdateOutboundContextes();
                this.txtUsername.Text = optset.Username;
                this.txtPassword.Text = optset.Password;
                this.lblExtension.Text = optset.PhoneExt;                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString());
            }

            
        }


        void frmMain_Load(object sender, EventArgs e)
        {
            GlobalizeApp();            
        }

        /// <summary>
        /// This function checks registry settings bHideConfiguration and bOnlyCti
        /// to see if the software should hide configuration button and/or hide
        /// outbound context and dial button
        /// </summary>
        void CheckRegistrySettings()
        {
            RegistryManager reg = new RegistryManager();
            string retval = reg.Read("bHideConfiguration");
            if (retval != null)
            {
                this.btnConfig.Visible = !retval.Equals("true");
            }
            else
            {
                reg.Write("bHideConfiguration", "false");

            }

            retval = reg.Read("bOnlyCti");
            if (retval != null)
            {
                this.txtPhoneNumber.Visible = !retval.Equals("true"); ;
                this.btnDial.Visible = !retval.Equals("true"); ;
                this.cboOutboundContextes.Visible = !retval.Equals("true"); ;
            }
            else
            {
                reg.Write("bOnlyCti", "false");

            }
        }

        /// <summary>
        /// Detects error in socket and show a Message Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        /// <param name="errorCode"></param>
        void socketmanager_SocketError(object sender, string data, int errorCode)
        {
            if (errorCode == 10061)
            {
                MessageBoxShow(data);
            }
        }

        /// <summary>
        /// When the cboOutBoundContextes combo have been changed, checks
        /// if we can enable dial actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cboOutboundContextes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.btnDial.Enabled = this.CanEnableDialButton();
        }

        /// <summary>
        /// When the text in txtPhoneNumber have been changed, checks
        /// if we can enable dial actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            this.btnDial.Enabled = this.CanEnableDialButton();
        }

        /// <summary>
        /// Sends NOOP to the server as some sort of keep-alive
        /// to avoid the connection is closed from the remote server side
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void noOpTimer_Tick(object sender, EventArgs e)
        {
            if (this.socketmanager.IsConnected)
            {
                this.socketmanager.SendData("NOOP");
            }
        }

        /// <summary>
        /// Save the username when the focus is lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtUsername_LostFocus(object sender, EventArgs e)
        {
            if (this.optset != null)
            {
                this.optset.Username = txtUsername.Text;
            }
        }

        /// <summary>
        /// Save the password when the focus is lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtPassword_LostFocus(object sender, EventArgs e)
        {
            if (this.optset != null)
            {
                this.optset.Password = txtPassword.Text;
            }
        }

        /// <summary>
        /// Check if mouse left button is down over the form: 
        /// we can move the form.
        /// The functions to move the form are called in MouseDown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
       
        }

        private bool MySQLRegister()
        {
            string connStr = "Persist Security Info=False;" +
                     "database=" + this.optset.MySQLDatabase  + ";" +
                     "server=" + this.optset.MySQLHost + ";" +
                     "user id=" + this.optset.MySQLUserName + ";" +
                     "Password=" + this.optset.MySQLPassword + ";" +
                     "Compress=false";
            MySqlCommand cmd = null;
            MySqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            
            MySqlConnection cn = new MySqlConnection(connStr);
            try
            {
                string sql = "";

                cn.Open();
                sql = "SELECT * FROM cti WHERE USERNAME=?user";

                cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.Add(new MySqlParameter("?user", this.optset.Username));
                cmd.Prepare();

                da = new MySqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds, "cti");
                dt = ds.Tables["cti"];
                if (dt.Rows.Count < 1)
                {
                    return false;
                }
                else
                {
                    sql = "UPDATE cti SET EXT=?ext WHERE USERNAME=?user";
                }
                dt.Dispose();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();

                cmd = new MySqlCommand(sql, cn);
                cmd.Parameters.Add(new MySqlParameter("?user", this.optset.Username));                
                cmd.Parameters.Add(new MySqlParameter("?ext", this.optset.PhoneExt));
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                cmd.Dispose();
                cn.Close();
                cn.Dispose();

                return true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //private bool MySQLUnRegister()
        //{
        //    string connStr = "Persist Security Info=False;" +
        //             "database=" + this.optset.MySQLDatabase + ";" +
        //             "server=" + this.optset.MySQLHost + ";" +
        //             "user id=" + this.optset.MySQLUserName + ";" +
        //             "Password=" + this.optset.MySQLPassword + ";" +
        //             "Compress=false";
        //    MySqlCommand cmd = null;
            
        //    MySqlConnection cn = new MySqlConnection(connStr);
        //    try
        //    {
        //        string sql = "";

        //        cn.Open();
        //        sql = "DELETE FROM cti WHERE HOST=?host";

        //        cmd = new MySqlCommand(sql, cn);
        //        cmd.Parameters.Add(new MySqlParameter("?host", this.optset.Host));
        //        cmd.Prepare();

        //        cmd.ExecuteNonQuery();

        //        cmd.Dispose();
        //        cn.Close();
        //        cn.Dispose();

        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //}

        public void DoFormHide()
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)this, "Visible",false });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)this, "ShowInTaskbar", false });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)notifyIcon1, "Visible", true });
            }
            else
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
        }

        public void DoFormShow()
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)this, "Visible", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)this, "ShowInTaskbar", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)notifyIcon1, "Visible", false });
            }
            else
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.notifyIcon1.Visible = false;
                
            }
            
        }

        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.DoFormShow();   
        }        

        private void mnuShowHide_Click(object sender, EventArgs e)
        {
            this.DoFormShow();
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            this.DoFormHide();           
            
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text.Equals(frmMain.RM.GetString("0008")))
            {
                CheckRegistrySettings();
                btnStartStop.Enabled = false;
                if (!MySQLRegister())
                {
                    btnStartStop.Enabled = true;
                    return;
                }
                if (this.socketmanager != null)
                {
                    this.socketmanager.Connect();
                }
                
            }
            else
            {
                this.socketmanager.Disconnect();
                //MySQLUnRegister();
                this.pnlExtension.Visible = false;
            }
        }

        void parser_Parsed(int message, string data)
        {
            if (message == 0)
            {
                #region Asterisk Events
                string eventId = "";
                string state = "";
                AsteriskCall call = AsteriskCall.CallFromXml(data, out eventId);
                if (call != null)
                {
                    switch (eventId)
                    {
                        case "Newstate":
                            state = call.State;
                            if (state != null)
                            {
                                switch (state)
                                {
                                    case "Up":
                                        state = frmMain.RM.GetString("0101"); // "Off Hook";
                                        break;
                                    case "Ringing":
                                        state = frmMain.RM.GetString("0102");  // "Ringing";
                                        break;
                                    case "Down":
                                        state = frmMain.RM.GetString("0103");  //"On Hook";
                                        break;
                                }
                            }
                            else
                            {
                                state = "";
                            }
                            Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)lblLineState, "Text", state });
                            break;
                        case "Newcallerid":
                            if (this.optset.TriggerCallerId)
                            {
                                // Check matching contextes before popup
                                GenericStringAction scf = new GenericStringAction(this.OpenCallerIdForm);
                                
                                scf.Invoke(call.CallerIDNum);
                                return;
                            }
                            break;
                        case "Link":
                            EnableCallActions(false);
                            CTIContext tostart = null;
                            foreach(CTIContext ctx in this.optset.CTIContextes) {
                                if (ctx.Context.Equals(call.Context))
                                {
                                    if (ctx.Enabled)
                                    {
                                        tostart = ctx;
                                        break;
                                    }
                                }
                            }
                            state = frmMain.RM.GetString("0104"); // "Connected"
                            Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)lblLineState, "Text", state });
                            if (tostart != null) ExecCTIApplication(tostart,call);
                            break;
                        case "Hangup":
                            state = frmMain.RM.GetString("0105"); // "Hangup"
                            Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)lblLineState, "Text", state });
                            EnableCallActions(true);
                            break;
                    }
                }
                #endregion
            }
            else
            {
                #region Main Switch
                switch (message)
                {
                    case 100:
                        switch (this.protocolStatus)
                        {
                            case PROTOCOL_STATES.STATUS_UNKNOWN:
                                this.protocolStatus = PROTOCOL_STATES.SENDED_USERNAME;
                                this.socketmanager.SendData("USER " + this.txtUsername.Text);
                                break;
                            case PROTOCOL_STATES.SENDED_USERNAME:
                                this.protocolStatus = PROTOCOL_STATES.SENDED_PASSWORD;
                                this.socketmanager.SendData("PASS " + ProtocolParser.MD5(this.txtPassword.Text));
                                break;
                            default:
                                break;
                        }
                        break;
                    case 101:
                        if (this.socketmanager.IsConnected)
                        {
                            this.socketmanager.SendData("QUIT");
                            this.socketmanager.Disconnect();
                            MessageBox.Show(frmMain.RM.GetString("0014")); // "Invalid credentials"
                        }
                        break;
                    case 102:
                        switch (this.protocolStatus)
                        {
                            case PROTOCOL_STATES.SENDED_PASSWORD:
                                this.protocolStatus = PROTOCOL_STATES.LOGGED_IN;
                                if (this.InvokeRequired)
                                {
                                    Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnStartStop, "Text", frmMain.RM.GetString("0009") });
                                    Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnStartStop, "Enabled", true });
                                    Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnConfig, "Enabled", false });
                                    Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)lblLineState, "Text", "" });
                                    if (this.optset.MinimizeOnStart)
                                    {
                                        Invoke(new SetMethodDelegate(SetMethod), new object[] { (object)this, "DoFormHide", null });
                                    }
                                    
                                }
                                else
                                {
                                    this.btnStartStop.Text = frmMain.RM.GetString("0009");
                                    this.btnStartStop.Enabled = true;
                                    this.btnConfig.Enabled = false;
                                    this.DoFormHide();
                                }
                                break;
                        }
                        break;
                }
                #endregion
            }
        }

        void socketmanager_DataArrival(object sender, string data)
        {
            this.parser.Parse(data);
        }

        void socketmanager_Disconnected(object sender)
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnStartStop, "Text", frmMain.RM.GetString("0008") });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnStartStop, "Enabled", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnConfig, "Enabled", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)pnlExtension, "Visible", false});
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)noOpTimer, "Enabled", false });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)lblLineState, "Text", "" });
            }
            else
            {
                this.btnStartStop.Text = frmMain.RM.GetString("0008");
                this.btnStartStop.Enabled = true;
                this.btnConfig.Enabled = true;
                this.pnlExtension.Visible = false;
                this.noOpTimer.Enabled = false;
                this.lblLineState.Text = "";
            }
            this.protocolStatus = PROTOCOL_STATES.STATUS_UNKNOWN;
            if (!this.Visible)
            {
                DoFormShow();
            }
        }

        void socketmanager_Connected(object sender)
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)noOpTimer, "Enabled", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)pnlExtension, "Visible", true });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnStartStop, "Enabled", true });
            }
            else
            {
                this.noOpTimer.Enabled = true;
                this.pnlExtension.Visible = true;
                this.btnStartStop.Enabled = true;
            }
            this.protocolStatus = PROTOCOL_STATES.STATUS_UNKNOWN;
        }

        void ExecCTIApplication(CTIContext ctx, AsteriskCall call)
        {
            if ( (!ctx.InternalBrowser) & (!File.Exists(ctx.Application)) )
            {
                GenericStringAction scf = new GenericStringAction(this.MessageBoxShow);
                scf.Invoke(string.Format("{0} {1}", frmMain.RM.GetString("0015"), ctx.DisplayName));
                return;
            }
            string parameters = ctx.Parameters;
            if (parameters != null)
            {
                parameters = parameters.Replace("{CALLERID}", call.CallerIDNum);
                parameters = parameters.Replace("{CALLERNAME}", call.CallerIDName);
                parameters = parameters.Replace("{CONTEXT}", call.Context);
                parameters = parameters.Replace("{CALLDATA}", call.AppData);
                parameters = parameters.Replace("{CHANNEL}", call.Channel);
                parameters = parameters.Replace("{UNIQUEID}", call.Uniqueid);
            }

            if (ctx.InternalBrowser)
            {
                
                Thread t = new Thread(this.OpenBrowserForm);
                t.SetApartmentState(ApartmentState.STA);
                t.Start(parameters);

                //GenericStringAction scf = new GenericStringAction(this.OpenBrowserForm);
                //scf.Invoke(parameters);
                
            }
            else
            {

                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.EnableRaisingEvents = false;
                proc.StartInfo.Arguments = parameters;
                proc.StartInfo.FileName = ctx.Application;
                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

                proc.Start();
            }
        }

        public void OpenBrowserForm(object szurl)
        {
            string url = (string)szurl;
            frmExplorer frmExp;
            if (url != null)
            {
                frmExp = new frmExplorer(this.optset, url);
            }
            else
            {
                frmExp = new frmExplorer(this.optset);
            }
            frmExp.ShowDialog();
        }

        void OpenCallerIdForm(string calleridnum)
        {
            frmCallerId frm = new frmCallerId(this.optset, calleridnum);
            frm.WindowState = FormWindowState.Normal;

            if (this.optset.TriggerCallerId)
            {
                if (this.InvokeRequired)
                {
                    Invoke(new FormShowDelegate(FormShow), new object[] { (object)frm });
                    Invoke(new SetMethodDelegate(SetMethod), new object[] { (object)frm, "StartTimer", new object[] { this.optset.CallerIdTimeout } });
                }
                else
                {
                    frm.Show();
                    frm.StartTimer(this.optset.CallerIdTimeout);
                }
            }
        }

        void MessageBoxShow(string message)
        {
            MessageBox.Show(null, message,"AstCTIClient", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        #region Metodi per l'INVOKE
        void SetProperty(object ctl, string propName, object newValue)
        {
            Type t = ctl.GetType();
            PropertyInfo pi = t.GetProperty(propName);

            if (pi != null)
            {
                pi.SetValue(ctl, newValue, null);
            }
        }

        void SetMethod(object ctl, string methodName, object[] parameters)
        {
            Type t = ctl.GetType();
            MethodInfo mi = t.GetMethod(methodName);

            if (mi != null)
            {
                mi.Invoke(ctl, parameters);
            }
        }

        DialogResult FormShowdialog(Form f)
        {
            return f.ShowDialog(this);
        }

        void FormShow(Form f)
        {
            
           f.Show();
        }
        #endregion

        private void btnConfig_Click(object sender, EventArgs e)
        {

            sm.EditConfig("Application Settings");
            try
            {
                sm.ReadConfig();
                optset = (LocalAppSettings)sm.AppSettingsObject;
                this.txtUsername.Text = optset.Username;
                this.txtPassword.Text = optset.Password;
                this.lblExtension.Text = optset.PhoneExt;

                this.UpdateOutboundContextes();

                if (this.noOpTimer != null)
                {
                    this.noOpTimer.Interval = this.optset.SocketTimeout;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            try
            {
                this.sm.AppSettingsObject = optset;
                this.sm = null;
                this.Close();
            }
            catch (Exception) { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string parameters = "http://centralino-voip.brunosalzano.com";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.Arguments = parameters;
            proc.StartInfo.FileName = "explorer.exe";
            proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;

            proc.Start();
        }

        private void btnDial_Click(object sender, EventArgs e)
        {
            if (this.txtPhoneNumber.Text.Equals(""))
            {
                MessageBox.Show(this, frmMain.RM.GetString("0016"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }
            if (this.cboOutboundContextes.SelectedItem == null) {
                MessageBox.Show(this, frmMain.RM.GetString("0017"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CTIOutboundContext ctx = (CTIOutboundContext)this.cboOutboundContextes.SelectedItem;
            if (ctx != null)
            {
                string cmd = string.Format("ORIG {0}|{1}|{2}",
                                this.txtPhoneNumber.Text,
                                ctx.Context,
                                ctx.Priority);
                EnableCallActions(false);
                this.socketmanager.SendData(cmd); 
            }
            
        }

        private void UpdateOutboundContextes()
        {
            this.cboOutboundContextes.Items.Clear();
            if (this.optset.CTIOutboundContextes.Count < 1)
            {
                this.txtPhoneNumber.Enabled = false;
                this.cboOutboundContextes.Enabled = false;
                this.btnDial.Enabled = false;
            }
            else
            {
                this.txtPhoneNumber.Enabled = true;
                this.cboOutboundContextes.Enabled = true;
                this.btnDial.Enabled = false;
                foreach (CTIOutboundContext ctx in optset.CTIOutboundContextes)
                {
                    this.cboOutboundContextes.Items.Add(ctx);
                }
            }
        }

        void EnableCallActions(bool bEnabled)
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)txtPhoneNumber, "Enabled", bEnabled });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)cboOutboundContextes, "Enabled", bEnabled });
                Invoke(new SetPropertyDelegate(SetProperty), new object[] { (object)btnDial, "Enabled", bEnabled });

            }
            else
            {
                this.txtPhoneNumber.Enabled = bEnabled;
                this.cboOutboundContextes.Enabled = bEnabled;
                this.btnDial.Enabled = bEnabled;
            }
        }

        private bool CanEnableDialButton()
        {
            return ((this.txtPhoneNumber.Text.Length > 0) &
                       (this.cboOutboundContextes.SelectedItem != null));
        }

        #region Localization Action

        public static ResourceManager RM
        {
            get
            {
                return rm;
            }
        }

        private void GlobalizeApp()
        {
            SetCulture();
            SetResource();
            SetUIChanges();
        }

        private void SetCulture()
        {
            if (optset.Language != "")
                strCulture = optset.Language;

            CultureInfo objCI = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;

        }
        private void SetResource()
        {
            rm = ResourceManager.CreateFileBasedResourceManager
                ("lang", strResourcesPath, null);
        }

        private void SetUIChanges()
        {

            this.toolTip1.SetToolTip(this.btnConfig, frmMain.RM.GetString("0001"));
            this.toolTip1.SetToolTip(this.btnHide, frmMain.RM.GetString("0002"));
            this.toolTip1.SetToolTip(this.txtPhoneNumber, frmMain.RM.GetString("0003"));
            this.toolTip1.SetToolTip(this.btnDial, frmMain.RM.GetString("0004"));
            this.toolTip1.SetToolTip(this.cboOutboundContextes , frmMain.RM.GetString("0005"));
            this.toolTip1.SetToolTip(this.btnQuit, frmMain.RM.GetString("0007"));
            this.toolTip1.SetToolTip(this.btnStartStop, frmMain.RM.GetString("0010"));

            this.lblLineState.Text = frmMain.RM.GetString("0100");
            this.btnQuit.Text = frmMain.RM.GetString("0006");
            this.btnStartStop.Text = frmMain.RM.GetString("0008");

            this.lblUserName.Text = frmMain.RM.GetString("0011");
            this.lblSecret.Text = frmMain.RM.GetString("0012");
            this.mnuShowHide.Text  = frmMain.RM.GetString("0013");

        }

        #endregion

        
    }
}