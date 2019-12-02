using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Text;
using System;
using System.Diagnostics;

namespace Clite
{
    class Build
    {
        string Code = "";
        string exe = "clite";
        public Build(string code,string _exe)
        {
            Code = "" ;
            exe = _exe;

            
            Code = PrepareCode(code);
            string[] lines = Code.Split('\n');
            Code = "";
            foreach (string l in lines)
                Code += string.Format("Console.WriteLine(\"{0}\");\n", l);
            Code = SetCode(Code);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters parameters = new CompilerParameters();
            parameters.ReferencedAssemblies.Add("System.dll");
            parameters.OutputAssembly = _exe;
            parameters.GenerateInMemory = true;
            parameters.GenerateExecutable = true;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, Code);
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in results.Errors)
                    sb.AppendLine(String.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));
                throw new InvalidOperationException(sb.ToString());
            }
            Assembly assembly = results.CompiledAssembly;
            Debug.WriteLine(Assembly.GetExecutingAssembly().CodeBase);
            System.Type program = assembly.GetType("Clite.Program");
            MethodInfo main = program.GetMethod("Main");
            main.Invoke(null, null);
        }

        private string SetCode(string _code)
        {
            string code="using System;namespace Clite{public class Program{public static void Main(){"+_code +"Console.Read();}}}";
            return code;
        }

        private string PrepareCode(string _code)
        {
            string Code = "";
            foreach (string l in _code.Split('\n'))
            {
                string[] tok = l.Split(' ');
                for (int i = 0; i < tok.Length - 1; i++)
                    Code += tok[i];
                Code += "\n";
            }

            return Code;
        }


        private string GetAssemblyCode(State state,string[] tokens)
        {
            string asm = "";
           
            foreach(string t in tokens)
            {
                
            }

            return "";
        }
    }
}
