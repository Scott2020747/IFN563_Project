using System;
using System.Collections.Generic;

namespace NotaktoGame
{
    [Serializable]
    public class Move
    {
        // Information about the move
        public int BoardIndex;
        public int Row;
        public int Column;
        public Piece Piece;

        // Create a new move
        public Move(int boardIndex, int row, int column, Piece piece)
        {
            BoardIndex = boardIndex;
            Row = row;
            Column = column;
            Piece = piece;
        }

        // Undo this move
        public void Undo(List<NotaktoBoard> boards)
        {
            // Make sure the board exists
            if (BoardIndex >= 0 && BoardIndex < boards.Count)
            {
                NotaktoBoard board = boards[BoardIndex];

                // Remove the piece from the board
                board.Grid[Row, Column] = null;

                // Mark the board as not dead
                board.SetDeadState(false);

                // Check if the board is still in a winning state
                if (board.CheckForWin())
                {
                    board.SetDeadState(true);
                }
            }
        }

        // Redo this move
        public void Redo(List<NotaktoBoard> boards)
        {
            // Make sure the board exists
            if (BoardIndex >= 0 && BoardIndex < boards.Count)
            {
                NotaktoBoard board = boards[BoardIndex];

                // Put the piece back on the board
                board.PlacePiece(Row, Column, Piece);

                // Check if this creates a winning state
                if (board.CheckForWin())
                {
                    board.SetDeadState(true);
                }
            }
        }
    }
}
