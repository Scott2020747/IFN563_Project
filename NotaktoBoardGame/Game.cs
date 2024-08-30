using System;
using System.Collections.Generic;
using System.Linq;

namespace NotaktoGame
{
    public class Game
    {
        public GameState State { get; private set; }

        public Game()
        {
            State = new GameState();
            NotaktoBoard.ResetAllBoards();
            State.Boards = new List<NotaktoBoard> {
                new NotaktoBoard(1),
                new NotaktoBoard(2),
                new NotaktoBoard(3)
            };
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Notakto!");
            SetupPlayers();

            while (!IsGameOver())
            {
                HandlePausedState();
                DisplayGameState();
                PlayTurn();
                CheckForCommands();
            }

            DisplayGameState();
            AnnounceWinner();
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
            State.Players.Add(new HumanPlayer(playerName, "X"));
        }

        private void AddComputerPlayer()
        {
            State.Players.Add(new ComputerPlayer("Computer"));
        }

        private void HandlePausedState()
        {
            if (State.IsPaused)
            {
                Console.ReadKey(true);
                State.ResumeGame();
            }
        }

        private void PlayTurn()
        {
            Player currentPlayer = State.Players[State.CurrentPlayerIndex];
            Move move = currentPlayer.GetMove(State.Boards);
            ExecuteMove(move);
            State.CurrentPlayerIndex = (State.CurrentPlayerIndex + 1) % State.Players.Count;
        }

        private void ExecuteMove(Move move)
        {
            State.Boards[move.BoardIndex].PlacePieceOnBoard(move);
            State.UndoStack.Push(move);
            State.RedoStack.Clear();
        }

        public bool IsGameOver()
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

        private void CheckForCommands()
        {
            Console.WriteLine("Enter a command (undo, redo, save, load, pause) or press Enter to continue:");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "undo": Undo(); break;
                case "redo": Redo(); break;
                case "save": State.SaveGame("notakto_save.dat"); break;
                case "load": LoadGame("notakto_save.dat"); break;
                case "pause": State.PauseGame(); break;
            }
        }

        private void Undo()
        {
            if (State.UndoStack.Count > 0)
            {
                Move move = State.UndoStack.Pop();
                move.Undo(State.Boards);
                State.RedoStack.Push(move);
                State.CurrentPlayerIndex = (State.CurrentPlayerIndex - 1 + State.Players.Count) % State.Players.Count;
                Console.WriteLine("Move undone.");
            }
            else
            {
                Console.WriteLine("No moves to undo.");
            }
        }

        private void Redo()
        {
            if (State.RedoStack.Count > 0)
            {
                Move move = State.RedoStack.Pop();
                move.Redo(State.Boards);
                State.UndoStack.Push(move);
                State.CurrentPlayerIndex = (State.CurrentPlayerIndex + 1) % State.Players.Count;
                Console.WriteLine("Move redone.");
            }
            else
            {
                Console.WriteLine("No moves to redo.");
            }
        }

        private void LoadGame(string fileName)
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

        private void AnnounceWinner()
        {
            int winnerIndex = (State.CurrentPlayerIndex + 1) % State.Players.Count;
            Console.WriteLine($"Game Over! {State.Players[winnerIndex].PlayerName} wins!");
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
            Console.WriteLine("8. Type 'undo' to undo the last move, 'redo' to redo an undone move.");
            Console.WriteLine("9. Type 'save' to save the game, 'load' to load a saved game.");
            Console.WriteLine("10. Type 'pause' to pause the game.");
            Console.WriteLine();
        }
    }
}
