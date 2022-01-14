using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OldMaid2
{
    interface IGameManager
    {
        void ShowDrawMain(Player getP,Deck giveD);
        void ShowDrawPlayer(Player drawP, Player drawedP, int cardNum);
        Player ShowDiscardSameNums();
        GameInfo InputGameInfo();
        int InputDrawNum(Player drawedP);

    }
}
