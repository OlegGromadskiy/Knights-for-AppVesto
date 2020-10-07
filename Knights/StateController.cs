using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights
{
    class StateController
    {
        Stack<bool[,]> states;

        public bool[,] State
        {
            get => states.Pop();
            set => states.Push(value);
        }
        public StateController()
        {
            states = new Stack<bool[,]>();
        }
    }
}
