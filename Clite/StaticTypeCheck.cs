
using System;
using System.Collections.Generic;

namespace Clite
{
	public class StaticTypeCheck
	{
		public static TypeMap Typing(Declarations d)
		{
			TypeMap map = new TypeMap();
			foreach (Declaration di in d.Values)
                map.Add(di.variable(), di.type());
			return map;
		}

		public static void Check(bool test, string msg)
		{
			if (test)
				return;
            throw new IException(msg, "", "Semantic : TypeCheching", 0, 0);
		}

		public static Type TypeOf(IExpression e, Functions funcs, TypeMap tm)
		{
			if (e is Value)
				return ((Value) e).Type();
			else if (e is Call)
			{
				Call c = (Call) e;
				Function f = funcs[c.Identifier().ToString()];
				return f.type();
			}
			else if (e is Variable)
			{
				Variable v = (Variable) e;
				Check(tm.ContainsKey(v), "Caught undefined variable in static type Checker: " + v);
                return (Type)tm[v];

			}
			else if (e is Binary)
			{
				Binary b = (Binary)e;
				if (b.Operator().IsArithmeticOp())
				{
					if (TypeOf(b.Term1(), funcs, tm) == Type.INT && TypeOf(b.Term1(), funcs, tm) == Type.INT)
						return Type.INT;
					else
						return Type.FLOAT;
				}
				else if (b.Operator().IsRelationalOp() || b.Operator().IsBooleanOp())
					return Type.BOOL;
				else
                    throw new IException("Unknown binary op in static type Checker, expression: " + e, "", "Semantic", 0, 0);
			}
			else if (e is Unary)
			{
				Unary u = (Unary) e;
				if (u.Operator().IsNotOp())
					return Type.BOOL;
				else if (u.Operator().IsNegateOp())
					return TypeOf(u.term, funcs, tm);
				else if (u.Operator().IsIntOp())
					return Type.INT;
				else if (u.Operator().IsFloatOp())
					return Type.FLOAT;
				else if (u.Operator().IsCharOp())
					return Type.CHAR;
			}
            throw new IException("Type Checking Error", "", "Semantic", 0, 0);
        }

		public static void Validate(IProgram p)
		{
			Check(p.functions().ContainsKey("main"), "Error! Main function not found!");
			Validate(p.functions(), Typing(p.Globals()));
		}

		public static void Validate(Functions functions, TypeMap tm)
		{
			foreach (Function func in functions.Values)
			{
				TypeMap newMap = new TypeMap();
				newMap.PutAll(tm);
				newMap.PutAll(Typing(func.Param()));
				newMap.PutAll(Typing(func.Locals()));
				Validate(func, functions, newMap);
			}
		}

		public static void Validate(Function func, Functions functions, TypeMap tm)
		{
			bool hasReturn = false;
			IEnumerator<IStatement> it = func.Body().GetMembers();
			while (it.MoveNext())
			{
				IStatement s = it.Current;
				if (s is Return)
				{
					Check(!hasReturn, "Function " + func.Id() + " has multiple return statements!");
					hasReturn = true;

				}
				else if (s is Call)
				{
					Check(!hasReturn, "Return must be last expression in function block (in function " + func.Id() + "!");
					Validate((Call) s, functions, tm);
				}
				else
				{
					Check(!hasReturn, "Return must be last expression in function block (in function " + func.Id() + "!");
					Validate(s, functions, tm);
				}
			}

			if (func.type() != Type.VOID && !func.Id().Equals("main"))
				Check(hasReturn, "Non-void function " + func.Id() + " missing return statement!");
			else if (func.type() == Type.VOID)
				Check(!hasReturn, "Void function " + func.Id() + " has return statement when it shouldn't!");
		}

		public static void Validate(Call c, Functions funcs, TypeMap tm)
		{

            Function f;
            bool find = funcs.TryGetValue(c.Identifier().ToString(),out f);
            if(!find)
            {
                string args = "";
                foreach(var a in  tm)
                    args += a.Value + " " + a.Key + ",";
                args = args.Remove(args.Length - 1);
                string msg = string.Format("Cannot find function : {0}({1})", c.Identifier().ToString(),args);
                throw new IException(msg, "", "Semantic: Call", 0, 0);
            }
			IEnumerator<Declaration> funcIt = f.Param().Values.GetEnumerator();
			IEnumerator<IExpression> callIt = c.Arguments();
			while (funcIt.MoveNext())
			{
				Declaration dec = funcIt.Current;
				Check(callIt.MoveNext(), "Incorrect number of arguments for function call!");
				IExpression exp = callIt.Current;
				Type expType = TypeOf(exp, funcs, tm);
				Check(dec.type() == expType, "Wrong type in function call for " + dec.variable() + " (got a " + expType + ", expected a " + dec.type() + ")");
			}
			Check(!callIt.MoveNext(), "Incorrect number of arguments for function call!");
		}
        
		public static void Validate(IExpression e, Functions funcs, TypeMap tm)
		{
			if (e is Value)
				return;
            if (e is Call)
            {
                Validate((Call)e, funcs, tm);
                return;
            }
			if (e is Variable)
            {
                Variable v = (Variable) e;
				Check(tm.ContainsKey(v), "Caught undeclared variable in static type Check: " + v);
				return;
			}
			if (e is Binary)
			{
				Binary b = (Binary) e;

				Type type1 = TypeOf(b.Term1(), funcs, tm);
				Type type2 = TypeOf(b.Term2(), funcs, tm);

				Validate(b.Term1(), funcs, tm);
				Validate(b.Term2(), funcs, tm);
				if (b.Operator().IsArithmeticOp())
				{
					Check((type1 == Type.INT && type2 == Type.INT) || (type1 == Type.FLOAT && type2 == Type.FLOAT) || (type1 == Type.INT && type2 == Type.FLOAT) || (type1 == Type.FLOAT && type2 == Type.INT), "Type error for arithmetic op " + b.Operator() + "; got types " + type1 + " and " + type2);
				}
				else if (b.Operator().IsRelationalOp())
				{
					Check((type1 == Type.INT && type2 == Type.INT) || (type1 == Type.FLOAT && type2 == Type.FLOAT) || (type1 == Type.INT && type2 == Type.FLOAT) || (type1 == Type.FLOAT && type2 == Type.INT) || (type1 == Type.BOOL && type2 == Type.BOOL) || (type1 == Type.CHAR && type2 == Type.CHAR), "Type error for relational op " + b.Operator() + "; got types " + type1 + " and " + type2);
				}
				else if (b.Operator().IsBooleanOp())
				{
					Check(type1 == Type.BOOL && type2 == Type.BOOL, "Caught non-bool operand for " + b.Operator() + " in static type Checker; got types " + type1 + " and " + type2);
				}
				else
                    throw new IException("Type Checking Error", "", "Semantic", 0, 0);
                return;
			}
			else if (e is Unary)
			{
				Unary u = (Unary) e;
				Type t = TypeOf(u.term, funcs, tm);
				Validate(u.term, funcs, tm);
				if (u.Operator().IsNotOp())
					Check(t == Type.BOOL, "Attempted not operation on non-bool (attempted on " + t + ")");
				else if (u.Operator().IsNegateOp())
					Check(t == Type.FLOAT || t == Type.INT, "Attempted negate operation on something other than a float or int (attempted on " + t + ")");
				else if (u.Operator().IsIntOp())
					Check(t == Type.FLOAT || t == Type.CHAR, "Attempted int cast from something other than a float or a char (attempted on " + t + ")");
				else if (u.Operator().IsFloatOp())
					Check(t == Type.INT, "Attempted float cast from something other than an int (attempted on " + t + ")");
				else if (u.Operator().IsCharOp())
					Check(t == Type.INT, "Attempted char cast from something other than an int (attempted on " + t + ")");
			}
			else
                throw new IException("Type Checking Error", "", "Semantic", 0, 0);
        }

        public static void Validate(IStatement s, Functions funcs, TypeMap tm)
		{
			if (s == null)
                throw new IException("TypeChecking: null statement", "", "Semantic", 0, 0);

            else if (s is Skip)
				return;
			else if (s is Assignment)
			{
				Assignment a = (Assignment) s;
				Check(tm.ContainsKey(a.Target()), "Target not found in type map! (target: " + a.Target() + ")");
				Validate(a.Source(), funcs, tm);
				Type targettype = (Type) tm[a.Target()];
				Type srctype = TypeOf(a.Source(), funcs, tm);
				if (targettype != srctype)
				{
					if (targettype == Type.FLOAT)
						Check(srctype == Type.INT, "Caught mixed mode assignment in static type Checker from " + srctype + " to " + targettype + " (target: " + a.Target() + ")");
					else if (targettype == Type.INT)
						Check(srctype == Type.CHAR || srctype == Type.FLOAT, "Caught mixed mode assignment in static type Checker from " + srctype + " to " + targettype + " (target: " + a.Target() + ")");
					else
						Check(false, "Caught mixed mode assignment in static type Checker from " + srctype + " to " + targettype + " (target: " + a.Target() + ")");
				}
				return;
			}
			else if (s is Block)
			{
				Block b = (Block) s;
				IEnumerator<IStatement> members = b.GetMembers();
				while (members.MoveNext())
					Validate(members.Current, funcs, tm);
			}
			else if (s is Loop)
			{
				Loop l = (Loop) s;
				Validate(l.Test(), funcs, tm);
				Validate(l.Body(), funcs, tm);
			}
			else if (s is Conditional)
			{
				Conditional c = (Conditional) s;
				Validate(c.Test(), funcs, tm);
				Validate(c.ThenBranch(), funcs, tm);
				Validate(c.ElseBranch(), funcs, tm);
			}
			else if (s is Return)
			{
				Return r = (Return)s;
				Function f = funcs[r.FunctionName().ToString()];
				Type t = TypeOf(r.Result(), funcs, tm);
				Check(t == f.type(), "Return expression doesn't match function's return type! (got a " + t + ", expected a " + f.type() + ")");
			}
			else
                throw new IException("Type Checking Error : " + s, "", "Semantic", 0, 0);
        }
	}
}