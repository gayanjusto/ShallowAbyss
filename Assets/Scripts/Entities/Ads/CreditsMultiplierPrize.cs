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
            //Multiplier
            base.Amount = RandomValueTool.GetRandomValue(1, 3);
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

            //override prize ads result button event
            var panel = GameObject.Find("PrizeResultPanel");
            var okBtn = panel.transform.FindChild("Button").GetComponent<Button>();
            okBtn.onClick.AddListener(() => { panel.gameObject.SetActive(false); gameOverManager.RollScoreForAdsCredits(totalCredits); });
        }
    }
}
