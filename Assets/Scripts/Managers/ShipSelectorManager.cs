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
    public class ShipSelectorManager : MonoBehaviour, ILanguageUI
    {
        public GameObject SelectedObject { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }
        public ShipsCarouselManager shipsCarouselManager;
        ShipCarousel selectedShip;

        public Text subSelectorTitle;
        public Text goText;

        private void Start()
        {
            LoadTextsLanguage();
            shipsCarouselManager.LoadOwnedShips(false);

            selectedShip = shipsCarouselManager.GetDefaultShip();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;

            selectedShip = this.SelectedObject.GetComponent<ShipCarousel>();
        }

        public void DefineShip()
        {
            PlayerStatusManager playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            PlayerStatusData playerdata = playerStatusManager.LoadPlayerStatus();
            string spritePath = selectedShip.shipSpritePath;

            playerdata.SetShipSpritePath(spritePath);

            playerStatusManager.SavePlayerStatus(playerdata);

            GameObject.Find("SceneManager").GetComponent<ScenesManager>().LoadNewGame();
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                subSelectorTitle.text = ld.chooseSubTitle;
                goText.text = ld.go;
            }
        }
    }
}
