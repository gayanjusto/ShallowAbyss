using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers.Shop;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using System;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Services;

namespace Assets.Scripts.Managers
{
    public class ShipSelectorManager : MonoBehaviour, IObjectSelector, ILanguageUI
    {
        public GameObject SelectedObject { get; set; }
        public Text SelectedObjectText { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }
        public ShipsCarouselManager shipsCarouselManager;
        ShipCarousel selectedShip;

        public Text subSelectorTitle;
        public Text returnBtnText;
        public Text goText;

        private void Start()
        {
            LoadTextsLanguage();
            SelectedObjectText = GameObject.Find("SelectedItemDescription").GetComponent<Text>();
            shipsCarouselManager.LoadOwnedShips(false);

            selectedShip = shipsCarouselManager.GetDefaultShip();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;
            this.SelectedObjectText.text = objectName;

            selectedShip = this.SelectedObject.GetComponent<ShipCarousel>();
        }

        public void DefineShip()
        {
            PlayerStatusManager playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            PlayerStatusData playerdata = playerStatusManager.LoadPlayerStatus();
            string spritePath = selectedShip.shipSpritePath;

            playerdata.shipSpritePath = spritePath;

            playerStatusManager.SavePlayerStatus(playerdata);

            GameObject.Find("SceneManager").GetComponent<ScenesManager>().LoadNewGame();
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                subSelectorTitle.text = ld.chooseSubTitle;
                returnBtnText.text = ld.returnMsg;
                goText.text = ld.go;
            }
        }
    }
}
