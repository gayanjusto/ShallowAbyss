using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers.Shop;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ShopSceneManager : MonoBehaviour, IObjectSelector
    {
        public PlayerStatusManager playerStatusManager;
        public Text scoreAmount;
        public AlertMessageManager alertMessageManager;

        public int playerScore;
        public ShipsCarouselManager shipsCarouselManager;

        public ShipCarousel selectedShip;

        public Text SelectedObjectText { get; set; }
        public GameObject SelectedObject { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }

        public string notEnoughMoneyMsg;
        public string shipAlreadyBoughtMsg;

        private void Start()
        {
            SelectedObjectText = GameObject.Find("SelectedItemDescription").GetComponent<Text>();

            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            playerScore = playerData.score;
            scoreAmount.text = playerScore.ToString();

            shipsCarouselManager.LoadAllShipsInCarousel();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;
            this.SelectedObjectText.text = objectName;
        }

        void BuyShip()
        {
            //Player has enough money?
            if (PlayerHasEnoughFundsToBuy(selectedShip.shipPrice))
            {
                //return message and prevent buying
            }

            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            //Player already bought this ship?
            if (!playerData.shipsOwnedIds.Contains(selectedShip.shipId))
            {

                playerScore -= selectedShip.shipPrice;
                scoreAmount.text = playerScore.ToString();

                playerData.score = playerScore;
                playerData.shipsOwnedIds.Add(selectedShip.shipId);

                playerStatusManager.SavePlayerStatus(playerData);
                shipsCarouselManager.DisableShipButtonClick(selectedShip.gameObject);

                //Clean data
                ShopSelectedObjectEnum = ShopSelectedObjectEnum.None;
                selectedShip = null;
            }
            else
            {
                alertMessageManager.SetAlertMessage(shipAlreadyBoughtMsg);
            }

        }

        public void BuySelectedObject()
        {
            if (ShopSelectedObjectEnum == ShopSelectedObjectEnum.Ship)
            {
                selectedShip = SelectedObject.GetComponent<ShipCarousel>();
                BuyShip();
                return;
            }

            if (ShopSelectedObjectEnum != ShopSelectedObjectEnum.None)
            {
                ShopBuff buff = SelectedObject.GetComponent<ShopBuff>();
                BuyBuff(ShopSelectedObjectEnum, buff);
            }
        }

        void BuyBuff(ShopSelectedObjectEnum buffType, ShopBuff buff)
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            if (buff.HasBoughtMaxAmount(playerData))
            {
                alertMessageManager.SetAlertMessage(buff.maxErrorMsg);
                return;
            }

            if (PlayerHasEnoughFundsToBuy(buff.buffPrice))
            {
                playerScore -= buff.buffPrice;

                if (buffType == ShopSelectedObjectEnum.LifeBuff)
                {
                    playerData.lifeBuff++;
                }
                else if (buffType == ShopSelectedObjectEnum.ShieldBuff)
                {
                    playerData.shieldBuff++;
                }
                scoreAmount.text = playerScore.ToString();
                playerData.score = playerScore;
                playerStatusManager.SavePlayerStatus(playerData);
            }
            else
            {
                alertMessageManager.SetAlertMessage(notEnoughMoneyMsg);
                //display not enough funds error
            }
        }

        bool PlayerHasEnoughFundsToBuy(int price)
        {
            return playerScore - price > 0;
        }
    }
}
