

using System;
using System.Collections.Generic;

namespace Clite
{

	public class State : Dictionary<Variable, Value>
	{
		public State() : base()
		{
		}
		public State(Variable variable, Value val) : base()
		{
            this.Add(variable, val);
        }
		public virtual State Onion(Variable variable, Value val)
		{
            if (this.ContainsKey(variable))
                this[variable] = val;
            else
                this.Add(variable, val);
			return this;
		}
		public virtual State Onion(State other)
		{
            foreach (Variable key in other.Keys)
                this.Add(key, other[key]);
			return this;
		}
		public virtual string Display()
		{
            string tmp = "";
			foreach (Variable v in this.Keys)
				tmp += v + "= " + this[v] + "\n";
            return tmp;
		}
	}

}