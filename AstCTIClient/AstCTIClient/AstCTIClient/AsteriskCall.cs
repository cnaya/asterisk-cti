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
using System.Xml;

namespace AstCTIClient
{
    /// <summary>
    /// This is an helper class to convert Server XML Messages into AsteriskCall object
    /// </summary>
    public class AsteriskCall
    {
        public string Channel = null;
        public string ParsedChannel = null;
        public string CallerIDNum = null;
        public string CallerIDName = null;
        public string Uniqueid = null;
        public string Context = null;
        public string AppData = null;
        public string State = null;
        
        /// <summary>
        /// This method extracts the channel for the current Asterisk Channel string
        /// </summary>
        public void ParseDestination()
        {
            if (this.Channel.Contains("-"))
                this.ParsedChannel = this.Channel.Substring(0, this.Channel.IndexOf("-"));

        }


        public override string ToString()
        {
            string call = "";
            if (this.Uniqueid != null) call += this.Uniqueid;
            call += " {\n";
            if (this.Channel != null) call += "\tChannel     : " + this.Channel + "\n";
            if (this.CallerIDNum != null) call += "\tCallerIDNum : " + this.CallerIDNum + "\n";
            if (this.CallerIDName != null) call += "\tCallerIDName: " + this.CallerIDName + "\n";
            if (this.Uniqueid != null) call += "\tUniqueid    : " + this.Uniqueid + "\n";
            if (this.Context != null) call += "\tContext     : " + this.Context + "\n";
            if (this.AppData != null) call += "\tAppData     : " + this.AppData + "\n";
            if (this.State != null) call += "\tState     : " + this.State + "\n";
            call += "}\n";
            return call;
        }

        /// <summary>
        /// Static method that maps XML object to AsteriskCall Object
        /// </summary>
        /// <param name="data">Data to parse</param>
        /// <param name="eventId">Outputs the eventId</param>
        /// <returns>An AsteriskCall object if parse is successfull. Otherwise null</returns>
        public static AsteriskCall CallFromXml(string data, out string eventId)
        {
            AsteriskCall xmlcall = null;
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = data;
            XmlElement root = doc.DocumentElement;
            eventId = "";
            if (root.Name.ToLower().Equals("event"))
            {
                eventId = root.GetAttribute("id");
                XmlNode call = root.ChildNodes[0];
                xmlcall = new AsteriskCall();
                foreach (XmlElement prop in call.ChildNodes)
                {
                    if (prop.Name.Equals("uniqueid")) xmlcall.Uniqueid = prop.InnerText;
                    if (prop.Name.Equals("channel")) xmlcall.Channel = prop.InnerText;
                    if (prop.Name.Equals("calleridnum")) xmlcall.CallerIDNum = prop.InnerText;
                    if (prop.Name.Equals("calleridname")) xmlcall.CallerIDName = prop.InnerText;
                    if (prop.Name.Equals("context")) xmlcall.Context = prop.InnerText;
                    if (prop.Name.Equals("appdata")) xmlcall.AppData = prop.InnerText;                    
                    if (prop.Name.Equals("state")) xmlcall.State = prop.InnerText; 

                }
            }
            return xmlcall;
        }
    }
}
