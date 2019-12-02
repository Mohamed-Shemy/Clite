using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Assignment : IStatement
    {
        private Variable target;
        private IExpression source;
        public Assignment(Variable target, IExpression source)
        {
            this.target = target;
            this.source = source;
        }

        public Variable Target() { return target; }

        public IExpression Source() { return source; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|" + target.ToString() + " = ";
            tmp += source.Display(indent + 1) + "\n";
            return tmp;
        }
    }
}
