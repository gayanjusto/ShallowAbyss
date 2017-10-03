//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Assets.Scripts.Managers;
//using UnityEngine;

//namespace Assets.Scripts.Entities.Ads
//{
//    public class ShieldsPrize : AdsPrize
//    {
//        public ShieldsPrize()
//        {
//            base.Amount = 1;
//        }
//        public override void GivePrize(GameOverManager gameOverManager)
//        {
//            PlayerStatusService.LoadPlayerStatus().storedShieldPrizes += base.Amount;

//            AdsPrizeData prizeData = new AdsPrizeData();
//            prizeData.message = base.languageDictionary.adsResultMsgShields;
//            prizeData.prizeSprite = Resources.Load<Sprite>(base.resourcesSpritePath + "StoredShields");

//            gameOverManager.SetPrizeMessage(prizeData);
//        }
//    }
//}
