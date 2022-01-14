using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    public enum Symbol { heart = 1, spade = 2, diamond = 3, club = 4, joker = 5 }
    public enum Number { A = 1, N2 = 2, N3 = 3, N4 = 4, N5 = 5, N6 = 6, N7 = 7, N8 = 8, N9 = 9, N10 = 10, J = 11, Q = 12, K = 13, Joker = 14 }
    public struct PlayCard//トランプカード一枚を表すクラス
    {
        public Symbol Symbol { get; }
        public Number Number { get; }

        public PlayCard(Symbol symbol, Number number)
        {
            this.Symbol = symbol;
            this.Number = number;
        }

    }
}
