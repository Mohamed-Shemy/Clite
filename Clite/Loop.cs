using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Loop : IStatement
    {
        private IExpression test;
        private IStatement body;
        public Loop(IExpression test, IStatement body)
        {
            this.test = test;
            this.body = body;
        }

        public IExpression Test() { return test; }
        public IStatement Body() { return body; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|While: ";
            tmp += test.Display(indent);
            tmp += body.Display(indent) + "\n";
            return tmp;
        }
    }
}
