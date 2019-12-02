using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class Return : IStatement
    {
        private IExpression result;

        private Variable functionName;

        public Return(Variable functionName, IExpression result)
        {
            this.result = result;
            this.functionName = functionName;
        }

        public IExpression Result() { return result; }

        public Variable FunctionName() { return functionName; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "| return ";
            tmp += result.Display(indent + 1) + "\n";
            return tmp;
        }
    }
}
