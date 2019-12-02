using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class BoolValue : Value
    {
        private bool value = false;

        public BoolValue() : base()
        {
            type = Clite.Type.BOOL;
        }
        public BoolValue(bool v) : this()
        {
            value = v;
            undefined = false;
        }

        public override bool DBoolValue()
        {
            return value;
        }

        public override int DIntValue()
        {
            return value ? 1 : 0;
        }

        public override string ToString()
        {
            if (undefined)
                return "undef";
            return value + " (Bool)";
        }

        public override string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += ("| " + value + " (Bool)\n");
            return tmp;
        }
    }
}
