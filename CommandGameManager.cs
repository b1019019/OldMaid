using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    class CommandGameManager
    {
        SystemManager systemManager = new SystemManager();
        public CommandGameManager()
        {
            systemManager.ShowDrawMain += ShowDrawMain;
            systemManager.ShowDrawPlayer += ShowDrawPlayer;
            systemManager.ShowDiscardSameNums += ShowDiscardSameNums;
            systemManager.InputGameInfo += InputGameInfo;
            systemManager.InputDrawNum += InputDrawNum;
            systemManager.ShowResult += ShowResult;
            
            systemManager.StartGame();
        }

        public Func<Player, int> InputDrawNum = (Player drawedP) =>
         {
             Console.WriteLine("何番目のカードを引きますか？");
             Console.WriteLine("1~" + drawedP.HandCount() + "の数字で入力してください");
             int cardNum = -1;
             bool trueValue = false;
             while (!trueValue)
             {
                 try
                 {
                     cardNum = int.Parse(Console.ReadLine());
                     trueValue = true;
                     if (drawedP.HandCount() < cardNum) throw new Exception();
                 }
                 catch
                 {
                     Console.WriteLine("もう一度カード番号を入力してください");
                     trueValue = false;
                 }
             }
             return cardNum - 1;



         };

        public Func<GameInfo> InputGameInfo = () =>
        {
            Console.WriteLine("名前を入力してください");
            string yourName = Console.ReadLine();
            Console.WriteLine("対戦相手の人数を入力してください");
            int enemyNum = int.Parse(Console.ReadLine());
            return new GameInfo(yourName, enemyNum);
        };

        public Action<Player, List<PlayCard>> ShowDiscardSameNums = (Player p, List<PlayCard> discards) =>
         {
             if (discards.Count == 0) return;
             Console.WriteLine(p.Name + "が");
             foreach(PlayCard d in discards)
             {
                 Console.WriteLine(d.Symbol + ":" + d.Number);
             }
             Console.WriteLine("を捨てました");
         };

        public Action<Player, Deck, PlayCard> ShowDrawMain = (Player getP, Deck giveD, PlayCard drawCard) =>
          {

              if (getP.Auto) Console.WriteLine(getP.Name + "が山札から1枚引きました");
              else Console.WriteLine(getP.Name + "が山札から" + drawCard.Symbol + ":" + drawCard.Number + "を引きました");
          };

        public Action<Player, Player, int, PlayCard> ShowDrawPlayer = (Player drawP, Player drawedP, int cardNum, PlayCard drawCard) =>
           {
               if (drawP.Auto) Console.WriteLine(drawP.Name + "が" + drawedP.Name + "から" + cardNum + "番のカードを引きました");
               else Console.WriteLine(drawP.Name + "が" + drawedP.Name + "から" + drawCard.Symbol + ":" + drawCard.Number + "を引きました");


           };
        public Action<Dictionary<int, string>> ShowResult = (Dictionary<int, string> result) =>
         {
             Console.WriteLine("結果");
             foreach(KeyValuePair<int,string> rankPair in result)
             {
                 Console.WriteLine(rankPair.Key + ":" + rankPair.Value);
             }
             Console.ReadLine();
         };
    }
}
