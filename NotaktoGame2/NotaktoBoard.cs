using NotaktoBoardGame;
using System;

namespace NotaktoGame
{
    [Serializable]
    public class NotaktoBoard : Board
    {
        // Constructor
        public NotaktoBoard(int boardId) : base(boardId, 3, 3)
        {
        }

        // Display the board
        public override void DisplayBoard()
        {
            // Loop through each row
            for (int row = 0; row < 3; row++)
            {
                // Loop through each column in the current row
                for (int col = 0; col < 3; col++)
                {
                    // Get the piece at the current position
                    Piece piece = Grid[row, col] as Piece;

                    // If there's a piece, show its symbol; otherwise, show a space
                    if (piece != null)
                    {
                        Console.Write(piece.Symbol);
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    // Add a separator between columns (except for the last column)
                    if (col < 2)
                    {
                        Console.Write("|");
                    }
                }

                // Move to the next line after printing a row
                Console.WriteLine();

                // Add a horizontal line between rows (except for the last row)
                if (row < 2)
                {
                    Console.WriteLine("-----");
                }
            }
        }


        // Place a piece on the board
        public override void PlacePiece(int row, int col, object piece)
        {
            if (piece is Piece notaktoPiece)
            {
                Grid[row, col] = notaktoPiece;
                if (CheckForWin())
                {
                    SetDeadState(true);
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid piece type for Notakto game.");
            }
        }

        // Check if there's a winning line on the board
        public override bool CheckForWin()
        {
            // Check rows and columns
            for (int i = 0; i < 3; i++)
            {
                if (IsLine(i, 0, i, 1, i, 2) || IsLine(0, i, 1, i, 2, i))
                {
                    return true;
                }
            }

            // Check diagonals
            if (IsLine(0, 0, 1, 1, 2, 2) || IsLine(0, 2, 1, 1, 2, 0))
            {
                return true;
            }

            return false;
        }

        // Helper method to check if three positions form a line of X's
        private bool IsLine(int r1, int c1, int r2, int c2, int r3, int c3)
        {
            // Check the first position
            bool firstIsX = IsPieceX(r1, c1);

            // Check the second position
            bool secondIsX = IsPieceX(r2, c2);

            // Check the third position
            bool thirdIsX = IsPieceX(r3, c3);

            // Return true if all three positions are X, otherwise false
            return firstIsX && secondIsX && thirdIsX;
        }

        // Helper method to check if a piece at a given position is X
        private bool IsPieceX(int row, int col)
        {
            // Get the piece at the given position
            Piece piece = Grid[row, col] as Piece;

            // Check if the piece exists and its symbol is "X"
            if (piece != null && piece.Symbol == "X")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Set the board as dead (won)
        public override void SetDeadState(bool isDead)
        {
            base.SetDeadState(isDead);
            if (isDead)
            {
                Console.WriteLine($"Board {BoardId} is now dead!");

                // Fill empty spaces with X
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

        // Place a piece using a Move object
        public void PlacePieceOnBoard(Move move)
        {
            PlacePiece(move.Row, move.Column, move.Piece);
        }
    }
}
