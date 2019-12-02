using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class Skip : IStatement
    {
        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += '\t';
           tmp += "(Skip)\n";
            return tmp;
        }
    }
}
