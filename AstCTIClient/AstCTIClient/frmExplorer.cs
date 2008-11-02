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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.Runtime.InteropServices;
using SettingsManager;

namespace AstCTIClient
{
    
    public partial class frmExplorer : Form
    {
        private SHDocVw.WebBrowserClass ie_events;
        
        private LocalAppSettings optset;
        private Localizator localizator;

        public delegate void OnNewWindow(Form f);
        public event OnNewWindow NewWindow;
        private ArrayList childs;

        #region Constructors
        public frmExplorer(AppSettings appsettings)
        {
            // Thread.CurrentThread.SetApartmentState(ApartmentState.STA); 
            InitializeComponent();
            this.childs = new ArrayList();
            this.optset = (LocalAppSettings)appsettings;
            UpdateInterfaceFromConfg();
            
            BindExplorerEvents();

            this.localizator = new Localizator();
            this.localizator.Culture = this.optset.Language;
            this.localizator.Localize(this);
            this.UpdateComboWidth();
            this.BringToFront();
        }

        public frmExplorer(AppSettings appsettings, string url) : this(appsettings)
        {
            this.BeginNavigate(url);
            
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.optset.CloseChildsOnClose)
            {
                if (childs.Count > 0)
                {
                    Form[] forms = (Form[])childs.ToArray(typeof(System.Windows.Forms.Form));
                    foreach (Form f in forms)
                    {
                        if (f != null) f.Close();
                    }

                }
            }
        }
        #endregion

        #region Window Events
        
        private void toolStrip1_SizeChanged(object sender, EventArgs e)
        {
            UpdateComboWidth();
        }

        private void UpdateInterfaceFromConfg()
        {
            this.cboAddressList.Enabled = this.optset.CanInsertUrls;
            this.toolStrip1.Visible = this.optset.ShowAddressBar;
            this.btnGo.Visible = this.optset.ShowGoButton;
            this.statusStrip1.Visible = this.optset.ShowStatusBar;
            UpdateComboWidth();

        }

        private void UpdateComboWidth()
        {
            int btnGoWidth = (this.optset.ShowGoButton) ? this.btnGo.Width : 0;
            this.cboAddressList.Width = toolStrip1.ClientRectangle.Width - btnGoWidth - 10;
        }
        #endregion

        #region Explorer Events
        private void BindExplorerEvents()
        {
            /* 
             * Many thanks to Stephane Rodriguez for giving the article at this URL:
             * http://www.codeproject.com/KB/bugs/iefix.aspx
             */
            ie_events = (SHDocVw.WebBrowserClass)
                    Marshal.CreateWrapperOfType(
                        axWebBrowser1.GetOcx(),
                        typeof(SHDocVw.WebBrowserClass)
                    );

            ie_events.NewWindow2 += new SHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(ie_events_NewWindow2);
            ie_events.ProgressChange += new SHDocVw.DWebBrowserEvents2_ProgressChangeEventHandler(ie_events_ProgressChange);
            ie_events.DocumentComplete += new SHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(ie_events_DocumentComplete);
            ie_events.NavigateComplete += new SHDocVw.DWebBrowserEvents_NavigateCompleteEventHandler(ie_events_NavigateComplete);
            ie_events.BeforeNavigate += new SHDocVw.DWebBrowserEvents_BeforeNavigateEventHandler(ie_events_BeforeNavigate);
            ie_events.TitleChange += new SHDocVw.DWebBrowserEvents2_TitleChangeEventHandler(ie_events_TitleChange);
            ie_events.StatusTextChange += new SHDocVw.DWebBrowserEvents2_StatusTextChangeEventHandler(ie_events_StatusTextChange);
        }

        void ie_events_BeforeNavigate(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Cancel)
        {
            this.cboAddressList.Text = URL;
        }

        void ie_events_StatusTextChange(string Text)
        {
            this.tsStatusLabel.Text = Text;
        }

        void ie_events_TitleChange(string Text)
        {
            this.Text = Text + " - AstCTI Explorer";
        }

        void ie_events_NavigateComplete(string URL)
        {
            this.tsProgressBar.Value = 0;
        }

        void ie_events_DocumentComplete(object pDisp, ref object URL)
        {
            this.tsProgressBar.Value = 0;
        }

        void ie_events_ProgressChange(int Progress, int ProgressMax)
        {
            if (Progress == -1)
            {
                this.tsProgressBar.Value = 0;
            }
            else
            {
                this.tsProgressBar.Value = (Progress * 100) / ProgressMax;
            }           
        }      

        void ie_events_NewWindow2(ref object ppDisp, ref bool Cancel)
        {
            frmExplorer frmExp;
            frmExp = new frmExplorer(this.optset);
            if (this.NewWindow != null) this.NewWindow(frmExp);

            ppDisp = frmExp.axWebBrowser1.Application;
            this.childs.Add(frmExp);
            frmExp.Show();
        }

        #endregion
               
        #region Navigation Events
        private void btnGo_Click(object sender, EventArgs e)
        {
            this.BeginNavigate(cboAddressList.Text);
        }

        private void cboAddressList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e == null || e.KeyCode == Keys.Enter)
            {
                if (!this.optset.ShowGoButton) return;
                this.BeginNavigate(cboAddressList.Text);
            }
        }
        private void BeginNavigate(string szUrl)
        {
            object obj = null;
            object url = (object)szUrl;
            axWebBrowser1.Navigate2(ref url, ref obj, ref obj, ref obj, ref obj);
        }
        #endregion

        
    }
}
