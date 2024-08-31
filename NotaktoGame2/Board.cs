using System;
using System.Collections.Generic;

namespace NotaktoBoardGame
{
    [Serializable]
    public abstract class Board
    {
        public object[,] Grid { get; protected set; }
        public bool IsDead { get; protected set; }
        public int BoardId { get; protected set; }
        protected static List<Board> AllBoards { get; set; } = new List<Board>();

        protected Board(int boardId, int rows, int columns)
        {
            Grid = new object[rows, columns];
            IsDead = false;
            BoardId = boardId;
            AllBoards.Add(this);
        }

        public static void AddBoard(Board board)
        {
            bool boardExists = false;
            foreach (Board existingBoard in AllBoards)
            {
                if (existingBoard.BoardId == board.BoardId)
                {
                    boardExists = true;
                    break;
                }
            }
            if (!boardExists)
            {
                AllBoards.Add(board);
            }
        }

        public abstract void DisplayBoard();

        public virtual bool IsPositionEmpty(int row, int col)
        {
            return Grid[row, col] == null;
        }

        public abstract void PlacePiece(int row, int col, object piece);

        public abstract bool CheckForWin();

        public virtual void SetDeadState(bool isDead)
        {
            IsDead = isDead;
        }

        public static void ResetAllBoards()
        {
            AllBoards.Clear();
        }
    }
}
