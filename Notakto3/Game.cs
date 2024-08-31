using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoGame
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Board> Boards { get; set; }
        public Piece Piece { get; set; }
        private int currentPlayerIndex;

        public Game()
        {
            Boards = new List<Board> { new Board(), new Board(), new Board() };
            Piece = new Piece();
            Players = new List<Player>();
            currentPlayerIndex = 0;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Notakto!");
            SetupPlayers();

            while (!IsGameOver())
            {
                DisplayGameState();
                Player currentPlayer = Players[currentPlayerIndex];
                Move move = currentPlayer.GetMove(Boards);
                ExecuteMove(move);
                currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
            }

            DisplayGameState();
            Console.WriteLine($"Game Over! {Players[(currentPlayerIndex + 1) % Players.Count].PlayerName} wins!");
        }

        private void SetupPlayers()
        {
            Console.WriteLine("Enter 1 for Human vs Human, 2 for Human vs Computer:");
            string choice = Console.ReadLine();
            Players.Add(new HumanPlayer("Player 1", "X"));
            if (choice == "1")
            {
                Players.Add(new HumanPlayer("Player 2", "X"));
            }
            else
            {
                Players.Add(new ComputerPlayer("Computer", "X"));
            }
        }

        private void ExecuteMove(Move move)
        {
            Boards[move.BoardIndex].PlacePieceOnBoard(move);
        }

        public bool IsGameOver()
        {
            return Boards.TrueForAll(board => board.CheckForWin());
        }

        private void DisplayGameState()
        {
            for (int i = 0; i < Boards.Count; i++)
            {
                Console.WriteLine($"Board {i + 1}:");
                Boards[i].DisplayBoard();
                Console.WriteLine();
            }
        }

        public static void DisplayHelp()
        {
            Console.WriteLine("Notakto - Game Instructions:");
            Console.WriteLine("1. The game is played on three 3x3 boards.");
            Console.WriteLine("2. Players take turns placing X's on any empty square on any board.");
            Console.WriteLine("3. The goal is to avoid creating three X's in a row (horizontally, vertically, or diagonally).");
            Console.WriteLine("4. When a board has three X's in a row, it is dead and can no longer be played on.");
            Console.WriteLine("5. The player who creates three X's in a row on the last available board loses the game.");
            Console.WriteLine("6. To make a move, enter the board number (1-3) and the position (1-9) separated by a space.");
            Console.WriteLine("7. The positions are numbered from 1 to 9, starting from the top-left corner, moving right and down.");
            Console.WriteLine();
        }
    }
}