using NotaktoGame;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GameFramework
{
    public abstract class Game
    {
        protected GameState State;
        protected List<Player> Players;
        protected int CurrentPlayerIndex;

        protected Game()
        {
            State = new GameState();
            Players = new List<Player>();
            CurrentPlayerIndex = 0;
        }

        public virtual void Start()
        {
            SetupGame();
            while (!IsGameOver())
            {
                PlayTurn();
                CheckForCommands();
            }
            EndGame();
        }

        protected abstract void SetupGame();
        protected abstract void PlayTurn();
        public abstract bool IsGameOver();
        protected abstract void EndGame();

        protected virtual void CheckForCommands()
        {
            Console.WriteLine("Enter a command (undo, redo, save, load, pause) or press Enter to continue:");
            string command = Console.ReadLine().ToLower();

            switch (command)
            {
                case "undo": Undo(); break;
                case "redo": Redo(); break;
                case "save": State.SaveGame("game_save.dat"); break;
                case "load": LoadGame("game_save.dat"); break;
                case "pause": State.PauseGame(); break;
            }
        }

        protected virtual void Undo()
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

        protected virtual void Redo()
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

        protected virtual void LoadGame(string fileName)
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

        public static void DisplayHelp()
        {            
            Console.WriteLine("Board Game Instructions:");
           

        }
    }
}
