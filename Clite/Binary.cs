using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Binary : IExpression
    {
        private Operator op;
        private IExpression term1, term2;

        public Binary(Operator op, IExpression term1, IExpression term2)
        {
            this.op = op;
            this.term1 = term1;
            this.term2 = term2;
        }


        public Operator Operator() { return op; }
        public IExpression Term1() { return term1; }
        public IExpression Term2() { return term2; }

        public string Display(int indent)
        {
            string tmp = "";
            tmp += term1.Display(indent + 1) + "\n";
            for (int i = 0; i < indent + 1; i++)
                tmp += "   ";
            tmp += "| " + op.val + "\n";
            tmp += term2.Display(indent + 1);
            return tmp;
        }
    }
}
