using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NotaktoGame
{
    public class GameState
    {
        public List<NotaktoBoard> Boards;
        public Stack<Move> UndoStack;
        public Stack<Move> RedoStack;
        public bool IsPaused;
        public int CurrentPlayerIndex { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();


        public GameState()
        {
            Boards = new List<NotaktoBoard>();
            UndoStack = new Stack<Move>();
            RedoStack = new Stack<Move>();
            IsPaused = false;
        }
        

        public void PauseGame()
        {
            IsPaused = true;
            Console.WriteLine("Game paused. Press any key to resume.");
        }

        public void ResumeGame()
        {
            IsPaused = false;
            Console.WriteLine("Game resumed.");
        }

        public void SaveGame(string fileName)
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                });
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine("Game saved successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving game: " + e.Message);
            }
        }

        public static GameState LoadSavedGame(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                GameState loadedState = JsonSerializer.Deserialize<GameState>(jsonString, new JsonSerializerOptions
                {
                    IncludeFields = true
                });
                Console.WriteLine("Game loaded successfully.");
                return loadedState;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading game: " + e.Message);
                return null;
            }
        }
    }
}
