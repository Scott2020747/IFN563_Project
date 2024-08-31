using System;
using System.Collections.Generic;

namespace NotaktoGame
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, string type) : base(name, type) { }

        public override Move GetMove(List<NotaktoBoard> boards)
        {
            while (true)
            {
                Console.Write($"{PlayerName}, enter your move (board [1-3] position [1-9]): ");
                string[] input = Console.ReadLine().Split();

                if (input.Length != 2 || !int.TryParse(input[0], out int boardNumber) || !int.TryParse(input[1], out int position))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                int boardIndex = boardNumber - 1;
                int row = (position - 1) / 3;
                int column = (position - 1) % 3;

                if (IsValidMove(boards, boardIndex, row, column))
                {
                    return new Move(boardIndex, row, column, new Piece(PlayerType));
                }
            }
        }

        private bool IsValidMove(List<NotaktoBoard> boards, int boardIndex, int row, int column)
        {
            if (boardIndex < 0 || boardIndex >= boards.Count)
            {
                Console.WriteLine("Invalid board number. Please choose 1, 2, or 3.");
                return false;
            }

            if (boards[boardIndex].IsDead)
            {
                Console.WriteLine("This board is dead. Choose another board.");
                return false;
            }

            if (!boards[boardIndex].IsPositionEmpty(row, column))
            {
                Console.WriteLine("This position is already occupied. Choose another position.");
                return false;
            }

            return true;
        }
    }
}
