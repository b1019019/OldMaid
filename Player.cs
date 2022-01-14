using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    class Player
    {
        //プレイヤーのメンバーを考える
        public int Number { get;}
        public string Name { get;}
        public bool Auto { get;}
        public int Ranking { get; set; } = 0;
        public Deck Hand { get; }
        public Player(int number, string name, bool auto)
        {
            Hand = new Deck();
            Number = number;
            Name = name;
            Auto = auto;
        }
        public void HandDrawMulti(Player givePlayer, int drawTimes) => Hand.DrawMulti(givePlayer.Hand, drawTimes);
        public void HandDrawMulti(Deck giveDeck, int drawTimes) => Hand.DrawMulti(giveDeck, drawTimes);
        public PlayCard HandDrawSelect(Player givePlayer, int cardNum) => Hand.DrawSelect(givePlayer.Hand, cardNum);
        public PlayCard HandDrawSelect(Deck giveDeck, int cardNum) => Hand.DrawSelect(giveDeck, cardNum);
        public PlayCard HandDrawTop(Player givePlayer) => Hand.DrawTop(givePlayer.Hand);
        public PlayCard HandDrawTop(Deck giveDeck) => Hand.DrawTop(giveDeck);
        public List<PlayCard> HandDiscardSameNums() => Hand.DiscardSameNums();
        public int HandCount() => Hand.CardCount();
        public IReadOnlyList<PlayCard> HandCards() => Hand.playCardsRO;




    }
}
