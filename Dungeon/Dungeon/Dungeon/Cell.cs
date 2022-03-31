using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Cell
    {
        private string[,] pixels; //Actual pixels of defined cell

        public Cell(String type)
        {
            pixels = new string[5, 10];

            if (type == "space")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {                       
                        pixels[i, j] = ".";
                    }
                }
                for (int i = 1; i < 4; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        pixels[i, j] = " ";
                    }
                }
            }
            else if (type == "Eleft")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j <10; j++)
                    {
                        if (i > 0 && i < 4 && j < 7)
                        {
                           pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Eright")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i > 0 && i < 4 && j > 2)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Edown")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (j > 2 && j < 7 && i > 1)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Lleft")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i > 0 && i < 4)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Ldown")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i > 0 && i < 4 && j < 7) || (i == 4 && j > 3 && j < 6))
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Rright")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i > 0 && i < 4)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Rdown")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i > 0 && i < 4 && j > 2) || (i == 4 && j > 3 && j < 6))
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Ddown")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i > 0 && i < 4 && j > 1 && j < 8) || ((i == 0 || i == 4) && j > 3 && j < 6))
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Dleft")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i > 0 && i < 4 && j < 8) || (i == 0 && j > 3 && j < 6))
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Dright")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if ((i > 0 && i < 4 && j > 1) || (i == 0 && j > 3 && j < 6))
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Lend")
            {                
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i > 0 && i < 4 && j < 7)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
            else if (type == "Rend")
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (i > 0 && i < 4 && j > 2)
                        {
                            pixels[i, j] = " ";
                        }
                        else
                        {
                            pixels[i, j] = "X";
                        }
                    }
                }
            }
        }

        public string Line(int i)
        {
            return pixels[i, 0] + pixels[i, 1] + pixels[i, 2] + pixels[i, 3] + pixels[i, 4] + pixels[i, 5] + pixels[i, 6] + pixels[i, 7] + pixels[i, 8] + pixels[i, 9];
        }

        public void Print()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(pixels[i, j]);
                }
                Console.WriteLine("");
            }
        }
    }
}
