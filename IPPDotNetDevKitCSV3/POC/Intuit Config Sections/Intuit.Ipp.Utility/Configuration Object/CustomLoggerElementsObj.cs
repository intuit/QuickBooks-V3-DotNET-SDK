using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.Utility
{
    public class CustomLoggerElementsObj
    {
        private string name;
        private string type;
        private bool enable;

        public CustomLoggerElementsObj()
        {
            this.name = string.Empty;
            this.type = string.Empty;
            this.enable = false;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        

        public bool Enable
        {
            get { return enable; }
            set { enable = value; }
        }
    }
}
