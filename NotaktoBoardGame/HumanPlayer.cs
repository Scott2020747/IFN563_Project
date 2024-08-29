using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, string type) : base(name, type) { }

        public override Move GetMove(List<Board> boards)
        {
            int boardIndex, position;

            do
            {
                Console.Write($"{PlayerName}, enter your move (board [1-3] position [1-9]): ");
                string[] input = Console.ReadLine().Split();

                boardIndex = int.Parse(input[0]) - 1;
                position = int.Parse(input[1]) - 1;
            } while (!IsValidMove(boards, boardIndex, position));

            // Create a new Move object with the provided indices
            return new Move(boardIndex, position / 3, position % 3, new Piece(PlayerType));
        }


        private bool IsValidMove(List<Board> boards, int boardIndex, int position)
        {
            if (boardIndex < 0 || boardIndex >= boards.Count || position < 0 || position >= 9)
            {
                return false;
            }
            if (boards[boardIndex].IsDead)
            {
                Console.WriteLine("This board is dead. Choose another board.");
                return false;
            }
            return boards[boardIndex].IsPositionEmpty(position / 3, position % 3);
        }
    }

}
