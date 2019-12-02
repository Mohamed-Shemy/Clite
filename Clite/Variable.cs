using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Variable : IExpression
    {
        private string id;
        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                 tmp += "   ";
            tmp += ("| " + id) + "\n";
            return tmp;
        }
        
        public Variable(string id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return id;
        }

        public override bool Equals(object obj)
        {
            String s = ((Variable)obj).id;
            return id.Equals(s);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
