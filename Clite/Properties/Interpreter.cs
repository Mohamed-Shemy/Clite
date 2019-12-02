using System.Collections.Generic;

namespace Clite
{

	public class Interpreter
	{
		public static State Interpret(IProgram p)
		{
			return interpret(p.functions().get("main").body(), p.functions(), initialState(p.globals()));
		}

		public static State initialState(Declarations d)
		{
			State state = new State();
			foreach (Declaration decl in d.values())
				state.put(decl.variable(), Value.mkValue(decl.type()));
			return state;
		}

		public static State interpret(Statement s, Functions funcs, State state)
		{
			if (s is Skip)
				return state;
			if (s is Assignment)
				return interpret((Assignment) s, funcs, state);
			if (s is Conditional)
				return interpret((Conditional) s, funcs, state);
			if (s is Loop)
				return interpret((Loop) s, funcs, state);
			if (s is Block)
				return interpret((Block) s, funcs, state);
			if (s is Call)
				return interpretCallStatement((Call)s, funcs, state);
			throw new System.ArgumentException("should never reach here");
		}
		public static State interpret(Assignment a, Functions funcs, State state)
		{
			return state.onion(a.target(), interpret(a.source(), funcs, state));
		}
		public static State interpret(Block b, Functions funcs, State state)
		{
			IEnumerator<Statement> members = b.Members;
			while (members.MoveNext())
				state = interpret(members.Current, funcs, state);
			return state;
		}

		public static State interpret(Conditional c, Functions funcs, State state)
		{
			if (interpret(c.test(), funcs, state).boolValue())
				return interpret(c.thenBranch(), funcs, state);
			else
				return interpret(c.elseBranch(), funcs, state);
		}
		public static State interpret(Loop l, Functions funcs, State state)
		{
			if (interpret(l.test(), funcs, state).boolValue())
				return interpret(l, funcs, interpret(l.body(), funcs, state));
			else
				return state;
		}
		public static Value interpret(Expression e, Functions funcs, State state)
		{
			if (e is Value)
				return (Value) e;
			if (e is Variable)
				return (Value)(state.get(e));
			if (e is Binary)
				return interpret((Binary) e, funcs, state);
			if (e is Unary)
				return interpret((Unary) e, funcs, state);
			if (e is Call)
				return interpretCallExpression((Call)e, funcs, state);
			throw new System.ArgumentException("should never reach here");
		}
		{
			Function f = funcs.get(c.identifier().ToString());
			foreach (Declaration decl in f.locals().values())
				state.put(decl.variable(), Value.mkValue(decl.type()));
			IEnumerator<Expression> argIt = c.arguments();
			IEnumerator<Declaration> funcIt = f.@params().values().GetEnumerator();
			while (argIt.MoveNext())
			{
				Expression exp = argIt.Current;
				Declaration dec = funcIt.next();
				Value v = interpret(exp, funcs, state);
				state.put(dec.variable(), v);
			}

			IEnumerator<Statement> members = f.body().Members;
			while (members.MoveNext())
			{
				Statement s = members.Current;
				if (s is Return)
				{
					foreach (Declaration decl in f.locals().values())
						state.remove(decl.variable());
					foreach (Declaration decl in f.@params().values())
						state.remove(decl.variable());
					return state;
				} 
				else
					state = interpret(s, funcs, state);
			}
			foreach (Declaration decl in f.locals().values())
				state.remove(decl.variable());
			foreach (Declaration decl in f.@params().values())
				state.remove(decl.variable());
			return state;
		}
		public static Value interpretCallExpression(Call c, Functions funcs, State state)
		{
			Function f = funcs.get(c.identifier().ToString());
			foreach (Declaration decl in f.locals().values())
				state.put(decl.variable(), Value.mkValue(decl.type()));
			IEnumerator<Expression> argIt = c.arguments();
			IEnumerator<Declaration> funcIt = f.@params().values().GetEnumerator();
			while (argIt.MoveNext())
			{
				Expression exp = argIt.Current;
				Declaration dec = funcIt.next();
				Value v = interpret(exp, funcs, state);
				state.put(dec.variable(), v);
			}

			IEnumerator<Statement> members = f.body().Members;
			while (members.MoveNext())
			{
				Statement s = members.Current;
				if (s is Return)
				{
					Value v = interpret(((Return)s).result(), funcs, state);
					foreach (Declaration decl in f.locals().values())
						state.remove(decl.variable());
					foreach (Declaration decl in f.@params().values())
						state.remove(decl.variable());
					return v;
				}
				else if (hasReturn(s))
				{
					if (s is Conditional && isSkipped((Conditional)s, funcs, state))
						continue;
					else
					{
						Value v = interpretWithReturn(s, funcs, state);
						foreach (Declaration decl in f.locals().values())
							state.remove(decl.variable());
						foreach (Declaration decl in f.@params().values())
							state.remove(decl.variable());
						return v;
					}
				}
				else
					state = interpret(s, funcs, state);
			}
			throw new System.ArgumentException("attemped to interpret function call with no return as expression");
		}
		public static Value interpretWithReturn(Statement s, Functions funcs, State state)
		{
			state.display();
			if (s is Conditional)
			{
				Conditional c = (Conditional)s;
				Statement chosen;
				if (interpret(c.test(), funcs, state).boolValue())
					chosen = c.thenBranch();
				else
					chosen = c.elseBranch();
				if (chosen is Return)
					return interpret(((Return)chosen).result(), funcs, state);
				else
					return interpretWithReturn(chosen, funcs, state);
			}

			if (s is Loop)
			{
				Loop l = (Loop)s;
				if (interpret(l.test(), funcs, state).boolValue())
					return interpretWithReturn(l, funcs, interpret(l.body(), funcs, state));
			}

			if (s is Block)
			{
				Block b = (Block) s;
				IEnumerator<Statement> it = b.Members;
				while (it.MoveNext())
				{
					Statement bs = it.Current;
					if (bs is Skip)
						continue;
					if (bs is Return)
						return interpret(((Return)s).result(), funcs, state);
					else
						state = interpret(s, funcs, state);
				}
			}

			if (s is Return)
				return interpret(((Return)s).result(), funcs, state);
			throw new System.ArgumentException("tried to interpret statement with return when it didn't have a return");
		}
		public static bool hasReturn(Statement s)
		{
			if (s is Skip || s is Assignment)
				return false;
			if (s is Conditional)
				Conditional c = (Conditional)s;
				return hasReturn(c.thenBranch()) || hasReturn(c.elseBranch());
			if (s is Loop)
				return hasReturn(((Loop)s).body());
			if (s is Block)
			{
				Block b = (Block) s;
				IEnumerator<Statement> it = b.Members;
				while (it.MoveNext())
					if (hasReturn(it.Current))
						return true;
				return false;
			}
			if (s is Return)
				return true;
			throw new System.ArgumentException("should never reach here");
		}
		public static bool isSkipped(Conditional c, Functions funcs, State state)
		{
			Statement chosen;
			if (interpret(c.test(), funcs, state).boolValue())
				chosen = c.thenBranch();
			else
				chosen = c.elseBranch();
			return chosen is Skip;
		}
		public static Value interpret(Unary u, Functions funcs, State state)
		{
			Operator op = u.@operator();
			Value v = interpret(u.term, funcs, state);
			StaticTypeCheck.check(!v.undefined(), "reference to undef value in unary op");
			if (op.val.Equals(Operator.NOT))
			{
				if (v.type() != Type.BOOL)
					throw new System.ArgumentException("Can only apply ! operator to bool (attempted on " + v + ")");
				else
					return new BoolValue(!v.boolValue());
			}

			else if (op.val.Equals(Operator.NEG))
			{
				if (v.type() == Type.FLOAT)
					return new FloatValue(-v.floatValue());
				else if (v.type() == Type.INT)
					return new IntValue(-v.intValue());
				else
					throw new System.ArgumentException("Can only apply - operator to int or float (attempted on " + v + ")");
			}
			else if (op.val.Equals(Operator.FLOAT))
			{
				if (v.type() != Type.INT)
					throw new System.ArgumentException("Can only cast int to float (tried to cast " + v + ")");
				else
					return new FloatValue((float)v.intValue());
			}
			else if (op.val.Equals(Operator.INT))
			{
				if (v.type() == Type.FLOAT)
					return new IntValue((int)v.floatValue());
				else if (v.type() == Type.CHAR)
					return new IntValue((int)v.charValue());
				else
					throw new System.ArgumentException("Can only cast float or char to int (tried to cast " + v + ")");
			}
			else if (op.val.Equals(Operator.CHAR))
			{
				if (v.type() == Type.INT)
					return new CharValue((char)v.intValue());
				else
					throw new System.ArgumentException("Can only cast int to char (tried to cast " + v + ")");
			}
			throw new System.ArgumentException("should never reach here");
		}
		public static Value interpret(Binary b, Functions funcs, State state)
		{
			Operator op = b.@operator();
			Value v1 = interpret(b.term1(), funcs, state);
			Value v2 = interpret(b.term2(), funcs, state);
			StaticTypeCheck.check(!v1.undefined() || !v2.undefined(), "reference to undef value in binary op");
			if (op.ArithmeticOp)
			{
				if (v1.type() == Type.INT && v1.type() == Type.INT)
				{
					if (op.val.Equals(Operator.PLUS))
						return new IntValue(v1.intValue() + v2.intValue());
					if (op.val.Equals(Operator.MINUS))
						return new IntValue(v1.intValue() - v2.intValue());
					if (op.val.Equals(Operator.TIMES))
						return new IntValue(v1.intValue() * v2.intValue());
					if (op.val.Equals(Operator.DIV))
						return new IntValue(v1.intValue() / v2.intValue());
				}
				else if (v1.type() == Type.FLOAT && v2.type() == Type.FLOAT)
				{
					if (op.val.Equals(Operator.PLUS))
						return new FloatValue(v1.floatValue() + v2.floatValue());
					if (op.val.Equals(Operator.MINUS))
						return new FloatValue(v1.floatValue() - v2.floatValue());
					if (op.val.Equals(Operator.TIMES))
						return new FloatValue(v1.floatValue() * v2.floatValue());
					if (op.val.Equals(Operator.DIV))
						return new FloatValue(v1.floatValue() / v2.floatValue());
				}
				else if ((v1.type() == Type.INT && v2.type() == Type.FLOAT) || (v1.type() == Type.FLOAT && v2.type() == Type.INT))
				{
					if (v1.type() == Type.INT)
						v1 = new FloatValue((float)v1.intValue());
					else if (v2.type() == Type.INT)
						v2 = new FloatValue((float)v2.intValue());
					return interpret(new Binary(op, v1, v2), funcs, state);
				}
				else
				{
					throw new System.ArgumentException("Attemped arithmetic op on a " + v1.type() + " and a " + v2.type() + ", not allowed (v1: " + v1 + " v2: " + v2 + ")");
				}

			}
			else if (op.BooleanOp)
			{
				if (!(v1.type() == Type.BOOL && v1.type() == Type.BOOL))
					throw new System.ArgumentException("Attemped boolean op on " + v1.type() + ", not allowed");
				else
				{
					if (op.val.Equals(Operator.AND))
						return new BoolValue(v1.boolValue() && v2.boolValue());
					else if (op.val.Equals(Operator.OR))
						return new BoolValue(v1.boolValue() || v2.boolValue());
				}

			}
			else if (op.RelationalOp)
			{
				if (v1.type() == Type.INT && v1.type() == Type.INT)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.intValue() < v2.intValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.intValue() > v2.intValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.intValue() <= v2.intValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.intValue() >= v2.intValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.intValue() == v2.intValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.intValue() != v2.intValue());
				}
				else if (v1.type() == Type.FLOAT && v1.type() == Type.FLOAT)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.floatValue() < v2.floatValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.floatValue() > v2.floatValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.floatValue() <= v2.floatValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.floatValue() >= v2.floatValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.floatValue() == v2.floatValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.floatValue() != v2.floatValue());
				}
				else if ((v1.type() == Type.INT && v2.type() == Type.FLOAT) || (v1.type() == Type.FLOAT && v2.type() == Type.INT))
				{
					if (v1.type() == Type.INT)
						v1 = new FloatValue((float)v1.intValue());
					else if (v2.type() == Type.INT)
						v2 = new FloatValue((float)v2.intValue());
					return interpret(new Binary(op, v1, v2), funcs, state);
				}
				else if (v1.type() == Type.CHAR && v2.type() == Type.CHAR)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.charValue() < v2.charValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.charValue() > v2.charValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.charValue() <= v2.charValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.charValue() >= v2.charValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.charValue() == v2.charValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.charValue() != v2.charValue());
				}
				else if (v1.type() == Type.BOOL && v2.type() == Type.BOOL)
				{
					if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.boolValue() == v2.boolValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.boolValue() != v2.boolValue());
					else
						throw new System.ArgumentException("Attempted illegal relational op " + op + " on two booleans (v1: " + v1 + " v2: " + v2 + ")");
				}
				else
				{
					throw new System.ArgumentException("Attemped relational op on a " + v1.type() + " and a " + v2.type() + ", not allowed (v1: " + v1 + " v2: " + v2 + ")");
				}
			}
			throw new System.ArgumentException("should never reach here (in DynamicTyping.applyBinary)");
		}
	}

}