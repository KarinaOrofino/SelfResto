using KO.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace KO.Framework.Web.Exportacion
{
    public class ExportableAttribute : Attribute
    {
        public ExportableAttribute()
        {

        }
        public ExportableAttribute(string GlobalResourceName, bool SkipDrawing = false)
        {
            this.GlobalResourceName = GlobalResourceName;
            ResourceManager MyResourceClass = new ResourceManager(typeof(Global));
            ColumnName = MyResourceClass.GetString(this.GlobalResourceName);
        }


        public string ColumnName { get; set; }
        private string GlobalResourceName { get; set; }
        public bool SkipDrawing { get; set; }
        public bool IsComposedHeader { get; set; }

        public string[] ChildHeaders { get; set; }
    }
}
