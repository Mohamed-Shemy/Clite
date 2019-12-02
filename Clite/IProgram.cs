using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class IProgram
    {
        private Declarations globals;
        private Functions _functions;
        public IProgram(Declarations globals, Functions _functions)
        {
            this.globals = globals;
            this._functions = _functions;
        }
        public Declarations Globals() { return globals; }
        public Functions functions() { return _functions; }

        public string Display()
        {
            string tmp = "";
            tmp += "Globals:\n";
            foreach (Declaration d in globals.Values)
                tmp += "\t" + d.variable() + " :: " + d.type() + "\n";

           tmp += "Functions:\n";
            IEnumerator<Function> members = _functions.Values.GetEnumerator();
            while (members.MoveNext())
               tmp +=  members.Current.Display(1) + "\n";
            return tmp;
        }
    }
}
