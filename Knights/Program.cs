using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseMove
{
    class Program
    {
        static void Main(string[] args)
        {
            Map.DrawMap();
            new Horse(0, 2).Start();

            Console.Read();
        }
    }
}
