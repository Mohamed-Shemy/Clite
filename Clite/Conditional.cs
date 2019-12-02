using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Conditional : IStatement
    {
        private IExpression test;
        private IStatement thenBranch, elseBranch;

        public Conditional(IExpression test, IStatement thenBranch) : this(test, thenBranch, null)
        {

        }

        public Conditional(IExpression test, IStatement thenBranch, IStatement elseBranch)
        {
            this.test = test;
            this.thenBranch = thenBranch;

            if (elseBranch == null)
                elseBranch = new Skip();
            else
                this.elseBranch = elseBranch;
        }

        public IExpression Test() { return test; }
        public IStatement ThenBranch() { return thenBranch; }
        public IStatement ElseBranch() { return elseBranch; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|Conditional: ";

            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|Test: ";

            tmp += test.Display(indent + 1) + "\n";

            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "|Then: ";

            tmp += thenBranch.Display(indent + 1) + "\n";

            if (!(elseBranch.GetType() == typeof(Skip)))
            {
                for (int i = 0; i < indent; i++)
                    tmp += "   ";
                tmp += "|Else: ";

                tmp += elseBranch.Display(indent + 1) + "\n";
            }
            return tmp;
        }
    }
}
