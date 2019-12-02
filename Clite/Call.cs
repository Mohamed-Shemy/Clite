using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Call : IStatement, IExpression
    {
        private Variable _identifier;
        private Stack<IExpression> _arguments;

        public Call(Variable id, Stack<IExpression> arguments)
        {
            this._identifier = id;
            this._arguments = arguments;
        }

        public Variable Identifier() { return _identifier; }

        public IEnumerator<IExpression> Arguments() { return _arguments.GetEnumerator(); }
       
        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
           tmp += "\n| call " + _identifier.ToString() + "(";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
           tmp += "| \n(";
            foreach (IExpression e in _arguments)
                tmp += e.Display(indent + 1) + "\n";
            for (int i = 0; i < indent; i++)
               tmp += "   ";
           tmp += "| )";
            return tmp;
        }
    }
}
