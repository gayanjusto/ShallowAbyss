using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Entities.Internationalization;

namespace Assets.Scripts.Managers
{
    public class GameSceneManager : MonoBehaviour, ILanguageUI
    {
        public Text gamePausedText;
        public Text continueText;
        public Text mainMenuPausedText;

        public Text newGameText;
        public Text mainMenuGameOverText;
        public Text shareText;
        public Text watchAdsText;

        private void Start()
        {
            LoadTextsLanguage();
        }
        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                gamePausedText.text = ld.pausedTitle;
                newGameText.text = ld.newGameMainMenu;
                continueText.text = ld.continueMsg;
                mainMenuPausedText.text = ld.mainMenu;

                newGameText.text = ld.newGameMainMenu;
                mainMenuGameOverText.text = ld.mainMenu;
                shareText.text = ld.share;
                watchAdsText.text = ld.watchAds;
            }
        }
    }
}
