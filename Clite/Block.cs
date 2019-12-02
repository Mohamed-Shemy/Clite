using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clite
{
    public class Block : IStatement
    {
        private Stack<IStatement> members;

        public Block()
        {
            members = new Stack<IStatement>();
        }

        public void AddMember(IStatement newMember)
        {
            members.Push(newMember);
        }

        public IEnumerator<IStatement> GetMembers()
        {
            return members.GetEnumerator();
        }

        public string Display(int indent)
        {
            string tmp = "";
            foreach (IStatement s in members)
                tmp += s.Display(indent + 1) + "\n";
            return tmp;
        }
    }
}
