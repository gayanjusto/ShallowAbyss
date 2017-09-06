using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers.Shop;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.Scripts.Services;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Tools;

namespace Assets.Scripts.Managers
{
    public class ShopSceneManager : MonoBehaviour, IObjectSelector, ILanguageUI
    {
        public PlayerStatusManager playerStatusManager;
        public Text scoreAmountTxt;
        public Text tokensAmountTxt;

        public Text livesAmountTxt;
        public Text storedLivesTxt;

        public Text shieldsAmountTxt;
        public Text storedShieldsTxt;

        public Text dashUpgradesTxt;


        string lifeAmountLbl;
        string shieldAmountLbl;
        string storedLivesAmountLbl;
        string storedShieldsAmountLbl;

        public AlertMessageManager alertMessageManager;

        public int playerScore;
        public ShipsCarouselManager shipsCarouselManager;

        public ShipCarousel selectedShip;

        public Text SelectedObjectText { get; set; }
        public GameObject SelectedObject { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }

        string notEnoughMoneyMsg;
        string subAlreadyBoughtMsg;
        string itemMaxReachedMsg;

        string tokenItemName;

        public Text returnBtnText;
        public Text tryYourLuckBtnTxt;
        public Text buyBtnText;

        void Start()
        {
            LoadTextsLanguage();
            SelectedObjectText = GameObject.Find("SelectedItemDescription").GetComponent<Text>();


            LoadPlayerData();
        }

        void BuyShip()
        {
            //Player has enough money?
            if (!PlayerHasEnoughFundsToBuy(selectedShip.shipPrice))
            {
                alertMessageManager.SetAlertMessage(notEnoughMoneyMsg);
                return;
            }

            PlayerStatusData playerData = PlayerStatusManager.PlayerDataInstance;

            //Player already bought this ship?
            if (!playerData.GetOwnedShipsIDs().Contains(selectedShip.shipId))
            {

                playerScore -= selectedShip.shipPrice;
                scoreAmountTxt.text = playerScore.ToString();

                playerData.DecreaseScore(selectedShip.shipPrice);
                playerData.GetOwnedShipsIDs().Add(selectedShip.shipId);

                playerStatusManager.SavePlayerStatus(playerData);
                shipsCarouselManager.DisableShipButtonClick(selectedShip.gameObject);

                //Clean data
                ShopSelectedObjectEnum = ShopSelectedObjectEnum.None;
                selectedShip = null;
            }
            else
            {
                alertMessageManager.SetAlertMessage(subAlreadyBoughtMsg);
            }

        }

        void BuyBuff(ShopSelectedObjectEnum buffType, ShopBuff buff)
        {
            PlayerStatusData playerData = PlayerStatusManager.PlayerDataInstance;

            if (!buff.CanIncreaseBuff().Invoke())
            {
                alertMessageManager.SetAlertMessage(itemMaxReachedMsg);
                return;
            }

            if (PlayerHasEnoughFundsToBuy(buff.buffPrice))
            {
                playerScore -= buff.buffPrice;

                buff.BuyBuff().Invoke();

                playerData.DecreaseScore(buff.buffPrice);
                playerStatusManager.SavePlayerStatus(playerData);
                LoadPlayerData();
            }
            else
            {
                alertMessageManager.SetAlertMessage(notEnoughMoneyMsg);
            }
        }

        bool PlayerHasEnoughFundsToBuy(int price)
        {
            return playerScore - price > 0;
        }

        void LoadPlayerData()
        {
            PlayerStatusData playerData = PlayerStatusManager.PlayerDataInstance;
            playerScore = playerData.GetScore();
            scoreAmountTxt.text = playerScore.ToString();
            shipsCarouselManager.LoadAllShipsInCarousel(true);

            SetTokensText(playerData.GetJackpotTokens());
            SetLivesAmountText(playerData.GetLifeBuff(), playerData.GetLifeUpgrade());
            SetShieldsAmountText(playerData.GetShieldBuff(), playerData.GetShieldUpgrade());

            SetStoredLifesText(playerData.GetStoreLifePrizes());
            SetStoredShieldsText(playerData.GetStoreLifePrizes());
            SetDashUpgradesText(playerData.GetDashUpgrade(), playerData.GetMaxDashUpgrade());
        }

        void SetLivesAmountText(int amount, int maxAmount)
        {
            livesAmountTxt.text = string.Format(": {0} / {1}", amount, maxAmount);
        }

        void SetShieldsAmountText(int amount, int maxAmount)
        {
            shieldsAmountTxt.text = string.Format(": {0} / {1}", amount, maxAmount);
        }

        void SetStoredLifesText(int amount)
        {
            storedLivesTxt.text = string.Format("{0}: {1}", storedLivesAmountLbl, amount);
        }

        void SetStoredShieldsText(int amount)
        {
            storedShieldsTxt.text = string.Format("{0}: {1}", storedShieldsAmountLbl, amount);
        }

        void SetTokensText(int amount)
        {
            tokensAmountTxt.text = string.Format("{0}s: {1}", tokenItemName, amount);
        }

        void SetDashUpgradesText(int amount, int maxAmount)
        {
            dashUpgradesTxt.text = string.Format(": {0} / {1}", amount, maxAmount);
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;
            this.SelectedObjectText.text = objectName;
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

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                returnBtnText.text = ld.returnMsg;
                buyBtnText.text = ld.buy;
                notEnoughMoneyMsg = ld.notEnoughFundsShop;
                subAlreadyBoughtMsg = ld.subAlreadyBoughtShop;
                lifeAmountLbl = ld.shopLifeAmount;
                shieldAmountLbl = ld.shopShieldsAmount;
                storedLivesAmountLbl = ld.shopStoredLives;
                storedShieldsAmountLbl = ld.shopStoredShields;
                itemMaxReachedMsg = ld.maxItemReachedShop;
                tryYourLuckBtnTxt.text = ld.tryYourLuckBtn;
                tokenItemName = ld.tokenItem;
            }
        }
    }
}
