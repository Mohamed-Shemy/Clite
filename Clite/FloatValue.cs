using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class FloatValue : Value
    {
        private float value = 0;

        public FloatValue() : base()
        {
            type = Clite.Type.FLOAT;
        }

        public FloatValue(float v) : this()
        {
            value = v;
            undefined = false;
        }

        public override float DFloatValue()
        {
            return value;
        }

        public override string ToString()
        {
            if (undefined)
                return "undef";
            return value + " (Float)";
        }

        public override string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += ("   ");
            tmp += ("| " + value + " (Float)\n");
            return tmp;
        }
    }
}
