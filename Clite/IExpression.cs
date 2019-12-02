using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public interface IExpression
    {
        string Display(int indent);
    }


    public interface IStatement
    {
        string Display(int indent);
    }
}
