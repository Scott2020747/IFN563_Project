using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
    public class ComputerPlayer : Player
    {
        private Random random = new Random();

        public ComputerPlayer(string name, string type) : base(name, type) { }

        public override Move GetMove(List<Board> boards)
        {
            Console.WriteLine($"{PlayerName} is thinking...");
            Thread.Sleep(1000); // Simulate thinking

            int boardIndex = ChooseRandomAvailableBoard(boards);
            int row, col;

            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (!boards[boardIndex].IsPositionEmpty(row, col));

            return new Move(boardIndex, row, col, new Piece(PlayerType));
        }

        private int ChooseRandomAvailableBoard(List<Board> boards)
        {
            List<int> availableBoards = new List<int>();
            for (int i = 0; i < boards.Count; i++)
            {
                if (!boards[i].IsDead)
                {
                    availableBoards.Add(i);
                }
            }
            return availableBoards[random.Next(availableBoards.Count)];
        }
    }

}
