using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class IntValue : Value
    {
        private int value = 0;
        public IntValue() : base()
        {
            type = Clite.Type.INT;
        }

        public IntValue(int v) : this()
        {
            value = v;
            undefined = false;
        }
        public override int DIntValue()
        {
            return value;
        }
        public override string ToString()
        {
            if (undefined)
                return "undef";
            return value + " (Int)";
        }

        public override string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "| " + value + " (Integar)\n";
            return tmp;
        }
    }
}
