using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Clite
{
    public abstract class Value : IExpression
    {
        protected Type type;
        protected bool undefined;

        public Value()
        {
            undefined = true;
        }

        virtual public int DIntValue()
        {
            return 0;
        }

        virtual public bool DBoolValue()
        {
            return false;
        }

        virtual public char DCharValue()
        {
            return ' ';
        }

        virtual public float DFloatValue()
        {
            return 0.0f;
        }

        virtual public bool Undefined() { return undefined; }

        public Type Type() { return type; }

        public static Value MkValue(Type type)
        {
            if (type == Clite.Type.INT)
                return new IntValue();
            if (type == Clite.Type.BOOL)
                return new BoolValue();
            if (type == Clite.Type.CHAR)
                return new CharValue();
            if (type == Clite.Type.FLOAT)
                return new FloatValue();
            throw new IException("Illegal type in MkValue","","Syntax: Value",0,0);
        }

        virtual public string Display(int indent)
        {
            return indent.ToString();
        }
    }
}
