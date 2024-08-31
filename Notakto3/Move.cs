using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoGame
{
    public class Move
    {
        public int BoardIndex { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Piece Piece { get; set; }

        public Move(int boardIndex, int row, int column, Piece piece)
        {
            BoardIndex = boardIndex;
            Row = row;
            Column = column;
            Piece = piece;
        }
    }
}
