using System;
using System.Collections.Generic;
using System.Numerics;
using GameFramework;

namespace NotaktoGame
{
    public class NotaktoGame : Game
    {
        public NotaktoGame() : base()
        {
            State.Boards = new List<NotaktoBoard>();
        }

        protected override void SetupGame()
        {
            Console.WriteLine("Welcome to Notakto!");
            SetupPlayers();
            NotaktoBoard.ResetAllBoards();
            State.Boards = new List<NotaktoBoard> {
                new NotaktoBoard(1),
                new NotaktoBoard(2),
                new NotaktoBoard(3)
            };
        }

        private void SetupPlayers()
        {
            Console.WriteLine("Enter 1 for Human vs Human, 2 for Human vs Computer:");
            string choice = Console.ReadLine();

            AddHumanPlayer("Player 1");

            if (choice == "1")
                AddHumanPlayer("Player 2");
            else
                AddComputerPlayer();
        }

        private void AddHumanPlayer(string playerPrompt)
        {
            Console.WriteLine($"Enter name for {playerPrompt}:");
            string playerName = Console.ReadLine();
            Players.Add(new HumanPlayer(playerName, "X"));
        }

        private void AddComputerPlayer()
        {
            Players.Add(new ComputerPlayer("Computer"));
        }

        protected override void PlayTurn()
        {
            HandlePausedState();
            DisplayGameState();
            Player currentPlayer = Players[CurrentPlayerIndex];
            Move move = currentPlayer.GetMove(State.Boards);
            ExecuteMove(move);
            CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
        }

        private void HandlePausedState()
        {
            if (State.IsPaused)
            {
                Console.ReadKey(true);
                State.ResumeGame();
            }
        }

        private void ExecuteMove(Move move)
        {
            State.Boards[move.BoardIndex].PlacePieceOnBoard(move);
            State.UndoStack.Push(move);
            State.RedoStack.Clear();
        }

        public override bool IsGameOver()
        {
            foreach (var board in State.Boards)
            {
                if (!board.IsDead)
                    return false;
            }
            return true;
        }

        private void DisplayGameState()
        {
            foreach (var board in State.Boards)
            {
                Console.WriteLine($"Board {board.BoardId}:");
                board.DisplayBoard();
                Console.WriteLine($"Status: {(board.IsDead ? "Dead" : "Alive")}");
                Console.WriteLine();
            }
        }

        protected override void Undo()
        {
            if (State.UndoStack.Count > 0)
            {
                Move move = State.UndoStack.Pop();
                move.Undo(State.Boards);
                State.RedoStack.Push(move);
                CurrentPlayerIndex = (CurrentPlayerIndex - 1 + Players.Count) % Players.Count;
                Console.WriteLine("Move undone.");
            }
            else
            {
                Console.WriteLine("No moves to undo.");
            }
        }

        protected override void Redo()
        {
            if (State.RedoStack.Count > 0)
            {
                Move move = State.RedoStack.Pop();
                move.Redo(State.Boards);
                State.UndoStack.Push(move);
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
                Console.WriteLine("Move redone.");
            }
            else
            {
                Console.WriteLine("No moves to redo.");
            }
        }

        protected override void LoadGame(string fileName)
        {
            GameState loadedState = GameState.LoadSavedGame(fileName);
            if (loadedState != null)
            {
                State = loadedState;
                NotaktoBoard.ResetAllBoards();
                foreach (var board in State.Boards)
                {
                    NotaktoBoard.AddBoard(board);
                }
                Console.WriteLine("Game loaded successfully.");
            }
        }

        protected override void EndGame()
        {
            DisplayGameState();
            AnnounceWinner();
        }

        private void AnnounceWinner()
        {
            int winnerIndex = (CurrentPlayerIndex + 1) % Players.Count;
            Console.WriteLine($"Game Over! {Players[winnerIndex].PlayerName} wins!");
        }

        public new static void DisplayHelp()
        {
            Console.WriteLine("Notakto Game Instructions:");
            Console.WriteLine("1. The game is played on three 3x3 boards.");
            Console.WriteLine("2. Players take turns placing X's on any empty square on any board.");
            Console.WriteLine("3. The goal is to avoid creating three X's in a row (horizontally, vertically, or diagonally).");
            Console.WriteLine("4. When a board has three X's in a row, it is dead and can no longer be played on.");
            Console.WriteLine("5. The player who creates three X's in a row on the last available board loses the game.");
            Console.WriteLine("6. To make a move, enter the board number (1-3) and the position (1-9) separated by a space.");
            Console.WriteLine("7. The positions are numbered from 1 to 9, starting from the top-left corner, moving right and down.");
            Console.WriteLine("8. Type 'undo' to undo the last move, 'redo' to redo an undone move.");
            Console.WriteLine("9. Type 'save' to save the game, 'load' to load a saved game.");
            Console.WriteLine("10. Type 'pause' to pause the game.");
            Console.WriteLine();
        }
    }
}
