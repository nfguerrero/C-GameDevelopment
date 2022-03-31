using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Map
    {
        private Cell[,] map; //4x4 map of cells

        public Map(int e)
        {
            map = new Cell[4, 4]; //Initialize map
            //map[0, e] = new Cell("Rend");

            Generate(e); //Run path generation

            //Fill in non path cells
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == null)
                    {
                        map[i, j] = new Cell("space");
                    }
                }
            }
        }

        private void Generate(int e)
        {
            string last = "e";
            int curX = e;
            int curY = 0;
            Random gen = new Random();
            bool lck = true;

            while (lck)
            {
                int p = gen.Next(1, 3);
                if (p == 1)//left
                {
                    if (last == "e" && curX != 0)
                    {
                        map[0, e] = new Cell("Eleft");
                        curX -= 1;
                        last = "l";
                    }
                    else if (last == "e" && curX == 0)
                    {
                        map[0, e] = new Cell("Eright");
                        curX += 1;
                        last = "r";
                    }
                    else if (last == "l" && curX != 0)
                    {
                        map[curY, curX] = new Cell("Lleft");
                        curX -= 1;
                        last = "l";
                    }
                    else if (last == "l" && curX == 0 && curY != 3)
                    {
                        map[curY, curX] = new Cell("Rdown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "l" && curX == 0 && curY == 3)
                    {
                        map[curY, curX] = new Cell("Rend");
                        lck = false; 
                    }
                    else if (last == "r" && curY != 3)
                    {
                        map[curY, curX] = new Cell("Ldown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "r" && curX == 0 && curY == 3)
                    {
                        map[curY, curX] = new Cell("Lend");
                        lck = false;
                    }
                    else if (last == "d" && curX != 0)
                    {
                        map[curY, curX] = new Cell("Dleft");
                        curX -= 1;
                        last = "l";
                    }
                    else if (last == "d" && curX == 0)
                    {
                        map[curY, curX] = new Cell("Dright");
                        curX += 1;
                        last = "r";
                    }
                }
                else if (p == 3)//right
                {
                    if (last == "e" && curX != 3)
                    {
                        map[0, e] = new Cell("ERight");
                        curX += 1;
                        last = "r";
                    }
                    else if (last == "e" && curX == 3)
                    {
                        map[0, e] = new Cell("ELeft");
                        curX -= 1;
                        last = "l";
                    }
                    else if (last == "r" && curX != 3)
                    {
                        map[curY, curX] = new Cell("Rright");
                        curX += 1;
                        last = "r";
                    }
                    else if (last == "r" && curX == 3 && curY != 3)
                    {
                        map[curY, curX] = new Cell("Ldown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "r" && curX == 3 && curY == 3)
                    {
                        map[curY, curX] = new Cell("Lend");
                        lck = false;
                    }
                    else if (last == "l" && curY != 3)
                    {
                        map[curY, curX] = new Cell("Rdown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "l" && curX == 0 && curY == 3)
                    {
                        map[curY, curX] = new Cell("Rend");
                        lck = false;
                    }
                    else if (last == "d" && curX != 3)
                    {
                        map[curY, curX] = new Cell("Dright");
                        curX += 1;
                        last = "r";
                    }
                    else if (last == "d" && curX == 3)
                    {
                        map[curY, curX] = new Cell("Dleft");
                        curX -= 1;
                        last = "l";
                    }
                }
                else if (p == 2)//down
                {
                    if (last == "e")
                    {
                        map[0, e] = new Cell("Edown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "l" && curY != 3)
                    {
                        map[curY, curX] = new Cell("Rdown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "l" && curY == 3)
                    {
                        map[curY, curX] = new Cell("Rend");
                        lck = false;
                    }
                    else if (last == "r" && curY != 3)
                    {
                        map[curY, curX] = new Cell("Ldown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "r" && curY == 3)
                    {
                        map[curY, curX] = new Cell("Lend");
                        lck = false;
                    }
                    else if (last == "d" && curY != 3)
                    {
                        map[curY, curX] = new Cell("Ddown");
                        curY += 1;
                        last = "d";
                    }
                    else if (last == "d" && curY == 3)
                    {
                        Random gen2 = new Random();
                        int d = gen2.Next(1, 2);
                        if (d == 1)
                        {
                            map[curY, curX] = new Cell("Dleft");
                            curX -= 1;
                            last = "l";
                        }
                        else
                        {
                            map[curY, curX] = new Cell("Dright");
                            curX += 1;
                            last = "r";
                        }
                    }
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(map[i, 0].Line(j) + map[i, 1].Line(j) + map[i, 2].Line(j) + map[i, 3].Line(j));
                }
            }
        }

        public void PrintCell(int y, int x)
        {
            map[y, x].Print();
        }
    }
}
