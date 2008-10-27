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
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using System.Drawing;
using SettingsManager;

namespace AstCTIClient
{
    public class LocalAppSettings : AppSettings
    {
        private string cti_host = "localhost";
        private int cti_port = 9000;
        private int cti_timeout = 1500;

        private string cti_username = "";
        private string cti_password = "";
        private string matchextension = "";

        private string mysql_host = "localhost";
        private string mysql_user = "root";
        private string mysql_pass = "";
        private string mysql_dbms = "test";
        private int mysql_port = 3306;

        CTIContextCollection cticontextes = new CTIContextCollection();
        CTIOutboundContextCollection ctioutboundcontextes = new CTIOutboundContextCollection();

        private bool cti_trigger_callerid = false;
        private int cti_callerid_timeout = 3;
        private int cti_callerid_speed = 10;

        private string language = "en-US";

        private bool minimize_on_start = false;

        private bool bIntBrowserShowStatusBar = false;
        private bool bIntBrowserShowAddressBar = false;
        private bool bIntBrowserShowGoButton = false;
        private bool bIntBrowserCanInsertUrls = false;
        private bool bIntBrowserCloseChilds = false;
        // private bool bIntBrowserDisableContextMenu = false;
        // private bool bIntBrowserDisablePopups = false;

        private Font fntInterface = new Font("Microsoft Sans Serif", 8);

        [Category("INTERNAL BROWSER"), Description("Toggle the visibility of the status bar")]
        public bool ShowStatusBar
        {
            get { return bIntBrowserShowStatusBar; }
            set { this.bIntBrowserShowStatusBar = value; } 
        }

        [Category("INTERNAL BROWSER"), Description("Toggle the visibility of the address bar")]
        public bool ShowAddressBar
        {
            get { return bIntBrowserShowAddressBar; }
            set { this.bIntBrowserShowAddressBar = value; } 
        }

        [Category("INTERNAL BROWSER"), Description("Toggle the visibility of the GO button")]
        public bool ShowGoButton
        {
            get { return bIntBrowserShowGoButton; }
            set { this.bIntBrowserShowGoButton = value; }
        }

        [Category("INTERNAL BROWSER"), Description("Toggle the possibility of insert new urls in the address bar")]
        public bool CanInsertUrls
        {
            get { return bIntBrowserCanInsertUrls; }
            set { this.bIntBrowserCanInsertUrls = value; }
        }

        [Category("INTERNAL BROWSER"), Description("Close child forms on window close")]
        public bool CloseChildsOnClose
        {
            get { return bIntBrowserCloseChilds; }
            set { this.bIntBrowserCloseChilds = value; }
        }
/*
        [Category("INTERNAL BROWSER"), Description("Toggle the browser context menu")]
        public bool DisableContextMenu
        {
            get { return bIntBrowserDisableContextMenu; }
            set { this.bIntBrowserDisableContextMenu = value; }
        }

        [Category("INTERNAL BROWSER"), Description("Toggle the possibility of popups")]
        public bool DisablePopups
        {
            get { return bIntBrowserDisablePopups; }
            set { this.bIntBrowserDisablePopups = value; }
        }
*/
        [Category("CTI APPLICATION"), Description("CTI Contextes")]
        [TypeConverter(typeof(CTIContextCollectionConverter))]
        public CTIContextCollection CTIContextes
        {
            get { return cticontextes; }
        }

        [Category("CTI APPLICATION"), Description("CTI Outbound Contextes")]
        [TypeConverter(typeof(CTIOutboundContextCollectionConverter))]
        public CTIOutboundContextCollection CTIOutboundContextes
        {
            get { return ctioutboundcontextes; }
        }

        [Category("CTI APPLICATION"), Description("CTI Trigger CallerId")]
        public bool TriggerCallerId
        {
            get { return this.cti_trigger_callerid; }
            set { this.cti_trigger_callerid = value; }
        }

        [Category("CTI APPLICATION"), Description("CTI PopUp Timeout in seconds")]
        public int CallerIdTimeout
        {
            get { return this.cti_callerid_timeout; }
            set { this.cti_callerid_timeout = value; }
        }

        [Category("CTI APPLICATION"), Description("CTI PopUp FadeOut Speed")]
        public int CalleridFadeSpeed
        {
            get { return this.cti_callerid_speed; }
            set { this.cti_callerid_speed = value; }
        }

        [Category("CTI SERVER"), Description("Asterisk Extension to match")]
        public string PhoneExt
        {
            get { return this.matchextension; }
            set { this.matchextension = value; }
        }

        [Category("CTI SERVER"), Description("CTI Server TCP/IP Address")]
        public string Host
        {
            get { return this.cti_host; }
            set { this.cti_host = value; }
        }

        [Category("CTI SERVER"), Description("CTI Server TCP/IP Port")]
        public int Port
        {
            get { return this.cti_port; }
            set { this.cti_port = value; }
        }

        [Category("CTI SERVER"), Description("CTI Socket Timeout in milliseconds")]
        public int SocketTimeout
        {
            get { return this.cti_timeout; }
            set { this.cti_timeout = value; }
        }

        [Category("CTI SERVER"), Description("Username to authenticate on CTI Server")]
        public string Username
        {
            get { return this.cti_username; }
            set { this.cti_username = value; }
        }
        [Category("CTI SERVER"), Description("Password to authenticate user on CTI Server")]
        [PasswordPropertyText(true)]
        public string Password
        {
            get { return this.cti_password; }
            set { this.cti_password = value; }
        }

        [Category("DATABASE"), Description("MySQL Server Hostname or IP")]
        public string MySQLHost
        {
            get { return this.mysql_host; }
            set { this.mysql_host = value; }
        }
        [Category("DATABASE"), Description("MySQL Username")]
        public string MySQLUserName
        {
            get { return this.mysql_user; }
            set { this.mysql_user = value; }
        }
        [Category("DATABASE"), Description("MySQL Password")]
        [PasswordPropertyText(true)]
        public string MySQLPassword
        {
            get { return this.mysql_pass; }
            set { this.mysql_pass = value; }
        }
        [Category("DATABASE"), Description("MySQL Database")]
        public string MySQLDatabase
        {
            get { return this.mysql_dbms; }
            set { this.mysql_dbms = value; }
        }

        [Category("DATABASE"), Description("MySQL Port")]
        public int MySQLPort
        {
            get { return this.mysql_port; }
            set { this.mysql_port = value; }
        }

        [Category("INTERFACE"), Description( "Language")]
        public string Language
        {
            get { return this.language; }
            set { this.language = value; }
        }

        [Category("INTERFACE"), Description("Font")]
        public Font InterfaceFont
        {
            get { return this.fntInterface ; }
            set { this.fntInterface = value; }
        }

        

        [Category("INTERFACE"), Description("Application Gui")]
        public bool MinimizeOnStart
        {
            get { return this.minimize_on_start; }
            set { this.minimize_on_start = value; }
        }        
    }
}
