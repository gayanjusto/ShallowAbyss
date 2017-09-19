using System;
using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Services;

namespace Assets.Scripts.Managers.UI
{
    public class PrizeResultManager : MonoBehaviour, ILanguageUI
    {
        public Text titleTxt;
        public Text resultTxt;
        public Image prizeImg;

        private void Start()
        {
            LoadTextsLanguage();
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();

            if (ld.isLoaded)
            {
                this.titleTxt.text = ld.resultPreMsgJackPot;
            }
        }

        public void SetPrizeMessage(string msg)
        {
            this.resultTxt.text = msg;
        }

        public void SetPrizeImage(Sprite img)
        {
            this.prizeImg.sprite = img;
        }

        public void Disable()
        {
            this.gameObject.SetActive(false);
        }
    }
}
