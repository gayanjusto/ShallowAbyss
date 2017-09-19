using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Entities.Ads
{
    public class LivesPrize : AdsPrize
    {
        public LivesPrize()
        {

            base.Amount = 1;
        }

        public override void GivePrize(GameOverManager gameOverManager)
        {

            PlayerStatusService.LoadPlayerStatus().storedLifePrizes += base.Amount;

            AdsPrizeData prizeData = new AdsPrizeData();

            prizeData.message =  base.languageDictionary.adsResultMsgLives;

            prizeData.prizeSprite = Resources.Load<Sprite>(base.resourcesSpritePath + "StoredLives");


            gameOverManager.SetPrizeMessage(prizeData);
        }
    }
}
