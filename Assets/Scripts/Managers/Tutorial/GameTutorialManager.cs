using System;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Tutorial
{
    public class GameTutorialManager : TutorialManager, ILanguageUI
    {
        public Text movementTxt;
        public Text scoringTxt;
        public Text dashTxt;
        public Text lifeShieldTxt;
        public Button pauseBtn;
        public DashManager dashManager;

        bool openViaPause;

        private void Awake()
        {
            var tutorialData = TutorialDataService.GetTutorialData();
            base.DeactivatePanel(tutorialData.dontShowGameTutorial);

            if (!tutorialData.dontShowGameTutorial)
            {
                pauseBtn.interactable = false;
                dashManager.DisableButtonInteraction();
            }
            LoadTextsLanguage();
        }

        public void LoadTextsLanguage()
        {
            var ld = LanguageService.GetLanguageDictionary();

            if (ld.isLoaded)
            {
                dontShowThisAgainTxt.text = ld.tutDontShowAgain;
                movementTxt.text = ld.tutMainGameMovement;
                scoringTxt.text = ld.tutMainGameScoring;
                dashTxt.text = ld.tutMainGameDash;
                lifeShieldTxt.text = ld.tutMainGameLifeShield;
            }
        }

        public void PressOk()
        {
            var tutorialData = TutorialDataService.GetTutorialData();
            tutorialData.dontShowGameTutorial = dontShowToggle.isOn;

            if (openViaPause)
            {
                TutorialDataService.SaveTutorialData(tutorialData);
                openViaPause = false;
                tutorialPanel.SetActive(false);
            }
            else
            {
                SaveAndRelease(tutorialData);
                pauseBtn.interactable = true;
                dashManager.EnableButtonInteraction();
            }

        }

        public void ShowTutorial()
        {
            openViaPause = true;
            var tutorialData = TutorialDataService.GetTutorialData();
            dontShowToggle.isOn = tutorialData.dontShowGameTutorial;
            dashManager.DisableButtonInteraction();
            DeactivatePanel(false);
        }


    }
}
