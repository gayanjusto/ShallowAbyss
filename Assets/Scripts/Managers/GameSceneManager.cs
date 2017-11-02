using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Services.Rating;

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
        public Text gameOverText;
        public Text bestPlayersText;

        public Text changeControlsBtnText;

        private void Start()
        {
            if (RatingRequestService.ratingRequestDisabled)
            {
                var ratingRequestService = new RatingRequestService();
                ratingRequestService.IncreaseTimesUserHasPlayed();
            }

            LoadTextsLanguage();
        }
        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                gamePausedText.text = ld.pausedTitle;
                newGameText.text = ld.newGame;
                continueText.text = ld.continueMsg;
                mainMenuPausedText.text = ld.mainMenu;
                gameOverText.text = ld.gameOver;

                mainMenuGameOverText.text = ld.mainMenu;
                shareText.text = ld.share;
                watchAdsText.text = ld.watchAds;
                bestPlayersText.text = ld.gameOverScores;
                changeControlsBtnText.text = ld.pauseControlSettingBtn;
            }
        }
    }
}
