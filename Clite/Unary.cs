using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Unary : IExpression
    {
        private Operator op;
        public IExpression term;

        public Unary(Operator op, IExpression term)
        {
            this.op = op;
            this.term = term;
        }

        public Operator Operator() { return op; }

        public IExpression Term() { return term; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|Unary Operator: ";
            for (int i = 0; i < indent + 1; i++)
                tmp += "   ";
            tmp += "|" + op.val;
            tmp += term.Display(indent + 1) + "\n";
            return tmp;
        }
    }
}
