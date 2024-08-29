using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace NotaktoBoardGame
{
        [Serializable]
        public class GameState
        {
            public List<Board> Boards { get; set; }
            public List<Player> Players { get; set; }
            public int CurrentPlayerIndex { get; set; }
            public bool IsPaused { get; set; }

            [XmlIgnore]
            public Stack<Move> UndoStack { get; set; }
            [XmlIgnore]
            public Stack<Move> RedoStack { get; set; }

            [XmlArray("UndoMoves")]
            public Move[] UndoMoves
            {
            
                get
                {
                    // If UndoStack is not null, convert it to an array; otherwise, return an empty array
                    if (UndoStack != null)
                        return UndoStack.ToArray();
                    else
                        return Array.Empty<Move>();
                }
                set
                {
                    // Set UndoStack to a new stack containing the provided value
                    UndoStack = new Stack<Move>(value);
                }
            }

            [XmlArray("RedoMoves")]
        
            public Move[] RedoMoves
            {
                get
                {
                    // If RedoStack is not null, convert it to an array; otherwise, return an empty array
                    if (RedoStack != null)
                        return RedoStack.ToArray();
                    else
                        return Array.Empty<Move>();
                }
                set
                {
                    // Set RedoStack to a new stack containing the provided value
                    RedoStack = new Stack<Move>(value);
                }
            }


        public GameState()
            {
                Boards = new List<Board>();
                Players = new List<Player>();
                UndoStack = new Stack<Move>();
                RedoStack = new Stack<Move>();
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
                var serializer = new XmlSerializer(typeof(GameState));
                using var fs = new FileStream(fileName, FileMode.Create);
                serializer.Serialize(fs, this);
                Console.WriteLine($"Game saved to {fileName}");
            }

            public static GameState LoadSavedGame(string fileName)
            {
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Save file not found.");
                    return null;
                }

                var serializer = new XmlSerializer(typeof(GameState));
                using var fs = new FileStream(fileName, FileMode.Open);
                return (GameState)serializer.Deserialize(fs);
            }

            public void AddMove(Move move)
            {
                UndoStack.Push(move);
                RedoStack.Clear();
            }

            public Move UndoMove() => UndoStack.Count > 0 ? MoveStack(UndoStack, RedoStack) : null;

            public Move RedoMove() => RedoStack.Count > 0 ? MoveStack(RedoStack, UndoStack) : null;

            private Move MoveStack(Stack<Move> fromStack, Stack<Move> toStack)
            {
                var move = fromStack.Pop();
                toStack.Push(move);
                return move;
            }

            public bool CanUndo() => UndoStack.Count > 0;

            public bool CanRedo() => RedoStack.Count > 0;

            public void SwitchPlayer() => CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;

            public Player GetCurrentPlayer() => Players[CurrentPlayerIndex];

            public bool IsGameOver() => Boards.TrueForAll(board => board.IsDead);
        }
    }


