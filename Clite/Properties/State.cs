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
			this[variable] = val;
		}
		public virtual State Onion(Variable variable, Value val)
		{
			this[variable] = val;
			return this;
		}
		public virtual State Onion(State other)
		{
			foreach (Variable key in other.Keys)
			{
				this[key] = other[key];
			}
			return this;
		}
		public virtual void Display()
		{
			foreach (Variable v in this.Keys)
			{
				Console.WriteLine(v + ": " + this[v]);
			}
		}
	}
}