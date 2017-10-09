using Assets.Scripts.Entities.Player;
using Assets.Scripts.Managers.Shop;
using Assets.Scripts.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Interfaces.UI;
using System;
using Assets.Scripts.Entities.Internationalization;
using Assets.Scripts.Services;
using System.Text;

namespace Assets.Scripts.Managers
{
    public class JackpotManager : MonoBehaviour, ILanguageUI
    {
        public AudioSource leverPullAudioSource, errorAudioSource, selectAudioSource, noPrizeAudioSource, prizeAudioSource, bestPrizeAudioSource;

        List<Sprite> spritesForResult;

        public int maxValue;

        public Text tokenAmountTxt;
        public Button leverBtn;
        public Text leverTxt;
        public Text returnTxt;



        public Image resultPanel;
        public Image resultPanelPrizeImg;
        public Text resultPreMsgTxt;
        public Text resultMsgTxt;
        public AlertMessageManager alertMessageManager;

        public Image firstImage, secondImage, thirdImage;
        public float timeToStopImage;
        public Button firstButton, secondButton, thirdButton;
        public Button returnBtn;

        Queue<Image> imageQueue;
        bool gameActive;
        Image currentRollingImage;
        bool isRollingImage;
        int currentImgValue;
        int rollingPrizeVal;

        string shieldItem;
        string lifeItem;
        string dashUpgradeItem;
        string creditsItem;
        string noRewardJackPot;
        string skinItem;


        string notEnoughTokensMsg;
        string twoPointsSkinsWarningJackPot;
        string twoPointsDashWarningJackPot;

        string stockedShieldMsg;
        string stockedLifeMsg;

        int skinPrizeId;
        Sprite skinPrize;

        int firstImgValue, secondImgValue, thirdImgValue;

        PlayerStatusData playerData;

        private void Start()
        {
            notEnoughTokensMsg = "Not enough tokens!";
            spritesForResult = new List<Sprite>();

            playerData = PlayerStatusService.LoadPlayerStatus();
            tokenAmountTxt.text = playerData.GetJackpotTokens().ToString();

            imageQueue = new Queue<Image>();

            var _spritesForResult = Resources.LoadAll<Sprite>("Sprites/Jackpot/RollingImages");

            foreach (var item in _spritesForResult)
            {
                spritesForResult.Add(item);
            }

            maxValue = (short)(spritesForResult.Count - 1);

            LoadTextsLanguage();
        }

        void LoadSkinForPrize()
        {
            //Has already loaded a prize from before
            if (skinPrize != null)
            {
                spritesForResult.Remove(skinPrize);
            }
            var playerSkins = Resources.LoadAll<Sprite>("Sprites/Player/Previews");
            List<int> owendSkins = PlayerStatusService.LoadPlayerStatus().GetOwnedShipsIDs();
            int[] skinsIds = playerSkins.Select(x => Convert.ToInt32(x.name)).ToArray();
            skinsIds = skinsIds.Where(x => !owendSkins.Contains(x)).ToArray();

            var ownedIdsStr = owendSkins.Select(x => x.ToString());
            playerSkins = playerSkins.Where(x => !ownedIdsStr.Contains(x.name)).ToArray();

            if (playerSkins.Length > 0)
            {
                skinPrize = GetSkinPrize(skinsIds, playerSkins);
                spritesForResult.Add(skinPrize);
            }
            else
            {
                maxValue = spritesForResult.Count - 1;
                Debug.Log("max value:" + maxValue);
            }
        }

        Sprite GetSkinPrize(int[] skinsIds, Sprite[] playerSkins)
        {
            skinPrizeId = RandomValueTool.GetRandomValue(skinsIds.Min(), skinsIds.Max());
            skinPrize = playerSkins.FirstOrDefault(x => skinPrizeId.ToString() == x.name);
            if (skinPrize == null)
            {
                return GetSkinPrize(skinsIds, playerSkins);
            }

            return skinPrize;
        }
        void DisableButtons()
        {
            firstButton.interactable = false;
            secondButton.interactable = false;
            thirdButton.interactable = false;
        }

        void EnableButtons()
        {
            firstButton.interactable = true;
            secondButton.interactable = true;
            thirdButton.interactable = true;
        }

        void ReenableLeverButton()
        {
            returnBtn.interactable = true;
            leverBtn.interactable = true;
        }

        void GetPrize(int firstValue, int secondValue, int thirdValue)
        {
            resultMsgTxt.text = noRewardJackPot;

            //All numbers equal
            if (firstValue == secondValue && secondValue == thirdValue)
            {
                //Return best prize
                var hasPrize = GetPrizeObject(firstValue, true);
                resultPanel.gameObject.SetActive(true);

                if (hasPrize)
                    bestPrizeAudioSource.Play();
                return;
            }

            //Two numbers equal
            int[] nums = new int[3] { firstValue, secondValue, thirdValue };

            var numsDic = new Dictionary<int, int>();
            int? equalNum = null;
            for (int i = 0; i < nums.Length; i++)
            {
                if (numsDic.ContainsKey(nums[i]))
                {
                    numsDic[nums[i]]++;
                    equalNum = nums[i];
                }
                else
                {
                    numsDic.Add(nums[i], 1);
                }
            }

            if (equalNum.HasValue)
            {
                //Return second best prize
                var hasPrize = GetPrizeObject(equalNum.Value, false);
                resultPanel.gameObject.SetActive(true);

                if (hasPrize)
                    prizeAudioSource.Play();

                return;
            }

            //return nothing
            resultPanel.gameObject.SetActive(true);
            noPrizeAudioSource.Play();

            PlayerStatusService.SavePlayerStatus(null);
        }

        bool GetPrizeObject(int prizeNum, bool bestPrize)
        {
            bool hasPrize = false;
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            switch (prizeNum)
            {
                case 1: //Life Buff
                GetLifePrize(bestPrize, playerData, prizeNum);
                hasPrize = true;
                break;
                case 2: //Extra Credits XXX 
                resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
                if (bestPrize)
                {
                    resultMsgTxt.text = string.Format("+300 {0}", creditsItem);

                    playerData.IncreaseScore(300);
                }
                else
                {
                    resultMsgTxt.text = string.Format("+50 {0}", creditsItem);

                    playerData.IncreaseScore(50);
                }
                hasPrize = true;
                break;
                case 3:  //Dash Upgrade
                if (bestPrize && playerData.CanUpgradeDash())
                {
                    playerData.IncreaseDashUpgrade();
                    resultMsgTxt.text = dashUpgradeItem;
                    resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
                    hasPrize = true;
                }
                else
                    resultMsgTxt.text = twoPointsDashWarningJackPot;
                break;
                case 4: //New Skin
                if (bestPrize)
                {
                    resultMsgTxt.text = skinItem;
                    resultPanelPrizeImg.sprite = skinPrize;
                    playerData.GetOwnedShipsIDs().Add(skinPrizeId);
                    hasPrize = true;
                }
                else
                    resultMsgTxt.text = twoPointsSkinsWarningJackPot;
                break;
            }
            PlayerStatusService.SavePlayerStatus(playerData);

            return hasPrize;
        }

        void GetLifePrize(bool bestPrize, PlayerStatusData playerData, int prizeNum)
        {
            resultMsgTxt.text = lifeItem;
            if (bestPrize)
            {
                resultMsgTxt.text = string.Format("+3 {0}", lifeItem);

                for (int i = 0; i < 3; i++)
                {
                    if (playerData.CanIncreaseLifeBuff())
                    {
                        playerData.IncreaseLifeBuff(1);
                    }
                    else
                    {
                        resultMsgTxt.text = string.Format("+3 {0}", stockedLifeMsg);

                        playerData.IncreaseLifeStock(3);
                    }
                }
            }
            else
            {
                if (playerData.CanIncreaseLifeBuff())
                {
                    playerData.IncreaseLifeBuff(1);
                }
                else
                {
                    resultMsgTxt.text = string.Format("+1 {0}", stockedLifeMsg);

                    playerData.IncreaseLifeStock(1);
                }
            }
            resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
        }

        void GetShieldPrize(bool bestPrize, PlayerStatusData playerData, int prizeNum)
        {
            resultMsgTxt.text = shieldItem;
            if (bestPrize)
            {
                resultMsgTxt.text = string.Format("+3 {0}", shieldItem);

                for (int i = 0; i < 3; i++)
                {
                    if (playerData.CanIncreaseShieldBuff())
                    {
                        playerData.IncreaseShieldBuff(1);
                    }
                    else
                    {
                        resultMsgTxt.text = string.Format("+3 {0}", stockedShieldMsg);

                        playerData.IncreaseShielStock(1);
                    }
                }
            }
            else
            {
                if (playerData.CanIncreaseShieldBuff())
                {
                    playerData.IncreaseShieldBuff(1);
                }
                else
                {
                    resultMsgTxt.text = string.Format("+1 {0}", stockedShieldMsg);

                    playerData.IncreaseShielStock(1);
                }
            }
            resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
        }

        IEnumerator RollImage()
        {
            while (isRollingImage)
            {
                yield return new WaitForSeconds(.1f);
                rollingPrizeVal = RandomValueTool.GetRandomValue(0, maxValue);
                currentRollingImage.sprite = spritesForResult[rollingPrizeVal];
            }
        }

        public void StopRollingImage()
        {
            if (!gameActive)
            {
                return;
            }
            isRollingImage = false;
            StopAllCoroutines();

            selectAudioSource.Play();

            if (currentImgValue == 0) //stopped first img
            {
                firstImgValue = rollingPrizeVal;
                currentRollingImage = imageQueue.Dequeue();
                isRollingImage = true;
                StartCoroutine(RollImage());
            }
            else if (currentImgValue == 1) //stopped second img
            {
                secondImgValue = rollingPrizeVal;
                currentRollingImage = imageQueue.Dequeue();
                isRollingImage = true;
                StartCoroutine(RollImage());
            }
            else if (currentImgValue == 2) //stopped third img
            {
                thirdImgValue = rollingPrizeVal;
                GetPrize(firstImgValue, secondImgValue, thirdImgValue);
                DisableButtons();
                gameActive = false;

                return;
            }

            currentImgValue++;
        }

        public void LoadTextsLanguage()
        {
            LanguageDictionary ld = LanguageService.GetLanguageDictionary();
            if (ld.isLoaded)
            {
                notEnoughTokensMsg = ld.notEnoughTokensJackPot;
                twoPointsSkinsWarningJackPot = ld.twoPointsSkinsWarningJackPot;
                twoPointsDashWarningJackPot = ld.twoPointsDashWarningJackPot;
                shieldItem = ld.shieldItem;
                lifeItem = ld.lifeItem;
                dashUpgradeItem = ld.dashUpgradeItem;
                creditsItem = ld.creditsItem;
                skinItem = ld.skinItem;
                noRewardJackPot = ld.noRewardJackPot;
                stockedLifeMsg = ld.shopStoredLives;
                stockedShieldMsg = ld.shopStoredShields;

                resultPreMsgTxt.text = ld.resultPreMsgJackPot;
            }
        }

        public void PullLever()
        {
            if (playerData.GetJackpotTokens() > 0 && !gameActive)
            {
                LoadSkinForPrize();
                leverPullAudioSource.Play();

                returnBtn.interactable = false;

                resultPanelPrizeImg.sprite = spritesForResult[0];

                playerData.DecreaseJackpotTokens(1);
                tokenAmountTxt.text = playerData.GetJackpotTokens().ToString();
                PlayerStatusService.SavePlayerStatus(playerData);

                leverBtn.interactable = false;

                imageQueue.Enqueue(secondImage);
                imageQueue.Enqueue(thirdImage);

                currentRollingImage = firstImage;
                currentImgValue = 0;
                isRollingImage = true;
                StartCoroutine(RollImage());

                gameActive = true;
                EnableButtons();

            }
            else
            {
                errorAudioSource.Play();
                alertMessageManager.SetAlertMessage(notEnoughTokensMsg);
            }
        }

        public void AcceptResult()
        {
            selectAudioSource.Play();
            resultPanel.gameObject.SetActive(false);
            ReenableLeverButton();
        }
    }
}
