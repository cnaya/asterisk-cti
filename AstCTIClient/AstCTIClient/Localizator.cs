using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.ComponentModel;

namespace AstCTIClient
{
    class Localizator
    {
        private string strResourcesPath = Application.StartupPath + Path.DirectorySeparatorChar + "lang";
        private string strCulture = "en-US";
        private ResourceManager rm;

        public Localizator()
            : this("")
        {
        }

        public Localizator(string culture)
        {
            if (culture.Length > 0)
            {
                this.strCulture = culture;
            }

            this.rm = ResourceManager.CreateFileBasedResourceManager
                ("lang", strResourcesPath, null);
            this.UpdateCulture();
        }

        public string ResourcePath
        {
            get { return this.strResourcesPath; }
        }

        public string Culture
        {
            get { return this.strCulture; }
            set
            {
                this.strCulture = value;
                this.UpdateCulture();
            }
        }

        public string this[string code]
        {
            get
            {
                return this.rm.GetString(code);
            }
        }

        public ResourceManager RM
        {
            get
            {
                return rm;
            }
        }

        private void UpdateCulture()
        {
            CultureInfo objCI = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;

        }

        public void Localize(Form f)
        {
            this.Localize(f,"");
        }
        
        public void Localize(Form f, string textCode)
        {
            if (textCode.Length > 0)
            {
                f.Text = this[textCode];
            }

            if (f.Menu != null)
            {
                RecursiveLocalizeMenu((Menu)f.Menu);
            }

            if (f.ContextMenu != null)
            {
                RecursiveLocalizeMenu((Menu)f.ContextMenu);
            }

            foreach (Control ctl in f.Controls)
            {
                RecursiveLocalize(ctl);
            }
            
        }

        public void Localize(Control ctl)
        {
            this.RecursiveLocalize(ctl);
        }

        public void Localize(Menu m)
        {
            this.RecursiveLocalizeMenu(m);
        }

        private void RecursiveLocalizeMenu(Menu m)
        {
            
            foreach(MenuItem mi in m.MenuItems)
            {
                
                string code = (string)mi.Tag;
                if (code != "")
                {
                    mi.Text= this[code];
                }
            }
        }

        private void RecursiveLocalize(Control cctl)
        {
            if (cctl.ContextMenu != null)
            {
                RecursiveLocalizeMenu((Menu)cctl.ContextMenu);
            }

            if (cctl.Tag != null)
            {
                string code = (string)cctl.Tag;
                if (code != "")
                {
                    cctl.Text = this[code];
                }
            }
            if (cctl.HasChildren)
            {
                foreach (Control scctl in cctl.Controls)
                {
                    RecursiveLocalize(scctl);
                }
            }
            
        }

    }


}
