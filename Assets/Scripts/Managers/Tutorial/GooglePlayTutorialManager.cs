using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Services.SocialServices;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Tutorial
{
    public class GooglePlayTutorialManager : MonoBehaviour, ILanguageUI
    {
        public Toggle dontShowAgainBtn;
        public GameObject tutorialPanel;
        public Text tutTitle, tutDescription;

        private void Start()
        {
            LoadTextsLanguage();

            if (TutorialDataService.GetTutorialData().dontShowJackpotTutorial)
            {
                return;
            }

            if (!GoogleGamePlayService.PlayerIsAuthenticated()
                && Application.internetReachability != NetworkReachability.NotReachable)
            {
                tutorialPanel.gameObject.SetActive(true);
            }
        }

        public void PressOk()
        {
            var data = TutorialDataService.GetTutorialData();
            data.dontShowJackpotTutorial = true;

            TutorialDataService.SaveTutorialData(data);

            tutorialPanel.gameObject.SetActive(false);
        }

        public void LoadTextsLanguage()
        {
            var ld = LanguageService.GetLanguageDictionary();

            if (ld.isLoaded)
            {
                tutTitle.text = ld.tutGoogleGamePlayTitle;
                tutDescription.text = ld.tutGoogleGamePlay;
            }
        }
    }
}
