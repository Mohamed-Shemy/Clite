using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Declarations : Dictionary<string, Declaration>
    {
        public string Display(int indent)
        {
            string tmp = "";
            foreach (Declaration d in this.Values)
            {
                for (int i = 0; i < indent; i++)
                    tmp += "   ";
                tmp += (d.variable() + " :: " + d.type() + "\n");
            }
            return tmp;
        }
    }
}
