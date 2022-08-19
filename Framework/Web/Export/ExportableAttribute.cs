using KO.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace KO.Framework.Web.Exportacion
{
#pragma warning disable CA1018 // Mark attributes with AttributeUsageAttribute
    public class ExportableAttribute : Attribute

    {
        public ExportableAttribute()
        {

        }
        public ExportableAttribute(string GlobalResourceName)
        {
            this.GlobalResourceName = GlobalResourceName;
            ResourceManager MyResourceClass = new(typeof(Global));
            ColumnName = MyResourceClass.GetString(this.GlobalResourceName);
        }


        public string ColumnName { get; set; }
        private string GlobalResourceName { get; set; }
        public bool SkipDrawing { get; set; }
        public bool IsComposedHeader { get; set; }

        public string[] ChildHeaders { get; set; }
    }
}
