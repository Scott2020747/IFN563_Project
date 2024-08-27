using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoGame
{
    public class Board
    {
        public Piece[,] Grid { get; private set; }
        public bool IsDead { get; private set; }

        public Board()
        {
            Grid = new Piece[3, 3];
            IsDead = false;
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
                IsDead = true;
            }
        }

        public bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((Grid[i, 0] != null && Grid[i, 0] == Grid[i, 1] && Grid[i, 1] == Grid[i, 2]) ||
                    (Grid[0, i] != null && Grid[0, i] == Grid[1, i] && Grid[1, i] == Grid[2, i]))
                {
                    return true;
                }
            }

            if ((Grid[0, 0] != null && Grid[0, 0] == Grid[1, 1] && Grid[1, 1] == Grid[2, 2]) ||
                (Grid[0, 2] != null && Grid[0, 2] == Grid[1, 1] && Grid[1, 1] == Grid[2, 0]))
            {
                return true;
            }

            return false;
        }
    }
}
