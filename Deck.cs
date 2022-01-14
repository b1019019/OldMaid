using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    public class Deck//トランプカードの束
    {
        //playingCardsのアクセサーについて考える
        private List<PlayCard> playCards;
        public IReadOnlyList<PlayCard> playCardsRO;

        public Deck()
        {
            playCards = new List<PlayCard>();
            playCardsRO = playCards.AsReadOnly();
        }
        public int CardCount() => playCards.Count;
        public static Deck MainDeck(int jokerNum)//ジョーカーの数は0~2
        {
            Deck d = new Deck();
            //デッキにカードを充填
            for (int number = 1; number <= 13; number++)
            {
                for (int symbol = 1; symbol <= 4; symbol++)
                {
                    d.playCards.Add(new PlayCard((Symbol)symbol, (Number)number));
                }

            }
            if (jokerNum == 1)
            {
                d.playCards.Add(new PlayCard((Symbol)5, (Number)14));
            }
            if (jokerNum > 1)
            {
                d.playCards.Add(new PlayCard((Symbol)5, (Number)14));
            }
            //shuffle 
            d.playCards = d.playCards.OrderBy(i => Guid.NewGuid()).ToList();
            //コレクション.OrderBy(昇順ソートに使う値をラムダ式で指定) 指定した値でソート
            //Guidというのはランダムに世界中で一意な数字を生成、
            //なのでGuidの大きさで並び変えればランダムな順番になる。
            return d;
        }
        public void DrawMulti(Deck giveDeck, int drawTimes)//リストの上から指定数引く
        {
            while (0 < drawTimes)
            {
                DrawTop(giveDeck);
                drawTimes--;
            }
        }

        public PlayCard DrawSelect(Deck giveDeck, int cardNum)//狙って一枚引く
        {
            List<PlayCard> givePlayCards = giveDeck.playCards;
            PlayCard giveCard = givePlayCards[cardNum];
            playCards.Add(givePlayCards[cardNum]);
            givePlayCards.RemoveAt(cardNum);
            return giveCard;
        }

        public PlayCard DrawTop(Deck giveDeck)
        {
            List<PlayCard> givePlayCards = giveDeck.playCards;
            PlayCard giveCard = givePlayCards[givePlayCards.Count - 1];
            playCards.Add(givePlayCards[givePlayCards.Count-1]);
            givePlayCards.RemoveAt(givePlayCards.Count-1);
            return giveCard;
        }

        //もっと最適化できそう
        public List<PlayCard> DiscardSameNums()
        {
            List<PlayCard> removeList = new List<PlayCard>(); 
            //for文の条件式がループ毎に更新されるのかわからなかったので変数で置いた。
            int p = playCards.Count;
            for (int i = 0; i < p-1; i++)
            {
                if (p < 2) break;
                for (int j = i+1; j < p; j++)
                {
                    if (playCards[i].Number == playCards[j].Number)
                    {
                        //要素Iを取り除いた後の要素Jの順番が変わることがエラーに影響していると仮定し、条件を付けた。

                        removeList.Add(playCards[i]);
                        removeList.Add(playCards[j]);

                        playCards.RemoveAt(j);
                        playCards.RemoveAt(i);
                        p -= 2;
                        i--;
                        break;
                    }
                }
            }
            return removeList;
        }



    }
}
