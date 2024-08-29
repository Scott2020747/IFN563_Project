using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaktoBoardGame
{
        public class Piece
        {
            public string Symbol { get; set; }

            public Piece(string symbol = "X")
            {
                Symbol = symbol;
            }
        }
    
}

