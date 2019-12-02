using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Function : IStatement
    {
        private Type _type;
        private String _id;
        private Declarations _param, _locals;
        private Block _body;

        public Function(Type t, string i, Declarations p, Declarations l, Block b)
        {
            this._type = t;
            this._id = i;
            this._param = p;
            this._locals = l;
            this._body = b;
        }

        public Type type() { return _type; }
        public string Id() { return _id; }
        public Declarations Param() { return _param; }
        public Declarations Locals() { return _locals; }
        public Block Body() { return _body; }

        public string Display(int indent)
        {
            string tmp = "";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += _id + " :: " + _type + "\n";
            indent++;
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "Parameters:\n";
            tmp += _param.Display(indent + 1) + "\n";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "Locals:\n";
            tmp += _locals.Display(indent + 1) + "\n";
            for (int i = 0; i < indent; i++)
                tmp += "   ";
            tmp += "Body:\n";
            tmp += _body.Display(indent) + "\n";
            return tmp;
        }
    }
}

