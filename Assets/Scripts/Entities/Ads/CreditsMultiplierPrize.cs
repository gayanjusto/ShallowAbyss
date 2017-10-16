using System;
using Assets.Scripts.Managers;
using Assets.Scripts.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Ads
{
    public class CreditsMultiplierPrize : AdsPrize
    {

        public CreditsMultiplierPrize()
        {
            int maxVal = 1;
            var probability = RandomValueTool.GetRandomValue(0, 100);

            if(probability > 95)
            {
                maxVal = 3;
            }else if(probability <= 95 && probability > 80)
            {
                maxVal = 2;
            }

            //Multiplier
            base.Amount = RandomValueTool.GetRandomValue(1, maxVal);
        }

        public override void GivePrize(GameOverManager gameOverManager)
        {
            int finalCredits = gameOverManager.GetFinalScore();
            int totalCredits = base.Amount * finalCredits;

            PlayerStatusService.LoadPlayerStatus().score += totalCredits;

            AdsPrizeData prizeData = new AdsPrizeData();

            if (!string.IsNullOrEmpty(base.languageDictionary.adsResultMsgCredits))
                prizeData.message = string.Format(base.languageDictionary.adsResultMsgCredits, base.Amount);

            prizeData.prizeSprite = Resources.Load<Sprite>(base.resourcesSpritePath + "Coin");


            gameOverManager.SetPrizeMessage(prizeData);
            resultPrizeOkBtn.onClick.RemoveAllListeners();
            resultPrizeOkBtn.onClick.AddListener(() => { resultPrizePanel.SetActive(false); gameOverManager.RollScoreForAdsCredits(totalCredits); });
        }
    }
}
