using System;
using System.Collections.Generic;

namespace Reversi
{
    class Board
    {
        private string[,] board; //Array representation of the board
        private Pair[] pairs;    //Array of all 8 directions

        /*
         * Board constructor sets initial configuration and a list of directions in 'pairs'
        */ 
        public Board()
        {
            board = new string[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = " ";
                }
            }
            board[3, 3] = "X";
            board[4, 4] = "X";
            board[3, 4] = "O";
            board[4, 3] = "O";

            pairs = new Pair[8];
            pairs[0] = new Pair(-1, -1);
            pairs[1] = new Pair(0, -1);
            pairs[2] = new Pair(1, -1);
            pairs[3] = new Pair(-1, 0);
            pairs[4] = new Pair(1, 0);
            pairs[5] = new Pair(-1, 1);
            pairs[6] = new Pair(0, 1);
            pairs[7] = new Pair(1, 1);
        }

        /*
         * Check if incoming move is valid for the 'player' given an 'x' and 'y'
        */
        public BoardState CheckMove(string player, int x, int y)
        {
            Board newBoard = new Board();
            newBoard.Copy(board);
            int total = 0;
            //Checks if move is an open space
            if (board[x, y] == " ")
            {
                for (int i = 0; i < 8; i++)
                {
                    //Checks if pair is inside playing area
                    if (pairs[i].x + x >= 0 && pairs[i].x + x <= 7 && pairs[i].y + y >= 0 && pairs[i].y + y <= 7)
                    {
                        total += CheckDirection(newBoard, pairs[i], player, x, y); //Adds the total number of chips that would be flipped in one of the 8 directions if this move is played
                    }
                }
            }

            //Place move if it was valid
            if (total > 0)
            {
                newBoard.Place(player, x, y);                
            }
            BoardState bs = new BoardState(newBoard, total);
            return bs;
        }

        /*
         * Helper method to CheckMove() to check each of the 8 directions 
        */ 
        private int CheckDirection(Board b, Pair p, string player, int x, int y)
        {
            int total = 0;
            bool valid = false;
            //Switch player to look for opposite chips to flip
            if (player == "X")
            {
                player = "O";
            }
            else
            {
                player = "X";
            }

            //While the next step in the direction is in the playing area, continue search
            while(p.x + x >= 0 && p.x + x <= 7 && p.y + y >= 0 && p.y + y <= 7)
            {
                x += p.x;
                y += p.y;

                if (board[x, y] == player)
                {
                    total += 1;
                }
                else if (board[x, y] != " " && total > 0)
                {
                    valid = true;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (valid) //After search, go back and flip all chips in the line
            {
                for (int i = 0; i < total; i++)
                {                    
                    x -= p.x;
                    y -= p.y;
                    b.Flip(x, y);
                }
                return total;
            }
            return 0;
        }

        /*
         * Returns a List of all valid moves given a player, can choose to return all valid moves or just serach if at least one exists
        */ 
        public List<Pair> ValidMoves(string player, bool allMoves)
        {
            List<Pair> moves = new List<Pair>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (CheckMove(player, i, j).total > 0)
                    {
                        moves.Add(new Pair(i, j));
                        if (!allMoves)
                        {
                            return moves;
                        }
                    }
                }
            }
            return moves;
        }

        /*
         * Get the score of a player
        */ 
        public int GetScore(String player)
        {
            int score = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == player)
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        /*
         * Public method to place a chip down (was for testing)
        */ 
        public void Place(string p, int x, int y)
        {
            board[x, y] = p;
        }

        /*
         * Helper method to flip chip given 'x' and 'y'
        */ 
        private void Flip(int x, int y)
        {
            if (board[x, y] == "X")
            {
                board[x, y] = "O";
            }
            else if (board[x, y] == "O")
            {
                board[x, y] = "X";
            }
        }

        /*
         * Copy board based on incoming array representation
        */ 
        public void Copy(string[,] b)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = b[i, j];
                }
            }
        }

        /*
         * Print the board to console 
        */ 
        public void Print()
        {
            Console.WriteLine();
            Console.WriteLine("          X: " + GetScore("X") + "        O: " + GetScore("O"));
            Console.WriteLine();
            Console.WriteLine("    1   2   3   4   5   6   7   8");
            Console.Write("  ");
            for (int j = 0; j < 32; j++)
            {
                Console.Write("-");
            }
            Console.WriteLine("-");
            for (int i = 0; i < 8; i++)
            {
                Console.Write(i + 1 + " ");
                for (int j = 0; j < 8; j++)
                {
                    Console.Write("| " + board[i,j] + " ");
                }
                Console.WriteLine("|");
                Console.Write("  ");
                for (int j = 0; j < 32; j++)
                {
                    Console.Write("-");
                }
                Console.WriteLine("-");
            }
            Console.WriteLine();
        }
    }
}
