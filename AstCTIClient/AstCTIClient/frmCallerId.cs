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
using System.Threading;
using SettingsManager;
using System.Resources;
using System.Globalization;


namespace AstCTIClient
{
    public partial class frmCallerId : FadingForm
    {
        private System.Windows.Forms.Timer myTimer;
        
        private LocalAppSettings optset;
        private Localizator localizator;

        public frmCallerId(AppSettings appsettings, string callerid)
        {
            InitializeComponent();
            this.optset = (LocalAppSettings)appsettings;
            Stream s = this.GetType().Assembly.GetManifestResourceStream("AstCTIClient.callerid.gif");
            Bitmap bmp = new Bitmap(s);
            this.Region = BitmapToRegion.Convert(bmp, bmp.GetPixel(0, 0), TransparencyMode.ColorKeyTransparent);
            this.Click += new EventHandler(frmCallerId_Click);
            this.lblCallerId.Text = callerid;
            this.lblCallerName.Text = "";
            this.Speed = 10;
            this.MouseEnter += new EventHandler(frmCallerId_MouseEnter);
            this.MouseLeave += new EventHandler(frmCallerId_MouseLeave);
            this.localizator = new Localizator();
            this.localizator.Culture = this.optset.Language;
            this.localizator.Localize(this);
        }

        

        void frmCallerId_MouseLeave(object sender, EventArgs e)
        {
            this.myTimer.Enabled = true;
        }

        void frmCallerId_MouseEnter(object sender, EventArgs e)
        {
            this.myTimer.Enabled = false;
        }
        public void StartTimer(int duration)
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = duration * 1000;
            myTimer.Tick += new EventHandler(myTimer_Tick);
            myTimer.Enabled = true;
        }
        void myTimer_Tick(object sender, EventArgs e)
        {          
            this.Dispose();
        }

        void frmCallerId_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void lnkLabelClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Dispose();
        }

        
    }
}