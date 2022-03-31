using System;

namespace Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            Random gen = new Random();
            int entrance = gen.Next(0, 3);
            Map map = new Map(entrance);
            map.PrintMap();

            //map.PrintCell(0, entrance);
        }
    }
}
