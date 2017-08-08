using Assets.Scripts.Entities;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers.Shop;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ShopSceneManager : MonoBehaviour
    {
        public PlayerStatusManager playerStatusManager;
        public Text scoreAmount;
        public Text selectedObjectText;
        public AlertMessageManager alertMessageManager;

        public int playerScore;
        public ShipsCarouselManager shipsCarouselManager;

        public ShipCarousel selectedShip;

        GameObject selectedObject;
        ShopSelectedObjectEnum shopSelectedObjectEnum;

        public string notEnoughMoneyMsg;
        public string shipAlreadyBoughtMsg;

        private void Start()
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            playerScore = playerData.score;
            scoreAmount.text = playerScore.ToString();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.shopSelectedObjectEnum = selectedObj;
            this.selectedObject = selectedGameObj;
            this.selectedObjectText.text = objectName;
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
                shopSelectedObjectEnum = ShopSelectedObjectEnum.None;
                selectedShip = null;
            }
            else
            {
                alertMessageManager.SetAlertMessage(shipAlreadyBoughtMsg);
            }

        }

        public void BuySelectedObject()
        {
            if (shopSelectedObjectEnum == ShopSelectedObjectEnum.Ship)
            {
                selectedShip = selectedObject.GetComponent<ShipCarousel>();
                BuyShip();
                return;
            }

            if (shopSelectedObjectEnum != ShopSelectedObjectEnum.None)
            {
                ShopBuff buff = selectedObject.GetComponent<ShopBuff>();
                BuyBuff(shopSelectedObjectEnum, buff);
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
