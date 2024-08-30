using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NotaktoGame
{
    public class GameState
    {
        // Game components
        public List<NotaktoBoard> Boards;
        public List<Player> Players;
        public int CurrentPlayerIndex;
        public Stack<Move> UndoStack;
        public Stack<Move> RedoStack;
        public bool IsPaused;

        // Constructor
        public GameState()
        {
            Boards = new List<NotaktoBoard>();
            Players = new List<Player>();
            CurrentPlayerIndex = 0;
            UndoStack = new Stack<Move>();
            RedoStack = new Stack<Move>();
            IsPaused = false;
        }

        // Pause the game
        public void PauseGame()
        {
            IsPaused = true;
            Console.WriteLine("Game paused. Press any key to resume.");
        }

        // Resume the game
        public void ResumeGame()
        {
            IsPaused = false;
            Console.WriteLine("Game resumed.");
        }

        // Save the game to a file
        public void SaveGame(string fileName)
        {
            try
            {
                // Convert the game state to JSON
                string jsonString = JsonSerializer.Serialize(this, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    IncludeFields = true
                });

                // Write JSON to file
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine("Game saved successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving game: " + e.Message);
            }
        }

        // Load a game from a file
        public static GameState LoadSavedGame(string fileName)
        {
            try
            {
                // Read JSON from file
                string jsonString = File.ReadAllText(fileName);

                // Convert JSON back to GameState object
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
