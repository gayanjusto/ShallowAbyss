using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities.Ads
{
    public abstract class AdsPrize
    {
        protected string resourcesSpritePath = "Sprites/GameUI/";
        public int Amount { get; set; }

        protected GameObject resultPrizePanel
        {
            get
            {
                return GameObject.Find("PrizeResultPanel");
            }
        }

        protected Button resultPrizeOkBtn
        {
            get
            {
                var okBtn = resultPrizePanel.transform.FindChild("Button").GetComponent<Button>();
                return okBtn;
            }
        }
        protected LanguageDictionary languageDictionary
        {
            get
            {
                return LanguageService.GetLanguageDictionary();
            }
        }

        public abstract void GivePrize(GameOverManager gameOverManager);
    }
}
