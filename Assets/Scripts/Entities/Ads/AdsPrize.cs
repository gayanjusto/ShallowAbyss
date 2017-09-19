using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Managers;
using Assets.Scripts.Services;

namespace Assets.Scripts.Entities.Ads
{
    public abstract class AdsPrize
    {
        protected string resourcesSpritePath = "Sprites/GameUI/";
        public int Amount { get; set; }

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
