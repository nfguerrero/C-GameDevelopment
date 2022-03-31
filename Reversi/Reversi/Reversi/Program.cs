using System;
using System.Collections.Generic;

namespace Reversi
{
    class Program
    {
        static int dif;     //difficulty
        static bool first;  //true if player chose to go first
        static Board board; //board object

        /*
         * Main Function 
        */
        static void Main(string[] args)
        {
            Initialize();

            board = new Board();

            Play();
        }

        /*
         * Initialize difficulty and whether player wants to go first or not 
        */
        static void Initialize()
        {
            bool lck = true;
            int k = 0;
            while (lck)
            {
                Console.Write("Difficulty(1-10): ");
                string key = Console.ReadLine();

                if (Int32.TryParse(key, out k))
                {
                    if (k >= 1 && k <= 10)
                    {
                        lck = false;
                    }
                    else
                    {
                        Console.WriteLine("Must be a number 1-10" + System.Environment.NewLine);
                    }
                }
                else
                {
                    Console.WriteLine("Must be a number 1-10" + System.Environment.NewLine);
                }
            }
            dif = k;

            string s = "";
            while (s != "y" && s != "n")
            {
                Console.Write(System.Environment.NewLine + "Would you like to go first?(y/n): ");
                s = Console.ReadLine();
            }
            if (s == "y")
            {
                first = true;
            }
            else
            {
                first = false;
            }
        }

        /*
         * Main game loop
        */
        static void Play()
        {
            board.Print();
            //Handle if player chose to go first
            if (first)
            {
                PlayerMove();
                board.Print();
            }
            //Main loop
            while (true)
            {                                
                bool comp = ComputerMove();
                if (comp) { board.Print(); }
                bool player = PlayerMove();
                if (player) { board.Print(); }
                if (!comp && !player)
                {
                    break;
                }                
            }
            //Handle game over scenario
            int x = board.GetScore("X");
            int o = board.GetScore("O");
            if (x > 32)
            {
                Console.WriteLine();
                Console.Write("You are the Winner! ");
            }
            else if (o > 32)
            {
                Console.WriteLine();
                Console.Write("The AI bested you this time! ");
            }
            else
            {
                Console.WriteLine();
                Console.Write("You went head to head and tied the AI! ");
            }
            Console.WriteLine("(X: " + x + " - " + "O: " + o + ")");
        }

        /*
         * Handle the player's move
        */ 
        static bool PlayerMove()
        {
            string move = "";
            List<Pair> exists = board.ValidMoves("X", false);
            //Loop for correct formatting of player's move from input only if there exists a valid move
            while (exists.Count > 0)
            {
                Console.Write("Your move(x,y): ");
                move = Console.ReadLine();
                move = move.Replace(" ", "");
                move = move.Replace(",", "");
                Console.WriteLine();
                //Send player move to board if it was valid
                if (CheckMove(move))
                {
                    BoardState bs = board.CheckMove("X", (int)Char.GetNumericValue(move[1]) - 1, (int)Char.GetNumericValue(move[0]) - 1);
                    if (bs.total > 0)
                    {
                        board = bs.board;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Move");
                    }
                }
            }
            return exists.Count > 0;
        }

        /*
         * Handle computer's move
        */ 
        static bool ComputerMove()
        {
            List<Pair> moves = board.ValidMoves("O", false);
            if (moves.Count > 0)
            {
                BoardState newBS = Minimax("O", board, dif, int.MaxValue, int.MaxValue);
                board = board.CheckMove("O", newBS.move.x, newBS.move.y).board;
                Console.WriteLine("Comp: (" + (newBS.move.y + 1) + ", " + (newBS.move.x + 1) + ")");
            }
            return moves.Count > 0;
        }

        /*
         * Main algorithm for computer to choose best move based on difficulty 'd'  - Recursive function
        */ 
        static BoardState Minimax(string p, Board b, int d, int alpha, int beta)
        {            
            if (d == 0)
            {
                return new BoardState(b, b.GetScore("O"));
            }
            //Max portion
            if (p == "O")
            {
                BoardState max = new BoardState(b, -100);
                List<Pair> moves = b.ValidMoves("O", true);
                Pair move = new Pair(0, 0);
                for (int i = 0; i < moves.Count; i++)
                {
                    Board newBoard = b.CheckMove("O", moves[i].x, moves[i].y).board;
                    BoardState bs = Minimax("X", newBoard, d - 1, alpha, beta);                    
                    if (bs.total > max.total)
                    {
                        max = bs;
                        move.x = moves[i].x;
                        move.y = moves[i].y;
                    }
                    alpha = Math.Max(alpha, bs.total);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }
                max.move = move;                
                return max;
            }
            //Min portion
            else
            {
                BoardState min = new BoardState(b, 100);
                List<Pair> moves = b.ValidMoves("X", true);
                Pair move = new Pair(0, 0);
                for (int i = 0; i < moves.Count; i++)
                {
                    Board newBoard = b.CheckMove("X", moves[i].x, moves[i].y).board;
                    BoardState bs = Minimax("O", newBoard, d - 1, alpha, beta);
                    if (bs.total < min.total)
                    {
                        min = bs;
                        move.x = moves[i].x;
                        move.y = moves[i].y;
                    }
                    beta = Math.Min(beta, bs.total);
                    if (beta <= alpha)
                    {
                        break;
                    }
                }                
                min.move = move;
                return min;
            }
        }

        /*
         * Check if formatting of player inputted move is correct
        */ 
        static bool CheckMove(string move)
        {            
            if (move.Length != 2)
            {
                Console.WriteLine("Invalid format");
                return false;
            }
            bool valid = true;
            if (Int32.TryParse(move[0] + "", out int x))
            {
                if (x > 8 || x < 1)
                {
                    Console.WriteLine("x must be 1-8");
                    valid = false;
                }
            }
            if (Int32.TryParse(move[1] + "", out int y))
            {
                if (y > 8 || y < 1)
                {
                    Console.WriteLine("y must be 1-8");
                    valid = false;
                }
            }
            return valid;
        }
    }
}
