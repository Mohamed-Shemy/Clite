using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Clite
{
    class Scanner
    {
        public string ExceptionType = "Lexical";
        public string FileName = "";
        private StreamReader input;
        private char currentChar = ' ';
        private string currentLine = "";
        private int lineno = 0;
        private int column = 1;
        private string LETTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string DIGITS = "0123456789";
        private const char EOL = '\n', EOF = '\u001a';

        public Scanner(string fileName)
        {
            try
            {
                input = new StreamReader(fileName, Encoding.UTF8);
                FileName = Path.GetFileNameWithoutExtension(fileName);
            }
            catch (FileNotFoundException e)
            {
                throw new IException(e.Message, FileName, "FileNotFound", lineno, column);
            }
        }

        public Scanner(Stream stream)
        {
            input = new StreamReader(stream);
            FileName = "Current Code";
        }
        private char NextChar()
        {
            if (currentChar == EOF)
                Error("Attempt to read past end of file");
            column++;
            if (column >= currentLine.Length)
            {
                try
                {
                    currentLine = input.ReadLine();
                }
                catch (IOException e)
                {
                    throw new IException(e.Message, FileName, "IO", lineno, column);
                }
                if (currentLine == null)
                    currentLine = "" + EOF;
                else
                {
                    lineno++;
                    currentLine += EOL;
                }
                column = 0;
            }
            return currentLine[column];
        }

        public Token Next()
        {
            do
            {
                if (char.IsLetter(currentChar))
                {
                    string spelling = Concat(LETTERS + DIGITS);
                    return Token.Keyword(spelling);
                }
                else if (char.IsDigit(currentChar))
                {
                    string number = Concat(DIGITS);
                    if (currentChar != '.')
                        return Token.MkIntLiteral(number);
                    else
                    {
                        number += Concat("." + DIGITS);
                        return Token.MkFloatLiteral(number);
                    }
                }
                else
                    switch (currentChar)
                    {
                        case ' ':
                        case '\t':
                        case '\r':
                        case EOL:
                            currentChar = NextChar();
                            break;

                        case '/':
                            currentChar = NextChar();
                            if (currentChar != '/')
                                return Token.divideTok;
                            currentChar = NextChar();
                            while (currentChar != EOL)
                                currentChar = NextChar();
                            currentChar = NextChar();
                            break;

                        case '\'':
                            char ch1 = NextChar();
                            NextChar();
                            currentChar = NextChar();
                            return Token.MkCharLiteral("" + ch1);

                        case '+':
                            currentChar = NextChar();
                            return Token.plusTok;

                        case '-':
                            currentChar = NextChar();
                            return Token.minusTok;

                        case '*':
                            currentChar = NextChar();
                            return Token.multiplyTok;

                        case '(':
                            currentChar = NextChar();
                            return Token.leftParenTok;

                        case ')':
                            currentChar = NextChar();
                            return Token.rightParenTok;

                        case '{':
                            currentChar = NextChar();
                            return Token.leftBraceTok;

                        case '}':
                            currentChar = NextChar();
                            return Token.rightBraceTok;
                       
                        case ';':
                            currentChar = NextChar();
                            return Token.semicolonTok;

                        case ',':
                            currentChar = NextChar();
                            return Token.commaTok;

                        case '&':
                            Check('&');
                            return Token.andTok;
                        case '|':
                            Check('|');
                            return Token.orTok;

                        case '=':
                            return ChkOpt('=', Token.assignTok, Token.eqeqTok);

                        case '<':
                            return ChkOpt('=', Token.ltTok, Token.lteqTok);

                        case '>':
                            return ChkOpt('=', Token.gtTok, Token.gteqTok);

                        case '!':
                            return ChkOpt('=', Token.notTok, Token.noteqTok);

                        default:
                            Error("Illegal character " + currentChar); break;
                    }
            } while ((currentChar != '\u001a'));
            return Token.MkCharLiteral("eof");
        }

        public void Error(string msg)
        {
            throw new IException(msg, FileName, ExceptionType, lineno, column);
        }

        private void Check(char c)
        {
            currentChar = NextChar();
            if (currentChar != c)
                Error("Illegal character, expecting " + c);
            currentChar = NextChar();
        }

        private Token ChkOpt(char c, Token one, Token two)
        {
            currentChar = NextChar();
            if (currentChar != c)
                return one;
            currentChar = NextChar();
            return two;
        }

        private string Concat(string set)
        {
            string r = "";
            while (set.Contains("" + currentChar))
            {
                r += currentChar;
                currentChar = NextChar();
            }
            return r;
        }

        public int LineNumber()
        {
            return lineno;
        }

        public int ColumnNumber()
        {
            return column;
        }
    }
}
