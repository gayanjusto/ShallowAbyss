using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class MainMenuManager : MonoBehaviour, ILanguageUI
    {
        public Text newGameBtnText;
        public Text selectSubBtnText;
        public Text shopBtnText;
        public Text versionText;

        private void Start()
        {
            versionText.text = string.Format("v{0} - alpha", Application.version);
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
            }
        }
    }
}
