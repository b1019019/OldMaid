using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    struct GameInfo
    {
        public string yourName;
        public int enemyNum;
        public GameInfo(string yourName,int enemyNum)
        {
            this.yourName = yourName;
            this.enemyNum = enemyNum;
        }

    }
    class SystemManager//システムマネージャー、実際の場に基づいたシステムを作成。ババ抜き用に関数を加工するもの。
    {
        //UIManagerなどの元となる
        private Player[] players;
        public IReadOnlyList<Player> playersRO;
        private GameInfo gameinfo;
        private int iniPlayerNum;
        public event Action<Player, Deck,PlayCard> ShowDrawMain;
        public event Action<Player, Player, int,PlayCard> ShowDrawPlayer;
        public event Action<Player,List<PlayCard>> ShowDiscardSameNums;
        public event Func<GameInfo> InputGameInfo;
        public event Func<Player,int> InputDrawNum;
        public event Action<Dictionary<int,string>> ShowResult;
        Deck main;
        public SystemManager()
        {
            
        }

        private void HandOutCard()//カードを配る
        {
            int p = 0;
            int playerNum = iniPlayerNum;
            int deckNum = main.CardCount();
            while (deckNum != 0)
            {
                
                ShowDrawMain(players[p],main, players[p].HandDrawTop(main));

                if (p == playerNum - 1) p = 0;
                else p++;
                deckNum--;
            }
        }
        private void RankPlayer(Player p)
        {
            if (p.HandCount() == 0 && p.Ranking == 0) 
            {
                int rank = 0;
                for(int i = 0; i < iniPlayerNum; i++)
                {
                    if (players[i].Ranking != 0) rank++; 
                }
                p.Ranking = rank + 1;
            }
        }
        private int SearchDrawedNum(int drawerNum)
        {
            int drawedNum = drawerNum;
            //drawedNumを探索,0を下回ったらLength-1にリセット

            for (int i = 0; i < iniPlayerNum - 1;i++)
            {
                if (--drawedNum < 0) drawedNum = iniPlayerNum - 1;
                if (players[drawedNum].Ranking == 0)
                {
                    return drawedNum;
                }
            }
            return -1;
        }

        private bool DrawByTurn(int drawerNum)//playersリストでの番号　一周、drawCardNumは何番目のカード引くか
        {//drawedを探し出せない場合、falseを返す
            int drawedNum = SearchDrawedNum(drawerNum);
            if(drawedNum == -1) return false;
            Player drawP = players[drawerNum];
            Player drawedP = players[drawedNum];
            
            int cardNum;
            Random rnd = new Random();
            if (drawP.Auto) cardNum = rnd.Next(drawedP.HandCount() - 1);
            else cardNum = InputDrawNum(drawedP);

            
            ShowDrawPlayer(drawP, drawedP, cardNum, drawP.HandDrawSelect(drawedP, cardNum));
            ShowDiscardSameNums(drawP,drawP.HandDiscardSameNums());
            RankPlayer(drawedP);//引かれた側が上がる方が早い
            RankPlayer(drawP);
            return true;
        }
        private void CyclicDraw() 
        {
            Random rnd = new Random();
            int drawerNum = rnd.Next(iniPlayerNum - 1);

            bool conti = true;//continue
            while (conti)
            {
                Player drawer = players[drawerNum];
                if(drawer.Ranking == 0)
                {
                    conti = DrawByTurn(drawerNum);
                }
                if (iniPlayerNum - 1 <= drawerNum) drawerNum = 0;
                else drawerNum++;
            }
            
        }
        private void InitialSet()
        {
            gameinfo = InputGameInfo();
            string yourName = gameinfo.yourName;
            int enemyNum = gameinfo.enemyNum;

            iniPlayerNum = enemyNum + 1;
            players = new Player[iniPlayerNum];
            main = Deck.MainDeck(1);
            players[0] = new Player(0, yourName, false);


            for (int i = 1; i <= enemyNum; i++)
            {
                Player player = new Player(i, "enemy" + i, true);
                players[i] = player;
            }
        }
        private void FinalProcess()
        {
            var rankPairs = new SortedDictionary<int,string>();
            foreach (Player p in players)
            {
                rankPairs.Add(p.Ranking + 1, p.Name);
            }
            ShowResult(new Dictionary<int, string>(rankPairs));
        }
        public void StartGame()
        {
            InitialSet();
            HandOutCard();
            CyclicDraw();
            FinalProcess();
        }
    }
}

