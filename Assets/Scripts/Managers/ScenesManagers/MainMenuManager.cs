using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Services.SocialServices;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class MainMenuManager : MonoBehaviour, ILanguageUI
    {
        public Text newGameBtnText;
        public Text selectSubBtnText;
        public Text shopBtnText;
        public Text rateUsBtnText;
        public Text versionText;
        public Button googlePlayGameBtn;

    
        private void Start()
        {
            if (GoogleGamePlayService.PlayerIsAuthenticated())
            {
                googlePlayGameBtn.interactable = false;
            }

            versionText.text = string.Format("v{0}", Application.version);
            LoadTextsLanguage();
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                newGameBtnText.text = ld.newGameMainMenu;
                selectSubBtnText.text = ld.selectSubMainMenu;
                shopBtnText.text = ld.shopMainMenu;
                rateUsBtnText.text = ld.rateUs;
            }
        }

        public void Quit()
        {
            GoogleGamePlayService.LogOut();
            Application.Quit();
        }

        public void RateUs() { Application.OpenURL("https://play.google.com/store/apps/details?id=com.NsaGames.Blu"); }

        public void LogInGoogleGamePlay()
        {
            GoogleGamePlayService.LogIn();

            googlePlayGameBtn.interactable = false;
        }

    }
}
