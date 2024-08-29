using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
    public class Board
    {
        public Piece[,] Grid { get; private set; }
        public bool IsDead { get; private set; }
        public int BoardId { get; private set; }

        public Board(int boardId)
        {
            Grid = new Piece[3, 3];
            IsDead = false;
            BoardId = boardId;
        }

        public void DisplayBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(Grid[row, col]?.Symbol ?? " ");
                    if (col < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (row < 2) Console.WriteLine("-+-+-");
            }
        }

        public bool IsPositionEmpty(int row, int col)
        {
            return Grid[row, col] == null;
        }

        public void PlacePieceOnBoard(Move move)
        {
            Grid[move.Row, move.Column] = move.Piece;
            if (CheckForWin())
            {
                SetDeadState(true);
            }
        }

        public bool CheckForWin()
        {
            // Check rows, columns, and diagonals
            for (int i = 0; i < 3; i++)
            {
                if ((Grid[i, 0]?.Symbol == "X" && Grid[i, 1]?.Symbol == "X" && Grid[i, 2]?.Symbol == "X") ||
                    (Grid[0, i]?.Symbol == "X" && Grid[1, i]?.Symbol == "X" && Grid[2, i]?.Symbol == "X"))
                {
                    return true;
                }
            }


            // Check diagonals
            if ((Grid[0, 0] != null && Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2]) ||
                (Grid[0, 2] != null && Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0]))
            {
                return true;
            }

            return false;
        }

        public void SetDeadState(bool isDead)
        {
            IsDead = isDead;
            if (isDead)
            {
                Console.WriteLine($"Board {BoardId} is now dead!");
                FillDeadBoard();
            }
        }

        private void FillDeadBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Grid[row, col] == null)
                    {
                        Grid[row, col] = new Piece("X");
                    }
                }
            }
        }
    }

}
