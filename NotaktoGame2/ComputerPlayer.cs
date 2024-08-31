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
            List<Move> availableMoves = new List<Move>();

            for (int boardIndex = 0; boardIndex < boards.Count; boardIndex++)
            {
                NotaktoBoard board = boards[boardIndex];

                if (board.IsDead)
                {
                    continue;
                }

                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        if (board.IsPositionEmpty(row, col))
                        {
                            availableMoves.Add(new Move(boardIndex, row, col, new Piece("X")));
                        }
                    }
                }
            }

            Move chosenMove = availableMoves[random.Next(availableMoves.Count)];

            Console.WriteLine($"{PlayerName} places X on Board {chosenMove.BoardIndex + 1} at position ({chosenMove.Row + 1}, {chosenMove.Column + 1})");

            return chosenMove;
        }
    }
}
