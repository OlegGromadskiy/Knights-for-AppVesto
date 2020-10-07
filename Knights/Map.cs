using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knights
{
    class Map
    {
        public int size { get; set; }
        public bool[,] MapArray { get; set; }
        public Map(int size)
        {
            this.size = size;
            MapArray = new bool[size, size];
            Console.ForegroundColor = ConsoleColor.Green;            
        }
        public int GetScore()
        {
            var temp = 0;
            foreach (var item in MapArray)
                if (item)
                    temp++;
            return temp;
        }
        public bool CheckPosition(int x, int y)
        {
            return !MapArray[y, x];
        }
        public void Save(bool[,] state)
        {
            MapArray = state;
        }
        public bool[,] Load()
        {
            bool[,] temp = new bool[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    temp[i, j] = MapArray[i, j];

            return temp;
        }
        public void MarkPosition(int x, int y)
        {
            MapArray[y, x] = true;
        }
    }
}
