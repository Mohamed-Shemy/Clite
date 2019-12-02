using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    class IException : Exception
    {
        public string Message { get; set; }
        public string File { get; set; }
        public string Type { get; set; }
        public int LineNumber { get; set; }
        public int ColmunNumber { get; set; }
        public IException(string msg, string file, string type, int line, int col)
        {
            Message = msg;
            File = file;
            LineNumber = line;
            ColmunNumber = col;
            Type = type;
        }
    }
}
