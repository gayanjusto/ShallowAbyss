using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers.Shop;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

namespace Assets.Scripts.Managers
{
    public class ShipSelectorManager : MonoBehaviour, IObjectSelector
    {
        public GameObject SelectedObject { get; set; }
        public Text SelectedObjectText { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }
        public ShipsCarouselManager shipsCarouselManager;
        public ShipCarousel selectedShip;

        private void Start()
        {

            SelectedObjectText = GameObject.Find("SelectedItemDescription").GetComponent<Text>();
            shipsCarouselManager.LoadOwnedShips();

            selectedShip = shipsCarouselManager.GetDefaultShip();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;
            this.SelectedObjectText.text = objectName;
        }

        public void StartNewGame()
        {
            PlayerStatusManager playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            PlayerStatusData playerdata = playerStatusManager.LoadPlayerStatus();
            string spritePath = selectedShip.shipSpritePath;

            playerdata.shipSpritePath = spritePath;

            playerStatusManager.SavePlayerStatus(playerdata);

            GameObject.Find("SceneManager").GetComponent<ScenesManager>().LoadNewGame();
        }
    }
}
