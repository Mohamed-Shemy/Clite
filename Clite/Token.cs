using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class Token
    {
        private Type type;
        private string value = "";
        private static Dictionary<string, Token> keywords = GetKeywordsMap();
        public enum Type
        {
            Int, Float, Char,
            Bool, True, False,

            Plus, Minus,
            Multiply, Divide,

            If, Else, While,

            And, Or,
            Less, LessEqual,
            Greater, GreaterEqual,
            Not, NotEqual,

            LeftBrace, RightBrace,
            LeftBracket, RightBracket,
            LeftParen, RightParen,

            Semicolon, Comma,
            Assign, Equals,

            Eof,
            Return, Void,

            Identifier,
            IntLiteral,
            FloatLiteral,
            CharLiteral
        }

        #region Tokens
        public static Token

        intTok = new Token(Type.Int, "int"),
        floatTok = new Token(Type.Float, "float"),
        charTok = new Token(Type.Char, "char"),

        boolTok = new Token(Type.Bool, "bool"),
        ifTok = new Token(Type.If, "if"),
        elseTok = new Token(Type.Else, "else"),
        trueTok = new Token(Type.True, "true"),
        falseTok = new Token(Type.False, "false"),
        whileTok = new Token(Type.While, "while"),
        andTok = new Token(Type.And, "&&"),
        orTok = new Token(Type.Or, "||"),

        leftBraceTok = new Token(Type.LeftBrace, "{"),
        rightBraceTok = new Token(Type.RightBrace, "}"),
        leftBracketTok = new Token(Type.LeftBracket, "["),
        rightBracketTok = new Token(Type.RightBracket, "]"),
        leftParenTok = new Token(Type.LeftParen, "("),
        rightParenTok = new Token(Type.RightParen, ")"),

        semicolonTok = new Token(Type.Semicolon, ";"),
        commaTok = new Token(Type.Comma, ","),
        assignTok = new Token(Type.Assign, "="),

        eqeqTok = new Token(Type.Equals, "=="),
        ltTok = new Token(Type.Less, "<"),
        lteqTok = new Token(Type.LessEqual, "<="),
        gtTok = new Token(Type.Greater, ">"),
        gteqTok = new Token(Type.GreaterEqual, ">="),
        notTok = new Token(Type.Not, "!"),
        noteqTok = new Token(Type.NotEqual, "!="),

        plusTok = new Token(Type.Plus, "+"),
        minusTok = new Token(Type.Minus, "-"),
        multiplyTok = new Token(Type.Multiply, "*"),
        divideTok = new Token(Type.Divide, "/");

        #endregion

        private static Dictionary<string, Token> GetKeywordsMap()
        {
            Dictionary<string, Token> kw = new Dictionary<string, Token>
            {
                { "bool", new Token(Type.Bool, "bool") },
                { "char", new Token(Type.Char, "char") },
                { "int", new Token(Type.Int, "int") },
                { "float", new Token(Type.Float, "float") },
                { "if", new Token(Type.If, "if") },
                { "else", new Token(Type.Else, "eles") },
                { "true", new Token(Type.True, "true") },
                { "false", new Token(Type.False, "false") },
                { "void",new Token(Type.Void,"void") },
                { "while", new Token(Type.While, "while") },
                { "return", new Token(Type.Return,"return") }
            };

            return kw;
        }

        public static Token Keyword(string name)
        {
            keywords.TryGetValue(name, out Token keyword);
            return keyword ?? MkIdentTok(name);
        }

        private Token(Type t, string v)
        {
            type = t;
            value = v;
        }

        public new Type GetType()
        {
            return type;
        }

        public string GetValue()
        {
            return value;
        }

        public static Token MkIdentTok(string name)
        {
            return new Token(Type.Identifier, name);
        }

        public static Token MkIntLiteral(string name)
        {
            return new Token(Type.IntLiteral, name);
        }

        public static Token MkFloatLiteral(string name)
        {
            return new Token(Type.FloatLiteral, name);
        }

        public static Token MkCharLiteral(string name)
        {
            return new Token(Type.CharLiteral, name);
        }

        public override string ToString()
        {
            if (type.CompareTo(Type.Identifier) < 0)
                return value;
            return type + "\t" + value;
        }
    }
}
