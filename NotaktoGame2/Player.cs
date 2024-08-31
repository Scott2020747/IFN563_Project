using System.Collections.Generic;

namespace NotaktoGame
{
    public abstract class Player
    {
        public string PlayerName { get; set; }
        public string PlayerType { get; set; }

        protected Player(string name, string type)
        {
            PlayerName = name;
            PlayerType = type;
        }

        public abstract Move GetMove(List<NotaktoBoard> boards);
    }
}
