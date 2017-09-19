using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Entities.Ads
{
    public class TokensPrize : AdsPrize
    {
        public TokensPrize()
        {
            base.Amount = 1;
        }
        public override void GivePrize(GameOverManager gameOverManager)
        {
            PlayerStatusService.LoadPlayerStatus().IncreaseJackpotTokens(base.Amount);

            AdsPrizeData prizeData = new AdsPrizeData();
            prizeData.message = base.languageDictionary.adsResultMsgTokens;
            prizeData.prizeSprite = Resources.Load<Sprite>(base.resourcesSpritePath + "Token");
 

            gameOverManager.SetPrizeMessage(prizeData);
        }
    }
}
