using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Declaration
    {
        private Variable var;
        private Type ttype;
        public Declaration(Variable var, Type _type)
        {
            this.var = var;
            this.ttype = _type;
        }
        public Variable variable() { return var; }

        public Type type() { return ttype; }
    }
}
