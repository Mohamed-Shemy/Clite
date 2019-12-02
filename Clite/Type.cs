using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public sealed class Type
    {
        public static readonly Type INT = new Type("INT", InnerEnum.INT, "int");
        public static readonly Type BOOL = new Type("BOOL", InnerEnum.BOOL, "bool");
        public static readonly Type CHAR = new Type("CHAR", InnerEnum.CHAR, "char");
        public static readonly Type FLOAT = new Type("FLOAT", InnerEnum.FLOAT, "float");
        public static readonly Type VOID = new Type("VOID", InnerEnum.VOID, "void");

        private static readonly IList<Type> valueList = new List<Type>();

        static Type()
        {
            valueList.Add(INT);
            valueList.Add(BOOL);
            valueList.Add(CHAR);
            valueList.Add(FLOAT);
            valueList.Add(VOID);
        }

        public enum InnerEnum
        {
            INT,
            BOOL,
            CHAR,
            FLOAT,
            VOID
        }

        public readonly InnerEnum innerEnumValue;
        private readonly string nameValue;
        private readonly int ordinalValue;
        private static int nextOrdinal = 0;

        private string id;

        private Type(string name, InnerEnum innerEnum, string id)
        {
            this.id = id;

            nameValue = name;
            ordinalValue = nextOrdinal++;
            innerEnumValue = innerEnum;
        }

        public override string ToString()
        {
            return id;
        }

        public static IList<Type> Values()
        {
            return valueList;
        }

        public int Ordinal()
        {
            return ordinalValue;
        }

        public static Type ValueOf(string name)
        {
            foreach (Type enumInstance in Type.valueList)
                if (enumInstance.nameValue == name)
                    return enumInstance;
            throw new IException(name, "", "Syntax: Type", 0, 0);
        }
    }
}
    
