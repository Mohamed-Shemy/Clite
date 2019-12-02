
using System.Collections.Generic;
using System.Linq;
using System;

namespace Clite
{
	public class TypeTransformer
	{
		public static IProgram Transform(IProgram p)
		{
			TypeMap globalMap = StaticTypeCheck.Typing(p.Globals());

            Functions funcs = p.functions();

			foreach (Function f in p.functions().Values.ToList())
			{
				TypeMap newMap = new TypeMap();
				newMap.PutAll(globalMap);
				newMap.PutAll(StaticTypeCheck.Typing(f.Param()));
				newMap.PutAll(StaticTypeCheck.Typing(f.Locals()));
				Block transformedBody = (Block) Transform(f.Body(), funcs, newMap);
                if (!funcs.ContainsKey(f.Id()))
                    funcs.Add(f.Id(), new Function(f.type(), f.Id(), f.Param(), f.Locals(), transformedBody));
                else
                    funcs[f.Id()] = new Function(f.type(), f.Id(), f.Param(), f.Locals(), transformedBody);

            }

			return new IProgram(p.Globals(), funcs);
		}

		public static IStatement Transform(IStatement s, Functions funcs, TypeMap tm)
		{
			if (s is Skip || s is Return || s is Call)
				return s;
			if (s is Assignment)
			{
				Assignment a = (Assignment) s;

				Variable target = a.Target();
				IExpression src = a.Source();

				Type targettype = (Type) tm[a.Target()];
				Type srctype = StaticTypeCheck.TypeOf(a.Source(), funcs, tm);

				if (targettype == Type.FLOAT)
				{
					if (srctype == Type.INT)
					{
						src = new Unary(new Operator(Operator.FLOAT), src);
						srctype = Type.FLOAT;
					}

				}
				else if (targettype == Type.INT)
				{
					if (srctype == Type.CHAR)
					{
						src = new Unary(new Operator(Operator.INT), src);
						srctype = Type.INT;
					}
					else if (srctype == Type.FLOAT)
					{
						src = new Unary(new Operator(Operator.INT), src);
						srctype = Type.INT;
					}
				}
				StaticTypeCheck.Check(targettype == srctype, "bug in assignment to " + target);
				return new Assignment(target, src);
			}

			if (s is Conditional)
			{
				Conditional c = (Conditional) s;
				IExpression test = c.Test();
				IStatement tbr = Transform(c.ThenBranch(), funcs, tm);
				IStatement ebr = Transform(c.ElseBranch(), funcs, tm);
				return new Conditional(test, tbr, ebr);
			}

			if (s is Loop)
			{
				Loop l = (Loop) s;
				IExpression test = l.Test();
				IStatement body = l.Body();
				return new Loop(test, body);
			}

			if (s is Block)
			{
				Block b = (Block) s;
				Block @out = new Block();
				IEnumerator<IStatement> members = b.GetMembers();
				while (members.MoveNext())
					@out.AddMember(Transform(members.Current, funcs, tm));
				return @out;
			}
			throw new IException("Semantic Error", "", "Semantic", 0, 0);
        }
	}

    public static class Extension
    {
        public static void PutAll<T1,T2>(this Dictionary<T1,T2> org, Dictionary<T1,T2> trg)
        {
            try
            {
                foreach (var a in trg)
                    org.Add(a.Key, a.Value);
            }catch(Exception ex)
            {
                throw new IException("there is a variable has already Defined", "", "Semantic", 0, 0);
            }
        }
    }

}