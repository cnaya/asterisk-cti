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
using System.ComponentModel.Design;
using System.Collections.Specialized;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing.Design;
using SettingsManager;

namespace AstCTIClient
{
    [TypeConverter(typeof(CTIOutboundContextConverter))]
    public class CTIOutboundContext
    {
        private string context = "";
        private int priority = 1;
        
        private string displayname = "";
        
        public string Context
        {
            get { return this.context; }
            set
            {
                this.context = value;
                this.displayname = string.Format("Outbound Context {0}", this.context);
            }
        }

        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public string DisplayName
        {
            get { return this.displayname; }
            set { this.displayname = value; }
        }
        public override int GetHashCode()
        {
            return this.context.GetHashCode();
        }
        public override string ToString()
        {
            return this.context;
        }
    }

    public class CTIOutboundContextCollection : CollectionBase, ICustomTypeDescriptor
    {
        #region collection impl

        /// <summary>
        /// Adds an employee object to the collection
        /// </summary>
        /// <param name="emp"></param>
        public void Add(CTIOutboundContext cti)
        {
            if (this.List.Contains(cti))
            {
                return;
            }
            this.List.Add(cti);
        }

        /// <summary>
        /// Removes an employee object from the collection
        /// </summary>
        /// <param name="emp"></param>
        public void Remove(CTIOutboundContext cti)
        {
            this.List.Remove(cti);
        }

        /// <summary>
        /// Returns an employee object at index position.
        /// </summary>
        public CTIOutboundContext this[int index]
        {
            get
            {
                return (CTIOutboundContext)this.List[index];
            }
        }

        #endregion

        // Implementation of interface ICustomTypeDescriptor 
        #region ICustomTypeDescriptor impl

        public String GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public String GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }


        /// <summary>
        /// Called to get the properties of this type. Returns properties with certain
        /// attributes. this restriction is not implemented here.
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        /// <summary>
        /// Called to get the properties of this type.
        /// </summary>
        /// <returns></returns>
        public PropertyDescriptorCollection GetProperties()
        {
            // Create a collection object to hold property descriptors
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);

            // Iterate the list of employees
            for (int i = 0; i < this.List.Count; i++)
            {
                // Create a property descriptor for the employee item and add to the property descriptor collection
                CTIOutboundContextCollectionPropertyDescriptor pd = new CTIOutboundContextCollectionPropertyDescriptor(this, i);
                pds.Add(pd);
            }
            // return the property descriptor collection
            return pds;
        }

        #endregion
    }

    public class CTIOutboundContextCollectionPropertyDescriptor : PropertyDescriptor
    {
        private CTIOutboundContextCollection collection = null;
        private int index = -1;

        public CTIOutboundContextCollectionPropertyDescriptor(CTIOutboundContextCollection coll, int idx)
            : base("#" + idx.ToString(), null)
        {
            this.collection = coll;
            this.index = idx;
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return this.collection.GetType();
            }
        }

        public override string DisplayName
        {
            get
            {
                CTIOutboundContext cti = this.collection[index];
                return cti.DisplayName;
            }
        }

        public override object GetValue(object component)
        {
            return this.collection[index];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override string Name
        {
            get { return "#" + index.ToString(); }
        }

        public override Type PropertyType
        {
            get { return this.collection[index].GetType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void SetValue(object component, object value)
        {
            // this.collection[index] = value;
        }
    }

    internal class CTIOutboundContextConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is CTIOutboundContext)
            {
                // Cast the value to an Employee type
                CTIOutboundContext cti = (CTIOutboundContext)value;

                return cti.Context;
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }

    internal class CTIOutboundContextCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destType)
        {
            if (destType == typeof(string) && value is CTIOutboundContextCollection)
            {
                return "CTI Outbound Contextes";
            }
            return base.ConvertTo(context, culture, value, destType);
        }
    }
}
