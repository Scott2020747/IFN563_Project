using System;
using System.Collections.Generic;

namespace NotaktoGame
{
    [Serializable]
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

        public void Undo(List<NotaktoBoard> boards)
        {
            if (BoardIndex >= 0 && BoardIndex < boards.Count)
            {
                NotaktoBoard board = boards[BoardIndex];
                board.Grid[Row, Column] = null;
                board.SetDeadState(false);
                if (board.CheckForWin())
                {
                    board.SetDeadState(true);
                }
            }
        }

        public void Redo(List<NotaktoBoard> boards)
        {
            if (BoardIndex >= 0 && BoardIndex < boards.Count)
            {
                NotaktoBoard board = boards[BoardIndex];
                board.PlacePiece(Row, Column, Piece);
                if (board.CheckForWin())
                {
                    board.SetDeadState(true);
                }
            }
        }
    }
}
