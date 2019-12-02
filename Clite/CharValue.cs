using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class CharValue : Value
    {
        private char value = '\0';
        public CharValue() : base()
        {
            type = Clite.Type.CHAR;
        }
        public CharValue(char v) : this()
        {
            value = v;
            undefined = false;
        }

        public override char DCharValue()
        {
            return value;
        }

        public override string ToString()
        {
            if (undefined)
                return "undef";
            return value + " (Char)";
        }

        public override string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += ("| " + value + " (Char)\n");
            return tmp;
        }
    }
}
