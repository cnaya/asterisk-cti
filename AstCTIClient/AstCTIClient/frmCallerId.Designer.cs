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
    partial class frmCallerId
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
            this.lblNewCallFrom = new System.Windows.Forms.Label();
            this.lblCallerId = new System.Windows.Forms.Label();
            this.lblCallerName = new System.Windows.Forms.Label();
            this.lnkLabelClose = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblNewCallFrom
            // 
            this.lblNewCallFrom.AutoSize = true;
            this.lblNewCallFrom.BackColor = System.Drawing.Color.Transparent;
            this.lblNewCallFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewCallFrom.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblNewCallFrom.Location = new System.Drawing.Point(12, 30);
            this.lblNewCallFrom.Name = "lblNewCallFrom";
            this.lblNewCallFrom.Size = new System.Drawing.Size(154, 24);
            this.lblNewCallFrom.TabIndex = 0;
            this.lblNewCallFrom.Tag = "0200";
            this.lblNewCallFrom.Text = "New Call From:";
            // 
            // lblCallerId
            // 
            this.lblCallerId.AutoSize = true;
            this.lblCallerId.BackColor = System.Drawing.Color.Transparent;
            this.lblCallerId.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCallerId.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblCallerId.Location = new System.Drawing.Point(172, 30);
            this.lblCallerId.Name = "lblCallerId";
            this.lblCallerId.Size = new System.Drawing.Size(21, 24);
            this.lblCallerId.TabIndex = 1;
            this.lblCallerId.Text = "0";
            // 
            // lblCallerName
            // 
            this.lblCallerName.AutoSize = true;
            this.lblCallerName.BackColor = System.Drawing.Color.Transparent;
            this.lblCallerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCallerName.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.lblCallerName.Location = new System.Drawing.Point(172, 64);
            this.lblCallerName.Name = "lblCallerName";
            this.lblCallerName.Size = new System.Drawing.Size(21, 24);
            this.lblCallerName.TabIndex = 2;
            this.lblCallerName.Text = "0";
            // 
            // lnkLabelClose
            // 
            this.lnkLabelClose.AutoSize = true;
            this.lnkLabelClose.BackColor = System.Drawing.Color.Transparent;
            this.lnkLabelClose.LinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkLabelClose.Location = new System.Drawing.Point(379, 109);
            this.lnkLabelClose.Name = "lnkLabelClose";
            this.lnkLabelClose.Size = new System.Drawing.Size(75, 13);
            this.lnkLabelClose.TabIndex = 5;
            this.lnkLabelClose.TabStop = true;
            this.lnkLabelClose.Tag = "0201";
            this.lnkLabelClose.Text = "Click To Close";
            this.lnkLabelClose.VisitedLinkColor = System.Drawing.Color.MidnightBlue;
            this.lnkLabelClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLabelClose_LinkClicked);
            // 
            // frmCallerId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AstCTIClient.Properties.Resources.callerid;
            this.ClientSize = new System.Drawing.Size(480, 140);
            this.Controls.Add(this.lnkLabelClose);
            this.Controls.Add(this.lblCallerName);
            this.Controls.Add(this.lblCallerId);
            this.Controls.Add(this.lblNewCallFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmCallerId";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCallerId";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNewCallFrom;
        private System.Windows.Forms.Label lblCallerId;
        private System.Windows.Forms.Label lblCallerName;
        private System.Windows.Forms.LinkLabel lnkLabelClose;
    }
}