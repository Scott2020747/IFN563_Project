using System;

namespace NotaktoGame
{
    [Serializable]
    public class Piece
    {
        public string Symbol { get; set; }

        public Piece(string symbol = "X")
        {
            Symbol = symbol;
        }
    }
}
