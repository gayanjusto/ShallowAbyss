using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers.Shop;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Interfaces.Shop;

namespace Assets.Scripts.Managers
{
    public class ShopSceneManager : MonoBehaviour, ILanguageUI
    {
        public AudioSource buyAudioSource;
        public AudioSource errorAudioSource;

        public SelectedObjectManager selectedObjectManager;

        public Text scoreAmountTxt;
        public Text tokensAmountTxt;

        public Text livesAmountTxt;
        public Text storedLivesTxt;

        public Text shieldsAmountTxt;
        public Text storedShieldsTxt;

        public Text dashUpgradesTxt;


        public AlertMessageManager alertMessageManager;

        public int playerScore;

        string notEnoughMoneyMsg;
        string subAlreadyBoughtMsg;
        string itemMaxReachedMsg;

        public Text tryYourLuckBtnTxt;
        public Text buyBtnText;

        void Start()
        {
            buyAudioSource.clip.LoadAudioData();
            errorAudioSource.clip.LoadAudioData();

            LoadTextsLanguage();

            LoadPlayerData();
        }

        void BuyShip(IShopItem selectedShip)
        {
            //Player has enough money?
            
            if (!PlayerHasEnoughFundsToBuy(selectedShip.GetPrice()))
            {
                PlayErrorSound();
                alertMessageManager.SetAlertMessage(notEnoughMoneyMsg);
                return;
            }

            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            //Player already bought this ship?
            if (!((IShopCharacterSkin)selectedShip).AlreadyHasShip().Invoke())
            {

                playerScore -= selectedShip.GetPrice();
                scoreAmountTxt.text = playerScore.ToString();

                selectedShip.BuyItem().Invoke();

                PlayerStatusService.SavePlayerStatus(playerData);

                ((IShopCharacterSkin)selectedShip).DisableCharacterButton();

                //Clear data
                //remove from selectedobjectmanager
                selectedObjectManager.RemoveSelectedObject();

                selectedShip = null;

                PlayBuySound();
            }
            else
            {
                PlayErrorSound();
                alertMessageManager.SetAlertMessage(subAlreadyBoughtMsg);
            }

        }

        void BuyBuff(ShopBuff item)
        {
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            if (!item.HasReachedItemMax().Invoke())
            {
                PlayErrorSound();
                alertMessageManager.SetAlertMessage(itemMaxReachedMsg);
                return;
            }

            if (PlayerHasEnoughFundsToBuy(item.buffPrice))
            {
                playerScore -= item.buffPrice;

                item.BuyItem().Invoke();

                playerData.DecreaseScore(item.buffPrice);
                PlayerStatusService.SavePlayerStatus(playerData);
                LoadPlayerData();

                item.DeselectObject();
                selectedObjectManager.RemoveSelectedObject();
                PlayBuySound();
            }
            else
            {
                PlayErrorSound();
                alertMessageManager.SetAlertMessage(notEnoughMoneyMsg);
            }
        }

        bool PlayerHasEnoughFundsToBuy(int price)
        {
            return playerScore - price >= 0;
        }

        void LoadPlayerData()
        {
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();
            playerScore = playerData.GetScore();
            scoreAmountTxt.text = playerScore.ToString();

            SetTokensText(playerData.GetJackpotTokens());
            SetLivesAmountText(playerData.GetLifeBuff(), playerData.GetLifeUpgrade());
            SetShieldsAmountText(playerData.GetShieldBuff(), playerData.GetShieldUpgrade());

            SetStoredLifesText(playerData.GetStoreLifePrizes());
            SetStoredShieldsText(playerData.GetStoredShieldPrizes());
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
            storedLivesTxt.text = string.Format(": {0}", amount);
        }

        void SetStoredShieldsText(int amount)
        {
            storedShieldsTxt.text = string.Format(": {0}", amount);
        }

        void SetTokensText(int amount)
        {
            tokensAmountTxt.text = string.Format("{0}", amount);
        }

        void SetDashUpgradesText(int amount, int maxAmount)
        {
            dashUpgradesTxt.text = string.Format(": {0} / {1}", amount, maxAmount);
        }

        void PlayBuySound()
        {
            buyAudioSource.Play();
        }

        void PlayErrorSound()
        {
            errorAudioSource.Play();
        }
        public void BuySelectedObject()
        {
            var selectedObject = selectedObjectManager.GetSelectedObject();

            if(selectedObject == null)
            {
                return;
            }
            if (selectedObject.GetObjectType() == ShopSelectedObjectEnum.Ship)
            {
                BuyShip((IShopItem)selectedObject);
                return;
            }

            if (selectedObject.GetObjectType() != ShopSelectedObjectEnum.None)
            {
                BuyBuff((ShopBuff)selectedObject);
            }
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                buyBtnText.text = ld.buy;
                notEnoughMoneyMsg = ld.notEnoughFundsShop;
                subAlreadyBoughtMsg = ld.subAlreadyBoughtShop;
                itemMaxReachedMsg = ld.maxItemReachedShop;
                tryYourLuckBtnTxt.text = ld.tryYourLuckBtn;
            }
        }
    }
}
 