using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Entities.Player
{
    [Serializable]
    public class PlayerStatusDataDTO
    {
        public PlayerStatusDataDTO(int score, int lifeUpgrade, int shieldUpgrade)
        {
            this.score = score;
            this.lifeUpgrade = lifeUpgrade;
            this.shieldUpgrade = shieldUpgrade;

            this.shipsOwnedIds = new List<int>();
        }

        int score;
        int credits;
        int lifeBuff;
        int shieldBuff;
        List<int> shipsOwnedIds;
        string shipSpritePath;
        int jackPotTokens;
        int storedLifePrizes;
        int storedShieldPrizes;

        int dashUpgrade;
        int lifeUpgrade;
        int shieldUpgrade;
    }
}
