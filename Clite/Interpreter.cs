using System.Collections.Generic;

namespace Clite
{
	public class Interpreter
	{
		public static State Interpret(IProgram p)
		{
			return Interpret(p.functions()["main"].Body(), p.functions(), InitialState(p.Globals()));
		}
		public static State InitialState(Declarations d)
		{
			State state = new State();
			foreach (Declaration decl in d.Values)
				state.Add(decl.variable(), Value.MkValue(decl.type()));
			return state;
		}
		public static State Interpret(IStatement s, Functions funcs, State state)
		{
			if (s is Skip)
				return state;
			if (s is Assignment)
				return Interpret((Assignment) s, funcs, state);
			if (s is Conditional)
				return Interpret((Conditional) s, funcs, state);
			if (s is Loop)
				return Interpret((Loop) s, funcs, state);
			if (s is Block)
				return Interpret((Block) s, funcs, state);
			if (s is Call)
				return InterpretCallStatement((Call)s, funcs, state);
			throw new IException("Semantic Error", "", "Semantic", 0, 0);
        }
		public static State Interpret(Assignment a, Functions funcs, State state)
		{
			return state.Onion(a.Target(), Interpret(a.Source(), funcs, state));
		}
		public static State Interpret(Block b, Functions funcs, State state)
		{
			IEnumerator<IStatement> members = b.GetMembers();
			while (members.MoveNext())
				state = Interpret(members.Current, funcs, state);
			return state;
		}
		public static State Interpret(Conditional c, Functions funcs, State state)
		{
			if (Interpret(c.Test(), funcs, state).DBoolValue())
				return Interpret(c.ThenBranch(), funcs, state);
			else
				return Interpret(c.ElseBranch(), funcs, state);
		}
		public static State Interpret(Loop l, Functions funcs, State state)
		{
			if (Interpret(l.Test(), funcs, state).DBoolValue())
				return Interpret(l, funcs, Interpret(l.Body(), funcs, state));
			else
				return state;
		}
		public static Value Interpret(IExpression e, Functions funcs, State state)
		{
			if (e is Value)
				return (Value) e;
            if (e is Variable)
                return (Value)(state[(Variable)e]);
			if (e is Binary)
				return Interpret((Binary) e, funcs, state);
			if (e is Unary)
				return Interpret((Unary) e, funcs, state);
			if (e is Call)
				return InterpretCallExpression((Call)e, funcs, state);
			throw new IException("Semantic Error", "", "Semantic", 0, 0);
        }
		public static State InterpretCallStatement(Call c, Functions funcs, State state)
		{
			Function f = funcs[c.Identifier().ToString()];
			foreach (Declaration decl in f.Locals().Values)
				state.Add(decl.variable(), Value.MkValue(decl.type()));
			IEnumerator<IExpression> argIt = c.Arguments();
			IEnumerator<Declaration> funcIt = f.Param().Values.GetEnumerator();
			while (argIt.MoveNext())
			{
				IExpression exp = argIt.Current;
				Declaration dec = funcIt.Current;
				Value v = Interpret(exp, funcs, state);
				state.Add(dec.variable(), v);
			}
			IEnumerator<IStatement> members = f.Body().GetMembers();
			while (members.MoveNext())
			{
				IStatement s = members.Current;
				if (s is Return)
				{
					foreach (Declaration decl in f.Locals().Values)
						state.Remove(decl.variable());
					foreach (Declaration decl in f.Param().Values)
						state.Remove(decl.variable());
					return state;
				}
				else
					state = Interpret(s, funcs, state);
			}
			foreach (Declaration decl in f.Locals().Values)
				state.Remove(decl.variable());
			foreach (Declaration decl in f.Param().Values)
				state.Remove(decl.variable());
			return state;
		}
		public static Value InterpretCallExpression(Call c, Functions funcs, State state)
		{
			Function f = funcs[c.Identifier().ToString()];
			foreach (Declaration decl in f.Locals().Values)
				state.Add(decl.variable(), Value.MkValue(decl.type()));

			IEnumerator<IExpression> argIt = c.Arguments();
			IEnumerator<Declaration> funcIt = f.Param().Values.GetEnumerator();
			while (argIt.MoveNext())
			{
                funcIt.MoveNext();
				IExpression exp = argIt.Current;
				Declaration dec = funcIt.Current;
				Value v = Interpret(exp, funcs, state);
				state.Add(dec.variable(), v);
			}
			IEnumerator<IStatement> members = f.Body().GetMembers();
			while (members.MoveNext())
			{
				IStatement s = members.Current;
				if (s is Return)
				{
					Value v = Interpret(((Return)s).Result(), funcs, state);
					foreach (Declaration decl in f.Locals().Values)
						state.Remove(decl.variable());
					foreach (Declaration decl in f.Param().Values)
						state.Remove(decl.variable());
					return v;
				}
				else if (HasReturn(s))
				{
					if (s is Conditional && IsSkipped((Conditional)s, funcs, state))
						continue;
					else
					{
						Value v = InterpretWithReturn(s, funcs, state);
						foreach (Declaration decl in f.Locals().Values)
							state.Remove(decl.variable());
						foreach (Declaration decl in f.Param().Values)
							state.Remove(decl.variable());
						return v;
					}
				}
				else
					state = Interpret(s, funcs, state);
			}
			throw new IException("attemped to Interpret function call with no return as expression", "", "Semantic", 0, 0);
        }
		public static Value InterpretWithReturn(IStatement s, Functions funcs, State state)
		{
			state.Display();
			if (s is Conditional)
			{
				Conditional c = (Conditional)s;
				IStatement chosen;
				if (Interpret(c.Test(), funcs, state).DBoolValue())
					chosen = c.ThenBranch();
				else
					chosen = c.ElseBranch();
				if (chosen is Return)
					return Interpret(((Return)chosen).Result(), funcs, state);
				else
					return InterpretWithReturn(chosen, funcs, state);
			}
			if (s is Loop)
			{
				Loop l = (Loop)s;
				if (Interpret(l.Test(), funcs, state).DBoolValue())
					return InterpretWithReturn(l, funcs, Interpret(l.Body(), funcs, state));
			}
			if (s is Block)
			{
				Block b = (Block) s;
				IEnumerator<IStatement> it = b.GetMembers();
				while (it.MoveNext())
				{
					IStatement bs = it.Current;
					if (bs is Skip)
						continue;
					if (bs is Return)
						return Interpret(((Return)s).Result(), funcs, state);
					else
						state = Interpret(s, funcs, state);
				}
			}
			if (s is Return)
				return Interpret(((Return)s).Result(), funcs, state);
			throw new IException("tried to Interpret statement with return when it didn't have a return", "", "Semantic", 0, 0);
        }
		public static bool HasReturn(IStatement s)
		{
			if (s is Skip || s is Assignment)
				return false;
            if (s is Conditional)
            {
                Conditional c = (Conditional)s;
                return HasReturn(c.ThenBranch()) || HasReturn(c.ElseBranch());
            }
			if (s is Loop)
				return HasReturn(((Loop)s).Body());
			if (s is Block)
			{
				Block b = (Block) s;
				IEnumerator<IStatement> it = b.GetMembers();
				while (it.MoveNext())
					if (HasReturn(it.Current))
						return true;
				return false;
			}
			if (s is Return)
				return true;
			throw new IException("Semantic Error", "", "Semantic", 0, 0);
        }
		public static bool IsSkipped(Conditional c, Functions funcs, State state)
		{
			IStatement chosen;
			if (Interpret(c.Test(), funcs, state).DBoolValue())
				chosen = c.ThenBranch();
			else
				chosen = c.ElseBranch();
			return chosen is Skip;
		}
		public static Value Interpret(Unary u, Functions funcs, State state)
		{
			Operator op = u.Operator();
			Value v = Interpret(u.term, funcs, state);
			StaticTypeCheck.Check(!v.Undefined(), "reference to undef value in unary op");
			if (op.val.Equals(Operator.NOT))
			{
				if (v.Type() != Type.BOOL)
					throw new IException("Can only apply ! operator to bool (attempted on " + v + ")", "", "Semantic", 0, 0);
                else
					return new BoolValue(!v.DBoolValue());
			}
			else if (op.val.Equals(Operator.NEG))
			{
				if (v.Type() == Type.FLOAT)
					return new FloatValue(-v.DFloatValue());
				else if (v.Type() == Type.INT)
					return new IntValue(-v.DIntValue());
				else
					throw new IException("Can only apply - operator to int or float (attempted on " + v + ")", "", "Semantic", 0, 0);
            }
			else if (op.val.Equals(Operator.FLOAT))
			{
				if (v.Type() != Type.INT)
					throw new IException("Can only cast int to float (tried to cast " + v + ")", "", "Semantic", 0, 0);
                else
					return new FloatValue((float)v.DIntValue());
			}
			else if (op.val.Equals(Operator.INT))
			{
				if (v.Type() == Type.FLOAT)
					return new IntValue((int)v.DFloatValue());
				else if (v.Type() == Type.CHAR)
					return new IntValue((int)v.DCharValue());
				else
					throw new IException("Can only cast float or char to int (tried to cast " + v + ")", "", "Semantic", 0, 0);
            }
			else if (op.val.Equals(Operator.CHAR))
			{
				if (v.Type() == Type.INT)
					return new CharValue((char)v.DIntValue());
				else
					throw new IException("Can only cast int to char (tried to cast " + v + ")", "", "Semantic", 0, 0);
            }
			throw new IException("Semantic Error", "", "Semantic", 0, 0);
        }
		public static Value Interpret(Binary b, Functions funcs, State state)
		{
			Operator op = b.Operator();
			Value v1 = Interpret(b.Term1(), funcs, state);
			Value v2 = Interpret(b.Term2(), funcs, state);
			StaticTypeCheck.Check(!v1.Undefined() || !v2.Undefined(), "reference to undef value in binary op");
			if (op.IsArithmeticOp())
			{
				if (v1.Type() == Type.INT && v1.Type() == Type.INT)
				{
					if (op.val.Equals(Operator.PLUS))
						return new IntValue(v1.DIntValue() + v2.DIntValue());
					if (op.val.Equals(Operator.MINUS))
						return new IntValue(v1.DIntValue() - v2.DIntValue());
					if (op.val.Equals(Operator.TIMES))
						return new IntValue(v1.DIntValue() * v2.DIntValue());
					if (op.val.Equals(Operator.DIV))
						return new IntValue(v1.DIntValue() / v2.DIntValue());
				}
				else if (v1.Type() == Type.FLOAT && v2.Type() == Type.FLOAT)
				{
					if (op.val.Equals(Operator.PLUS))
						return new FloatValue(v1.DFloatValue() + v2.DFloatValue());
					if (op.val.Equals(Operator.MINUS))
						return new FloatValue(v1.DFloatValue() - v2.DFloatValue());
					if (op.val.Equals(Operator.TIMES))
						return new FloatValue(v1.DFloatValue() * v2.DFloatValue());
					if (op.val.Equals(Operator.DIV))
						return new FloatValue(v1.DFloatValue() / v2.DFloatValue());
				}
				else if ((v1.Type() == Type.INT && v2.Type() == Type.FLOAT) || (v1.Type() == Type.FLOAT && v2.Type() == Type.INT))
				{
					if (v1.Type() == Type.INT)
						v1 = new FloatValue((float)v1.DIntValue());
					else if (v2.Type() == Type.INT)
						v2 = new FloatValue((float)v2.DIntValue());
					return Interpret(new Binary(op, v1, v2), funcs, state);
				}
				else
                    throw new IException("Attemped arithmetic op on a " + v1.Type() + " and a " + v2.Type() + ", not allowed (v1: " + v1 + " v2: " + v2 + ")", "", "Semantic", 0, 0);
			}
			else if (op.IsBooleanOp())
			{
				if (!(v1.Type() == Type.BOOL && v1.Type() == Type.BOOL))
					throw new IException("Attemped boolean op on " + v1.Type() + ", not allowed", "", "Semantic", 0, 0);
                else
				{
					if (op.val.Equals(Operator.AND))
						return new BoolValue(v1.DBoolValue() && v2.DBoolValue());
					else if (op.val.Equals(Operator.OR))
						return new BoolValue(v1.DBoolValue() || v2.DBoolValue());
				}
			}
			else if (op.IsRelationalOp())
			{
				if (v1.Type() == Type.INT && v1.Type() == Type.INT)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.DIntValue() < v2.DIntValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.DIntValue() > v2.DIntValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.DIntValue() <= v2.DIntValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.DIntValue() >= v2.DIntValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.DIntValue() == v2.DIntValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.DIntValue() != v2.DIntValue());
				}
				else if (v1.Type() == Type.FLOAT && v1.Type() == Type.FLOAT)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.DFloatValue() < v2.DFloatValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.DFloatValue() > v2.DFloatValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.DFloatValue() <= v2.DFloatValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.DFloatValue() >= v2.DFloatValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.DFloatValue() == v2.DFloatValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.DFloatValue() != v2.DFloatValue());
				}
				else if ((v1.Type() == Type.INT && v2.Type() == Type.FLOAT) || (v1.Type() == Type.FLOAT && v2.Type() == Type.INT))
				{
					if (v1.Type() == Type.INT)
						v1 = new FloatValue((float)v1.DIntValue());
					else if (v2.Type() == Type.INT)
						v2 = new FloatValue((float)v2.DIntValue());
					return Interpret(new Binary(op, v1, v2), funcs, state);
				}
				else if (v1.Type() == Type.CHAR && v2.Type() == Type.CHAR)
				{
					if (op.val.Equals(Operator.LT))
						return new BoolValue(v1.DCharValue() < v2.DCharValue());
					else if (op.val.Equals(Operator.GT))
						return new BoolValue(v1.DCharValue() > v2.DCharValue());
					else if (op.val.Equals(Operator.LE))
						return new BoolValue(v1.DCharValue() <= v2.DCharValue());
					else if (op.val.Equals(Operator.GE))
						return new BoolValue(v1.DCharValue() >= v2.DCharValue());
					else if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.DCharValue() == v2.DCharValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.DCharValue() != v2.DCharValue());
				}
				else if (v1.Type() == Type.BOOL && v2.Type() == Type.BOOL)
				{
					if (op.val.Equals(Operator.EQ))
						return new BoolValue(v1.DBoolValue() == v2.DBoolValue());
					else if (op.val.Equals(Operator.NE))
						return new BoolValue(v1.DBoolValue() != v2.DBoolValue());
					else
                        throw new IException("Attempted illegal relational op " + op + " on two booleans (v1: " + v1 + " v2: " + v2 + ")", "", "Semantic", 0, 0);
				}
				else
					throw new IException("Attemped relational op on a " + v1.Type() + " and a " + v2.Type() + ", not allowed (v1: " + v1 + " v2: " + v2 + ")", "", "Semantic", 0, 0);
            }
			throw new IException("Semantic Error: (in DynamicTyping.ApplyBinary)", "", "Semantic", 0, 0);
        }
	}
}