using System;
using System.Collections.Generic;
using System.Text;

namespace SubrightEditor.Nodes.Variables
{
    public class String : BaseObject
    {
        public string value;

        public String() : base("String") { }

        public void SetValue(string setvalue)
        {
            value = setvalue;
        }
    }
}
