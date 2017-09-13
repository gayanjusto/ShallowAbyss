using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Managers.Shop;

namespace Assets.Scripts.Entities
{
    public class ShipCarousel : MonoBehaviour, IShopItem, IShopCharacterSkin, IShopSelectedObject
    {
        public Sprite shipImage;
        public string shipDescription;
        public int shipPrice;
        public int shipId;
        public string shipSpritePath;
        public Text priceTag;

        Text ui_shipDescription;
        PlayerStatusData playerData;
        Transform shipButton;
        Color originalColor;

        private void Start()
        {
            playerData = PlayerStatusManager.PlayerDataInstance;

            priceTag.text = string.Format("$ {0}", shipPrice);
            ui_shipDescription = GameObject.Find("SelectedItemDescription").GetComponent<Text>();
            var selectedObjectManager = GameObject.Find("SelectedObjectManager").GetComponent<SelectedObjectManager>();


            shipButton = this.transform.FindChild("ShipButton");
            var cmp = shipButton.gameObject.GetComponentInChildren<Image>();
            cmp.sprite = shipImage;

            //set listener when clicking to select object
            shipButton.GetComponent<Button>().onClick.AddListener(() => selectedObjectManager.SetSelectedObject(this));

            originalColor = shipButton.GetComponent<Image>().color;
        }

        public void SelectShip()
        {
            //Set ship description
            ui_shipDescription.text = this.shipDescription;
        }

        public Func<bool> HasEnoughCreditsToBuy()
        {
            return () =>  !playerData.GetOwnedShipsIDs().Contains(this.shipId); 
        }

        public Action BuyItem()
        {
            return () => { playerData.DecreaseScore(shipPrice); playerData.GetOwnedShipsIDs().Add(this.shipId); };
        }

        public void SelectObject()
        {
            //Set gray color
            shipButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f);
        }

        public void DeselectObject()
        {
           shipButton.GetComponent<Image>().color = originalColor;
        }

        public string GetName()
        {
            return this.shipDescription;
        }

        public ShopSelectedObjectEnum GetObjectType()
        {
            return ShopSelectedObjectEnum.Ship;
        }

        public int GetPrice()
        {
            return this.shipPrice;
        }

        public int GetId()
        {
            return this.shipId;
        }

        public void DisableCharacterButton()
        {
            Transform shipTransform = transform.FindChild("ShipButton");
            shipTransform.GetComponent<Button>().enabled = false;

            Image shipImage = shipTransform.GetComponent<Image>();

            //set disabled color
            Color shipColor = shipImage.color;
            shipImage.color = new Color(shipColor.r, shipColor.g, shipColor.b, .1f);
        }

        public Func<bool> AlreadyHasShip()
        {
            return () => !playerData.GetOwnedShipsIDs().Contains(shipId);
        }
    }
}
