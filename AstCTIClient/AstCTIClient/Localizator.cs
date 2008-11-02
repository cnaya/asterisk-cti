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
        #region Private Vars
        private string strResourcesPath = Application.StartupPath + Path.DirectorySeparatorChar + "lang";
        private string strCulture = "en-US";
        private ResourceManager rm;
        #endregion

        #region Events
        public delegate void OnLocalizationCompleted(Object obj);
        public event OnLocalizationCompleted LocalizationCompleted;
        #endregion

        #region Constructors
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
        #endregion

        #region Public Properties
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
                string res = "";
                try
                {
                    res = this.rm.GetString(code);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Missing string for code: " + code + " in locale: " + this.strCulture);
                    return "";
                }
                return res;
            }
        }

        public ResourceManager RM
        {
            get
            {
                return rm;
            }
        }
        #endregion
        
        #region Form
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
                String ctl_type = ctl.GetType().ToString();
                if (ctl_type.Equals("System.Windows.Forms.ToolStrip"))
                {
                    LocalizeToolStrip((ToolStrip)ctl);
                }
                else
                {
                    RecursiveLocalize(ctl);
                }
            }
            if (this.LocalizationCompleted != null) this.LocalizationCompleted(f);
        }
        #endregion

        #region Control
        public void Localize(Control ctl)
        {
            this.RecursiveLocalize(ctl);
            if (this.LocalizationCompleted != null) this.LocalizationCompleted(ctl);

        }
        #endregion

        #region Menu
        public void Localize(Menu m)
        {
            this.RecursiveLocalizeMenu(m);
            if (this.LocalizationCompleted != null) this.LocalizationCompleted(m);
        }
        #endregion

        #region Private Methods
        private void UpdateCulture()
        {
            CultureInfo objCI = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;

        }

        private void LocalizeToolStrip(ToolStrip tsp)
        {
            foreach (ToolStripItem ti in tsp.Items)
            {
                if (ti.Tag != null)
                {
                    string code = (string)ti.Tag;
                    if (code != "")
                    {
                        ti.Text = this[code];
                    }
                }
            }
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
            string ctl_type = cctl.GetType().ToString();
            Console.WriteLine(ctl_type);

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
        #endregion
    }


}
