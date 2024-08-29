using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
        [Serializable]
        public class Move
        {
            public int BoardIndex { get; }
            public int Row { get; }
            public int Column { get; }
            public Piece Piece { get; }

            public Move(int boardIndex, int row, int column, Piece piece)
            {
                BoardIndex = boardIndex;
                Row = row;
                Column = column;
                Piece = piece;
            }

            public void Undo(List<Board> boards)
            {
                if (IsValidBoardIndex(boards))
                {
                    Board board = boards[BoardIndex];
                    board.Grid[Row, Column] = null;
                    UpdateBoardState(board);
                }
            }

            public void Redo(List<Board> boards)
            {
                if (IsValidBoardIndex(boards))
                {
                    Board board = boards[BoardIndex];
                    board.Grid[Row, Column] = Piece;
                    UpdateBoardState(board);
                }
            }

            private bool IsValidBoardIndex(List<Board> boards)
            {
                return BoardIndex >= 0 && BoardIndex < boards.Count;
            }

            private void UpdateBoardState(Board board)
            {
                board.SetDeadState(board.CheckForWin());
            }
        }
    }


