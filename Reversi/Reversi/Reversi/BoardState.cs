using System;
using System.Collections.Generic;
using System.Text;

namespace Reversi
{
    class BoardState
    {
        public Board board; //Board object
        public int total;   //Used as chips turned after a move, or the score of a player after a move
        public Pair move;   //Move that was just done to result in the current BoardState

        public BoardState(Board b, int t)
        {
            board = b;
            total = t;
        }
    }
}
