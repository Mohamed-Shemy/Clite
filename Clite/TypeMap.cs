
using System;
using System.Collections.Generic;

namespace Clite
{


	public class TypeMap : Dictionary<Variable, Type>
	{
		public virtual string Display()
		{
            string tmp = "";
			foreach (Variable v in this.Keys)
                tmp += "    " + v.ToString() + " :: " + this[v] + "\n";
            return tmp;
		}
	}

}