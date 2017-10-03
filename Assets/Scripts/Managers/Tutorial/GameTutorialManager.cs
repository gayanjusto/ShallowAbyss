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

        private void Awake()
        {
            var tutorialData = TutorialDataService.GetTutorialData();
            base.ActivatePanel(tutorialData.dontShowGameTutorial);

            LoadTextsLanguage();
        }

        public void PressOk()
        {
            var tutorialData = TutorialDataService.GetTutorialData();
            tutorialData.dontShowGameTutorial = dontShowToggle.isOn;

            base.PressOk(tutorialData);
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
    }
}
