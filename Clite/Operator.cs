using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Operator
    {
        public static string
                      AND = "&&",
                      OR = "||",

                      LT = "<",
                      LE = "<=",
                      EQ = "==",
                      NE = "!=",
                      GT = ">",
                      GE = ">=",

                      PLUS = "+",
                      MINUS = "-",
                      TIMES = "*",
                      DIV = "/",

                      NOT = "!",
                      NEG = "-",

                      INT = "int",
                      FLOAT = "float",
                      CHAR = "char";

        public string val;
        public Operator(string s) { val = s; }


        public override string ToString() { return val; }

        public override bool Equals(object obj) { return val.Equals(obj); }

        public bool IsBooleanOp()
        {
            return val.Equals(AND) || val.Equals(OR);
        }

        public bool IsRelationalOp()
        {
            return val.Equals(LT) ||
                   val.Equals(LE) ||
                   val.Equals(EQ) ||
                   val.Equals(NE) ||
                   val.Equals(GT) ||
                   val.Equals(GE);
        }

        public bool IsArithmeticOp()
        {
            return val.Equals(PLUS) ||
                   val.Equals(MINUS) ||
                   val.Equals(TIMES) ||
                   val.Equals(DIV);
        }

        public bool IsNotOp()
        {
            return val.Equals(NOT);
        }

        public bool IsNegateOp()
        {
            return val.Equals(NEG);
        }

        public bool IsIntOp()
        {
            return val.Equals(INT);
        }

        public bool IsFloatOp()
        {
            return val.Equals(FLOAT);
        }

        public bool IsCharOp()
        {
            return val.Equals(CHAR);
        }
    }
}
