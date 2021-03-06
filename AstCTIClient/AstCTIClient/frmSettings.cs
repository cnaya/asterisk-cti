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
using System.IO;
using System.Resources;
using System.Globalization;
using System.Threading;
using SettingsManager;
using System.Collections;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace AstCTIClient
{
    public partial class frmSettings : Form
    {
        private LocalAppSettings optset;
        private LocalAppSettings initial_optset;
        private int startupLanguageIdx = 0;
        private static ResourceManager rm;
        private Localizator localizator;
        private InOutContext inoutcontextes;
        private InOutContext initial_inoutcontextes;

        private Hashtable locales;

        public frmSettings(LocalAppSettings appsettings)
        {
            this.optset = appsettings;
            
            InitializeComponent();
            this.txtCtiServerPort.LostFocus += new EventHandler(CheckNumericAndDefault);
            this.txtCtiServerTimeout.LostFocus += new EventHandler(CheckNumericAndDefault);
            this.txtCalleridTimeout.LostFocus += new EventHandler(CheckNumericAndDefault);
            this.txtCalleridFadeoutSpeed.LostFocus += new EventHandler(CheckNumericAndDefault);
            this.txtMysqlPort.LostFocus += new EventHandler(CheckNumericAndDefault);
            this.localizator = new Localizator();
            this.localizator.Culture = this.optset.Language;
            this.localizator.Localize(this);
            LoadLocales();
            LoadLanguageDirectory(this.localizator.ResourcePath);
            SetFont();
            
            
            this.SetAppSettings();
            
            
            this.cbLanguage.SelectedValueChanged += new EventHandler(cbLanguage_SelectedValueChanged);
        }


        void LoadLocales()
        {
            this.locales = new Hashtable();
            FileInfo locales_file = new FileInfo(Application.StartupPath + Path.DirectorySeparatorChar + "locales.txt");
            if (locales_file.Exists)
            {
                StreamReader lstream = locales_file.OpenText();
                string locale_line = "";
                while(!lstream.EndOfStream)
                {
                    try
                    {
                        locale_line = lstream.ReadLine().Trim();
                        if ((locale_line.Length > 0) & (locale_line.Contains(":")))
                        {
                            string[] locale_data = locale_line.Trim().Split(':');
                            locales.Add(locale_data[0], locale_data[1]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Error loading locales:\n"+ex.Message, "Error", MessageBoxButtons.OK);
                    }
                }
                lstream.Close();
            }
        }


        void CheckNumericAndDefault(object sender, EventArgs e)
        {
            string ctlname = (((TextBox)sender).Name);
            string ctltxt = (((TextBox)sender).Text);
            int outnum = 0;
            bool bparsed = int.TryParse(ctltxt, out outnum);
            switch (ctlname)
            {
                case "txtCtiServerPort":
                    ((TextBox)sender).Text = bparsed ? outnum.ToString() : "8999";
                    break;
                case "txtCtiServerTimeout":
                    ((TextBox)sender).Text = bparsed ? outnum.ToString() : "1500";
                    break;
                case "txtCalleridTimeout":
                    ((TextBox)sender).Text = bparsed ? outnum.ToString() : "3";
                    break;
                case "txtCalleridFadeoutSpeed":
                    ((TextBox)sender).Text = bparsed ? outnum.ToString() : "10";
                    break;
                case "txtMysqlPort":
                    ((TextBox)sender).Text = bparsed ? outnum.ToString() : "3306";
                    break;
                    
            }
        }

        void SetAppSettings()
        {
            this.txtCtiServerHostname.Text = optset.Host;
            this.txtCtiServerPort.Text = optset.Port.ToString();
            this.txtCtiServerTimeout.Text = optset.SocketTimeout.ToString();

            this.txtClientExten.Text = optset.PhoneExt;
            this.txtClientUsername.Text = optset.Username;
            this.txtClientPassword.Text = optset.Password;
            this.cboCalleridShow.Checked = optset.TriggerCallerId;
            this.txtCalleridTimeout.Text = optset.CallerIdTimeout.ToString();
            this.txtCalleridFadeoutSpeed.Text = optset.CalleridFadeSpeed.ToString();

            this.txtMysqlHost.Text = optset.MySQLHost;
            this.txtMysqlUser.Text = optset.MySQLUserName;
            this.txtMysqlDatabase.Text = optset.MySQLDatabase;
            this.txtMysqlPassword.Text = optset.MySQLPassword;
            this.txtMysqlPort.Text = this.optset.MySQLPort.ToString();
            this.chkIBAddrBar.Checked = this.optset.ShowAddressBar;
            this.chkIBAllowUrls.Checked = this.optset.CanInsertUrls;
            this.chkIBCloseChilds.Checked = this.optset.CloseChildsOnClose;
            this.chkIBShowGo.Checked = this.optset.ShowGoButton;
            this.chkIBStatusBar.Checked = this.optset.ShowStatusBar;
            
            this.chkMinimizeOnStart.Checked = this.optset.MinimizeOnStart;
            this.chkStartWithWindows.Checked = false;
            this.txtUIFont.Text = this.optset.InterfaceFont.ToString();

            this.inoutcontextes = new InOutContext();
            this.initial_inoutcontextes = new InOutContext();
            initial_inoutcontextes.Inbound = (CTIContextCollection)optset.CTIContextes.Clone();
            initial_inoutcontextes.Outbound = (CTIOutboundContextCollection)optset.CTIOutboundContextes.Clone();

            inoutcontextes.Inbound = (CTIContextCollection)optset.CTIContextes;
            inoutcontextes.Outbound = (CTIOutboundContextCollection)optset.CTIOutboundContextes;

            

            this.propInbound.SelectedObject = inoutcontextes;
            this.propInbound.ExpandAllGridItems();
        }

            

        void GetAppSettings()
        {
            int tmp = 0;
            optset.Host = this.txtCtiServerHostname.Text;
            int.TryParse(this.txtCtiServerPort.Text, out tmp);
            optset.Port = tmp;

            tmp = 0;
            int.TryParse(this.txtCtiServerTimeout.Text, out tmp);
            optset.SocketTimeout = tmp;

            optset.PhoneExt = this.txtClientExten.Text;
            optset.Username = this.txtClientUsername.Text;
            optset.Password =this.txtClientPassword.Text;
            optset.TriggerCallerId = this.cboCalleridShow.Checked;
            tmp = 0;
            int.TryParse(this.txtCalleridTimeout.Text, out tmp);
            optset.CallerIdTimeout = tmp;

            tmp = 0;
            int.TryParse(this.txtCalleridFadeoutSpeed.Text, out tmp);
            optset.CalleridFadeSpeed = tmp;

            optset.MySQLHost = this.txtMysqlHost.Text;
            optset.MySQLDatabase = this.txtMysqlDatabase.Text;
            optset.MySQLUserName = this.txtMysqlUser.Text;
            optset.MySQLPassword = this.txtMysqlPassword.Text;
            tmp = 0;
            int.TryParse(this.txtMysqlPort.Text, out tmp);
            this.optset.MySQLPort = tmp;

            this.optset.ShowAddressBar = this.chkIBAddrBar.Checked;
            this.optset.CanInsertUrls = this.chkIBAllowUrls.Checked;
            this.optset.CloseChildsOnClose = this.chkIBCloseChilds.Checked;
            this.optset.ShowGoButton = this.chkIBShowGo.Checked;
            this.optset.ShowStatusBar = this.chkIBStatusBar.Checked;

            this.optset.MinimizeOnStart = this.chkMinimizeOnStart.Checked;

            
        }

        public LocalAppSettings Settings
        {
            get { return this.optset; }
        }

        void cbLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            ImageComboItem itm = (ImageComboItem)this.cbLanguage.SelectedItem;
            LocaleItem lang = (LocaleItem)itm.Tag;
           
            optset.Language = lang.LocaleValue;
            this.localizator.Culture = optset.Language;
            this.localizator.Localize(this);

        }

        

        private void LoadLanguageDirectory(string path)
        {
            int i;
            cbLanguage.Items.Clear();

            if (!Directory.Exists(path)) return;

            string[] langFiles = Directory.GetFiles(path, "*.resources");
            if (langFiles.Length <= 0)
                return;

            for (i = 0; i < langFiles.Length; i++)
            {
                FileInfo finfo = new FileInfo(langFiles[i]);
                string args = finfo.Name.Replace(finfo.Extension, "");
                args = args.Replace("lang.", "");
                
                string locale_name = (string)locales[args];
                LocaleItem itm = new LocaleItem(locale_name, args);
                ImageComboItem cbitm = new ImageComboItem(itm.LocaleName, LocaleImageIndex(args));
                cbitm.Tag = itm;
                int pos = cbLanguage.Items.Add(cbitm);
                

            }

            for (i = 0; i < cbLanguage.Items.Count; i++)
            {
                ImageComboItem itm =(ImageComboItem)cbLanguage.Items[i]; 
                LocaleItem litm = (LocaleItem)itm.Tag;
                if ( litm.LocaleValue == optset.Language)
                {
                    cbLanguage.SelectedIndex = i;
                    startupLanguageIdx = i;
                    break;
                }

            }
        }

        private int LocaleImageIndex(string locale)
        {
            string[] locales = locale.Split('-');

            return imageList1.Images.IndexOfKey(locales[1].ToLower() + ".png");
        }

        

        void SetFont()
        {
            Font f = this.optset.InterfaceFont;
            foreach (Control ctl in this.Controls)
            {
                ctl.Font = f;
                RecursiveSetFont(ctl, f);
            }
        }

        void RecursiveSetFont(Control ctl, Font f)
        {
            foreach (Control cctl in ctl.Controls)
            {
                cctl.Font = f;

                if (cctl.HasChildren)
                {
                    foreach (Control scctl in cctl.Controls)
                    {
                        RecursiveSetFont(scctl, f);
                    }
                }
            }
        }

        private void btnUIFont_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = this.optset.InterfaceFont;
            fontDialog1.ShowApply = false;
            DialogResult res = fontDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                this.optset.InterfaceFont = fontDialog1.Font;
                this.txtUIFont.Text = this.optset.InterfaceFont.ToString();
                SetFont();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.optset.CTIContextes.Clear();
            this.optset.CTIOutboundContextes.Clear();

            foreach (CTIContext ctx in this.initial_inoutcontextes.Inbound)
            {
                this.optset.CTIContextes.Add(ctx);
            }

            foreach (CTIOutboundContext octx in this.initial_inoutcontextes.Outbound)
            {
                this.optset.CTIOutboundContextes.Add(octx);
            }


            this.DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.GetAppSettings();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void btnDbTest_Click(object sender, EventArgs e)
        {
            string connStr = "Persist Security Info=False;" +
                     "database=" + this.txtMysqlDatabase.Text+ ";" +
                     "server=" + this.txtMysqlHost.Text + ";" +
                     "Port=" + this.txtMysqlPort.Text + ";" +
                     "user id=" + this.txtMysqlUser.Text + ";" +
                     "Password=" + this.txtMysqlPassword.Text + ";" +
                     "Compress=false";
            
            
            MySqlConnection cn = new MySqlConnection(connStr);
            try
            {
                cn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show(this, this.localizator["8037"], this.localizator["8036"], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class LocaleItem
    {
        private string localeName;
        private string localeValue;

        public LocaleItem(string locName, string locValue)
        {
            this.localeName = locName;
            this.localeValue = locValue;
        }

        public string LocaleName
        {
            get { return this.localeName; }
        }

        public string LocaleValue
        {
            get { return this.localeValue; }
        }

        public override int GetHashCode()
        {
            return localeValue.GetHashCode();
        }
        public override string  ToString()
        {
             return localeName.ToString();
        }
    }

    public class InOutContext
    {
        private CTIContextCollection context;
        private CTIOutboundContextCollection outcontext;

        [Category("Inbound"), Description("Inbound")]
        [TypeConverter(typeof(CTIContextCollectionConverter))]
        public CTIContextCollection Inbound
        {
            get { return this.context; }
            set { this.context = value; }
        }

        [Category("Inbound"), Description("Outbound")]
        [TypeConverter(typeof(CTIOutboundContextCollectionConverter))]
        public CTIOutboundContextCollection Outbound
        {
            get { return this.outcontext; }
            set { this.outcontext = value; }
        }
    }
}