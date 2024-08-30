using System;
using System.Collections.Generic;

namespace NotaktoGame
{
    public class ComputerPlayer : Player
    {
        private Random random;

        public ComputerPlayer(string name) : base(name, "Computer")
        {
            random = new Random();
        }

        public override Move GetMove(List<NotaktoBoard> boards)
        {
            // Find all available moves
            List<Move> availableMoves = new List<Move>();

            // Check each board
            for (int boardIndex = 0; boardIndex < boards.Count; boardIndex++)
            {
                NotaktoBoard board = boards[boardIndex];

                // Skip dead boards
                if (board.IsDead)
                {
                    continue;
                }

                // Check each cell on the board
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        // If the cell is empty, it's an available move
                        if (board.Grid[row, col] == null)
                        {
                            availableMoves.Add(new Move(boardIndex, row, col, new Piece("X")));
                        }
                    }
                }
            }

            // Choose a random move from available moves
            Move chosenMove = availableMoves[random.Next(availableMoves.Count)];

            // Print the chosen move
            Console.WriteLine($"{PlayerName} places X on Board {chosenMove.BoardIndex + 1} at position ({chosenMove.Row + 1}, {chosenMove.Column + 1})");

            return chosenMove;
        }
    }
}
