using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
    public abstract class Player
    {
        public string PlayerName { get; set; }
        public string PlayerType { get; set; }

        public Player(string name, string type)
        {
            PlayerName = name;
            PlayerType = type;
        }

        public abstract Move GetMove(List<Board> boards);
    }

}
