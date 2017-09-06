using Assets.Scripts.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    [Serializable]
    public class PlayerStatusData
    {
        public PlayerStatusData(int score, int lifeUpgrade, int shieldUpgrade)
        {
            this.score = score;
            this.lifeUpgrade = lifeUpgrade;
            this.shieldUpgrade = shieldUpgrade;

            this.shipsOwnedIds = new List<int>();
        }

        public int score;
        public int credits;
        public int lifeBuff;
        public int shieldBuff;
        public List<int> shipsOwnedIds;
        public string shipSpritePath;
        public int jackPotTokens;
        public int storedLifePrizes;
        public int storedShieldPrizes;

        public int dashUpgrade;
        public int lifeUpgrade;
        public int shieldUpgrade;
        public string currentLanguage;

        const int maxDashUpgrade = 5;
        const int maxLifeUpgrade = 6;
        const int maxShieldUpgrade = 8;

        public int GetScore()
        {
            return this.score;
        }

        public void IncreaseScore(int score)
        {
            this.score += score;
        }

        public void DecreaseScore(int score)
        {
            this.score -= score;
        }

        public int GetCredits()
        {
            return this.credits;
        }

        public int GetLifeBuff()
        {
            return this.lifeBuff;
        }

        public void IncreaseLifeBuff(int amount)
        {
            this.lifeBuff += amount;
        }

        public void SetLifeBuff(int value)
        {
            this.lifeBuff = value;
        }

        public void IncreaseLifeStock(int amount)
        {
            this.storedLifePrizes += amount;
        }

        public int GetShieldBuff()
        {
            return this.shieldBuff;
        }

        public void SetShieldBuff(int value)
        {
            this.lifeBuff = value;
        }

        public void IncreaseShieldBuff(int amount)
        {
            this.shieldBuff += amount;
        }

        public void IncreaseShielStock(int amount)
        {
            this.storedShieldPrizes += amount;
        }

        public List<int> GetOwnedShipsIDs()
        {
            return this.shipsOwnedIds;
        } 

        public string GetShipSpritePath()
        {
            return this.shipSpritePath;
        }

        public void SetShipSpritePath(string path)
        {
            this.shipSpritePath = path;
        }

        public int GetJackpotTokens()
        {
            return this.jackPotTokens;
        }

        public void IncreaseJackpotTokens(int amount)
        {
            this.jackPotTokens += amount;
        }

        public void DecreaseJackpotTokens(int amount)
        {
            this.jackPotTokens -= amount;
        }

        public int GetStoreLifePrizes()
        {
            return this.storedLifePrizes;
        }

        public int GetStoredShieldPrizes()
        {
            return this.storedShieldPrizes;
        }

        public bool CanBuyBuff(string buffName, int amount)
        {
            if(buffName == MemberInfo.GetMemberName(() => this.lifeBuff))
            {
                return CanIncreaseLifeBuff();
            }

            if (buffName == MemberInfo.GetMemberName(() => this.shieldBuff))
            {
                return CanIncreaseShieldBuff();
            }

            return true;
        }

        public bool CanIncreaseLifeBuff()
        {
            return lifeBuff < lifeUpgrade;
        }

        public bool CanIncreaseShieldBuff()
        {
            return shieldBuff < shieldUpgrade;
        }

        public int GetLifeUpgrade()
        {
            return lifeUpgrade;
        }

        public int GetShieldUpgrade()
        {
            return shieldUpgrade;
        }


        public bool CanUpgradeDash()
        {
            return dashUpgrade < maxDashUpgrade;
        }

        public int GetDashUpgrade()
        {
            return dashUpgrade;
        }

        public int GetMaxDashUpgrade()
        {
            return maxDashUpgrade;
        }

        public bool IncreaseDashUpgrade()
        {
            if (dashUpgrade < maxDashUpgrade)
            {
                dashUpgrade++;
                return true;
            }

            return false;
        }

        public bool IncreaseLifeUpgrade()
        {
            if(lifeUpgrade <= maxLifeUpgrade)
            {
                lifeUpgrade++;
                return true;
            }

            return false;
        }

        public bool CanUpgradeLife()
        {
            return lifeUpgrade < maxLifeUpgrade;
        }


        public bool IncreaseShieldUpgrade()
        {
            if (shieldUpgrade <= maxShieldUpgrade)
            {
                shieldUpgrade++;
                return true;
            }

            return false;
        }

        public bool CanUpgradeShield()
        {
            return shieldUpgrade < maxShieldUpgrade;
        }

        public void SwapStoredLivesToBuff()
        {
            if (!CanIncreaseLifeBuff())
            {
                return;
            }

            int originalStoredVal = storedLifePrizes;
            for (int i = 0; i < originalStoredVal; i++)
            {
                lifeBuff++;
                storedLifePrizes--;
                if(!CanIncreaseLifeBuff())
                {
                    return;
                }
            }
        }

        public void SwapStoredShields()
        {
            if (!CanIncreaseShieldBuff())
            {
                return;
            }

            int originalStoredVal = storedShieldPrizes;
            for (int i = 0; i < originalStoredVal; i++)
            {
                shieldBuff++;
                storedShieldPrizes--;
                if (!CanIncreaseShieldBuff())
                {
                    return;
                }
            }
        }

        public string GetCurrentLanguage()
        {
            return this.currentLanguage;
        }

        public void SetCurrentLanguage(string currentLanguage)
        {
            this.currentLanguage = currentLanguage;
        }
    }
}
