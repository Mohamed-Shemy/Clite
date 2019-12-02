using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
   
    class Parser
    {
        private Token currentToken;
        private Variable currentFunction;
        private Scanner lexer;
        private string FileName = "";
        private string ExceptionType = "Syntax";

        public Parser(string file) : this(new Scanner(file)) { FileName = file; }
      
        public Parser(Scanner lexer)
        {
            this.lexer = lexer;
            currentToken = lexer.Next();
            FileName = lexer.FileName;
        }
        private string Match(Token.Type t)
        {
            if (currentToken.GetType() == t)
                currentToken = lexer.Next();
            else
                Error(t.ToString());
            return currentToken.GetValue();
        }
        private void Error(string message)
        {
            string msg = (message + "; CT: " + currentToken.GetValue());
            throw new IException(msg, FileName, ExceptionType, lexer.LineNumber(), lexer.ColumnNumber());
        }

        public IProgram Program()
        {
            Declarations globals = new Declarations();
            Functions funcs = new Functions();
            globals.PutAll(Declarations(funcs));
            return new IProgram(globals, funcs);
        }

        private Declarations Declarations(Functions functions)
        {
            Declarations ds = new Declarations();

            while (IsType())
                Declaration(ds, functions);

            return ds;
        }
        private void Declaration(Declarations ds, Functions functions)
        {
            Type Type = GetType();

            while (currentToken.GetType() != Token.Type.Eof)
            {
                Variable v = new Variable(currentToken.GetValue());
                Match(Token.Type.Identifier);
                if (currentToken.GetType() == Token.Type.LeftParen)
                {
                    Function(functions, Type, v);
                    if (IsType())
                        Type = GetType();
                    else
                        break;
                }
                else
                {
                   ds.Add(v.ToString(), new Declaration(v, Type));
                    if (currentToken.GetType() == Token.Type.Comma)
                        Match(Token.Type.Comma);
                    else if (currentToken.GetType() == Token.Type.Semicolon)
                    {
                        Match(Token.Type.Semicolon);
                        if (IsType())
                            Type = GetType();
                        else
                            break;
                    }
                }
            }
        }

       
        private void Function(Functions functions, Type t, Variable v)
        {
            currentFunction = v;
            Match(Token.Type.LeftParen);
            Declarations param = Parameters();
            Match(Token.Type.RightParen);
            Match(Token.Type.LeftBrace);
            Declarations locals = Declarations(functions);
            Block body = Statements();
            Match(Token.Type.RightBrace);

            functions.Add(v.ToString(), new Function(t, v.ToString(), param, locals, body));
        }

        private Declarations Parameters()
        {
            Declarations decs = new Declarations();

            while (currentToken.GetType() != Token.Type.RightParen)
            {
                Type t = GetType();
                Variable v = new Variable(currentToken.GetValue());
                Match(Token.Type.Identifier);
                decs.Add(v.ToString(), new Declaration(v, t));
                if (currentToken.GetType() == Token.Type.Comma)
                    Match(Token.Type.Comma);
            }

            return decs;
        }
        private new Type GetType()
        {
            Type t = null;
           
            if (currentToken.GetType() == Token.Type.Int)
                t = Type.INT;
            else if (currentToken.GetType() == Token.Type.Bool)
                t = Type.BOOL;
            else if (currentToken.GetType() == Token.Type.Float)
                t = Type.FLOAT;
            else if (currentToken.GetType() == Token.Type.Char)
                t = Type.CHAR;
            else if (currentToken.GetType() == Token.Type.Void)
                t = Type.VOID;
            else
                Error("Current token not Type (current token: " + currentToken + ")");

            Match(currentToken.GetType());
            return t;
        }

        private IStatement Statement()
        {
            if (currentToken.GetType() == Token.Type.Semicolon)
                return new Skip();

            else if (currentToken.GetType() == Token.Type.If)
                return IfStatement();

            else if (currentToken.GetType() == Token.Type.While)
                return WhileStatement();

            else if (currentToken.GetType() == Token.Type.Identifier)
            {
                Variable name = new Variable(currentToken.GetValue());
                Match(Token.Type.Identifier);

                if (currentToken.GetType() == Token.Type.Assign)
                    return Assignment(name);
                else if (currentToken.GetType() == Token.Type.LeftParen)
                {
                    Call c = CallStatement(name);
                    Match(Token.Type.Semicolon);
                    return c;
                }

            }
            else if (currentToken.GetType() == Token.Type.LeftBrace)
            {
                Match(Token.Type.LeftBrace);
                Block bl = Statements();
                Match(Token.Type.RightBrace);
                return bl;
            }
            else if (currentToken.GetType() == Token.Type.Return)
            
                return ReturnStatement(currentFunction);
            else
                Error("Unknown statement Type: " + currentToken.GetValue());

            return null;
        }

        private Call CallStatement(Variable id)
        {
            Match(Token.Type.LeftParen);

            Stack<IExpression> args = new Stack<IExpression>();
            while (!(currentToken.GetType() == Token.Type.RightParen))
            {
                args.Push(Expression());
                if (currentToken.GetType() == Token.Type.Comma)
                    Match(Token.Type.Comma);
            }

            Match(Token.Type.RightParen);

            return new Call(id, args);
        }
        private Return ReturnStatement(Variable functionName)
        {
            Match(Token.Type.Return);

            IExpression ret = Expression();

            Match(Token.Type.Semicolon);

            return new Return(functionName, ret);
        }

        private Block Statements()
        {
            Block b = new Block();
            while (currentToken.GetType() != Token.Type.RightBrace && currentToken.GetType() != Token.Type.Eof)
                b.AddMember(Statement());
            return b;
        }

        private Assignment Assignment(Variable id)
        {
            Match(Token.Type.Assign);
            IExpression source = Expression();
            Match(Token.Type.Semicolon);
            return new Assignment(id, source);
        }

        private Conditional IfStatement()
        {
            Match(Token.Type.If);

            Match(Token.Type.LeftParen);
            IExpression _expression = Expression();
            Match(Token.Type.RightParen);
            IStatement ifstatement = Statement();
            IStatement elsestatement = (currentToken.GetType() == Token.Type.Else) ? elsestatement = Statement() : new Skip();

            return new Conditional(_expression, ifstatement, elsestatement);
        }

        private Loop WhileStatement()
        {
            Match(Token.Type.While);

            Match(Token.Type.LeftParen);
            IExpression expression = Expression();
            Match(Token.Type.RightParen);

            IStatement statement = Statement();

            return new Loop(expression, statement);
        }

        private IExpression Expression()
        {
            IExpression e = Conjunction();

            while (currentToken.GetType() == Token.Type.Or)
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(Token.Type.Or);
                IExpression term2 = Conjunction();
                e = new Binary(op, e, term2);
            }

            return e;
        }

        private IExpression Conjunction()
        {
            IExpression e = Equality();

            while (currentToken.GetType() == Token.Type.And)
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(Token.Type.And);
                IExpression term2 = Equality();
                e = new Binary(op, e, term2);
            }

            return e;
        }

        private IExpression Equality()
        {
            IExpression e = Relation();

            while (IsEqualityOp())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                IExpression term2 = Relation();
                e = new Binary(op, e, term2);
            }

            return e;
        }

        private IExpression Relation()
        {
            IExpression e = Addition();

            while (IsRelationalOp())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                IExpression term2 = Addition();
                e = new Binary(op, e, term2);
            }

            return e;
        }

        private IExpression Addition()
        {
            IExpression e = Term();

            while (IsAddOp())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                IExpression term2 = Term();
                e = new Binary(op, e, term2);
            }
            return e;
        }

        private IExpression Term()
        {
            IExpression e = Factor();

            while (IsMultiplyOp())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                IExpression term2 = Factor();
                e = new Binary(op, e, term2);
            }
            return e;
        }

        private IExpression Factor()
        {
            if (IsUnaryOp())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                IExpression term = Primary();

                return new Unary(op, term);
            }
            else
                return Primary();
        }
        private IExpression Primary()
        {
            IExpression e = null;

            if (currentToken.GetType() == Token.Type.Identifier)
            {
                Variable v = new Variable(currentToken.GetValue());
                Match(Token.Type.Identifier);

                if (currentToken.GetType() == Token.Type.LeftParen)
                    e = CallStatement(v);
                else
                    e = v;

            }
            else if (IsLiteral())
                e = Literal();
            else if (currentToken.GetType().Equals(Token.Type.LeftParen))
            {
                currentToken = lexer.Next();
                e = Expression();
                Match(Token.Type.RightParen);
            }
            else if (IsType())
            {
                Operator op = new Operator(currentToken.GetValue());
                Match(currentToken.GetType());
                Match(Token.Type.LeftParen);
                IExpression term = Expression();
                Match(Token.Type.RightParen);
                e = new Unary(op, term);
            }
            else
                Error("Identifier | Literal | ( | Type");
            return e;
        }

        private Value Literal()
        {
            try
            {
                if (currentToken.GetType() == Token.Type.IntLiteral)
                {
                    Value v = new IntValue(int.Parse(currentToken.GetValue()));
                    Match(Token.Type.IntLiteral);
                    return v;
                }
                else if (currentToken.GetType() == Token.Type.FloatLiteral)
                {
                    Value v = new FloatValue(float.Parse(currentToken.GetValue()));
                    Match(Token.Type.FloatLiteral);
                    return v;
                }

                else if (currentToken.GetType() == Token.Type.CharLiteral)
                {
                    Value v = new CharValue(currentToken.GetValue()[(0)]);
                    Match(Token.Type.CharLiteral);
                    return v;
                }
                else
                    Error("unknown token Type for literal! Token value: " + currentToken.GetValue());
            }
            catch (Exception e)
            {
                Error("Inavlid number format " + e.Message);
            }
            return null;
        }

        private bool IsAddOp()
        {
            return currentToken.GetType() == Token.Type.Plus
                || currentToken.GetType() == Token.Type.Minus;
        }

        private bool IsMultiplyOp()
        {
            return currentToken.GetType() == Token.Type.Multiply
                || currentToken.GetType() == Token.Type.Divide;
        }

        private bool IsUnaryOp()
        {
            return currentToken.GetType() == Token.Type.Not
                || currentToken.GetType() == Token.Type.Minus;
        }

        private bool IsEqualityOp()
        {
            return currentToken.GetType() == Token.Type.Equals
                || currentToken.GetType() == Token.Type.NotEqual;
        }

        private bool IsRelationalOp()
        {
            return currentToken.GetType() == Token.Type.Less
                || currentToken.GetType() == Token.Type.LessEqual
                || currentToken.GetType() == Token.Type.Greater
                || currentToken.GetType() == Token.Type.GreaterEqual;
        }

        private bool IsType()
        {
            return currentToken.GetType() == Token.Type.Int
                || currentToken.GetType() == Token.Type.Bool
                || currentToken.GetType() == Token.Type.Float
                || currentToken.GetType() == Token.Type.Char
                || currentToken.GetType() == Token.Type.Void;
        }

        private bool IsLiteral()
        {
            return currentToken.GetType() == Token.Type.IntLiteral
                || currentToken.GetType() == Token.Type.FloatLiteral
                || currentToken.GetType() == Token.Type.CharLiteral
                || IsBooleanLiteral();
        }

        private bool IsBooleanLiteral()
        {
            return currentToken.GetType() == Token.Type.True
                || currentToken.GetType() == Token.Type.False;
        }
    }
}
