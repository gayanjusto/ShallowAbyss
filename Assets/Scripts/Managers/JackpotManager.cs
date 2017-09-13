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

namespace Assets.Scripts.Managers
{
    public class JackpotManager : MonoBehaviour, ILanguageUI
    {
        //Suggested prizes: Life, Shield, 50 Credits, DashDuration, Character Skin
        //Best Prizes: 3x Life / 3x Shield / 300 Credits / DashDuration / LifeUpgrade /  Character Skin (may not exist)
        //Second Best Prizes: Life / Shield
        List<Sprite> spritesForResult;

        public int maxValue;
        public int amountTokens;

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


        Queue<Image> imageQueue;
        bool gameActive;
        Image currentRollingImage;
        bool isRollingImage;
        int currentImgValue;
        int rollingPrizeVal;

        PlayerStatusManager playerStatusManager;

        string shieldItem;
        string lifeItem;
        string dashUpgradeItem;
        string creditsItem;
        string noRewardJackPot;
        string skinItem;


        string notEnoughTokensMsg;
        string twoPointsSkinsWarningJackPot;
        string twoPointsDashWarningJackPot;





        int firstImgValue, secondImgValue, thirdImgValue;

        private void Start()
        {
            notEnoughTokensMsg = "Not enough tokens!";
            spritesForResult = new List<Sprite>();
            playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();

            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            amountTokens = playerData.GetJackpotTokens();
            tokenAmountTxt.text = amountTokens.ToString();

            imageQueue = new Queue<Image>();

            var _spritesForResult = Resources.LoadAll<Sprite>("Sprites/Jackpot/RollingImages");
            var playerSkins = Resources.LoadAll<Sprite>("Sprites/Player");
            var ownedIdsStr = playerData.GetOwnedShipsIDs().Select(x => x.ToString());
            playerSkins = playerSkins.Where(x => !ownedIdsStr.Contains(x.name)).ToArray();

            foreach (var item in _spritesForResult)
            {
                spritesForResult.Add(item);
            }
            if (playerSkins.Length > 0)
            {
                int rndSkin = RandomValueTool.GetRandomValue(0, playerSkins.Length - 1);
                Sprite playerSkin = playerSkins[rndSkin];
                spritesForResult.Add(playerSkin);
            }

            maxValue = (short)(spritesForResult.Count - 1);

            LoadTextsLanguage();
        }

        public void PullLever()
        {
            if (1 + 1 == 2/*amountTokens > 0 && !gameActive*/)
            {
                resultPanelPrizeImg.sprite = spritesForResult[0];

                PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
                playerData.DecreaseJackpotTokens(1);
                playerStatusManager.SavePlayerStatus(playerData);
                tokenAmountTxt.text = playerData.GetJackpotTokens().ToString();

                leverBtn.interactable = false;

                imageQueue.Enqueue(secondImage);
                imageQueue.Enqueue(thirdImage);

                currentRollingImage = firstImage;
                currentImgValue = 0;
                isRollingImage = true;
                StartCoroutine(RollImage());

                gameActive = true;
            }
            else
            {
                alertMessageManager.SetAlertMessage(notEnoughTokensMsg);
            }
        }

        public void AcceptResult()
        {
            resultPanel.gameObject.SetActive(false);
            ReenableLeverButton();
        }

        void ReenableLeverButton()
        {
            leverBtn.interactable = true;
        }

        void GetPrize(int firstValue, int secondValue, int thirdValue)
        {
            resultMsgTxt.text = noRewardJackPot;

            //All numbers equal
            if (firstValue == secondValue && secondValue == thirdValue)
            {
                //Return best prize
                GetPrizeObject(firstValue, true);
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
                GetPrizeObject(equalNum.Value, false);
            }

            //return nothing
            resultPanel.gameObject.SetActive(true);
        }

        void GetPrizeObject(int prizeNum, bool bestPrize)
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            switch (prizeNum)
            {
                case 1: //Life Buff
                GetLifePrize(bestPrize, playerData, prizeNum);
                break;
                case 2: //Shield Buff
                GetShieldPrize(bestPrize, playerData, prizeNum);
                break;
                case 3: //Extra Credits
                resultMsgTxt.text = creditsItem;
                resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
                if (bestPrize)
                {
                    playerData.IncreaseScore(300);
                }
                else
                {
                    playerData.IncreaseScore(50);
                }
                playerStatusManager.SavePlayerStatus(playerData);
                break;
                case 4: //Dash Upgrade
                if (bestPrize && playerData.CanUpgradeDash())
                {
                    playerData.IncreaseDashUpgrade();
                    resultMsgTxt.text = dashUpgradeItem;
                    resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
                }
                else
                    resultMsgTxt.text = twoPointsDashWarningJackPot;
                break;
                case 5: //New Skin
                if (bestPrize)
                    resultMsgTxt.text = skinItem;
                else
                    resultMsgTxt.text = twoPointsSkinsWarningJackPot;
                break;
            }


        }

        void GetLifePrize(bool bestPrize, PlayerStatusData playerData, int prizeNum)
        {
            resultMsgTxt.text = lifeItem;
            if (bestPrize)
            {

                for (int i = 0; i < 3; i++)
                {
                    if (playerData.CanIncreaseLifeBuff())
                    {
                        playerData.IncreaseLifeBuff(1);
                    }
                    else
                    {
                        playerData.IncreaseShielStock(1);
                    }
                }

                playerData.IncreaseLifeUpgrade();
            }
            else
            {
                if (playerData.CanIncreaseLifeBuff())
                {
                    playerData.IncreaseLifeBuff(1);
                }
                else
                {
                    playerData.IncreaseLifeStock(1);
                }
            }
            playerStatusManager.SavePlayerStatus(playerData);
            resultPanelPrizeImg.sprite = spritesForResult[prizeNum];
        }

        void GetShieldPrize(bool bestPrize, PlayerStatusData playerData, int prizeNum)
        {
            resultMsgTxt.text = shieldItem;
            if (bestPrize)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (playerData.CanIncreaseShieldBuff())
                    {
                        playerData.IncreaseShieldBuff(1);
                    }
                    else
                    {
                        playerData.IncreaseShieldBuff(1);
                    }
                }
            }else
            {
                if (playerData.CanIncreaseShieldBuff())
                {
                    playerData.IncreaseShieldBuff(1);
                }
                else
                {
                    playerData.IncreaseShieldBuff(1);
                }
            }
            playerStatusManager.SavePlayerStatus(playerData);
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
                leverTxt.text = ld.pushBtnJackPot;
                resultPreMsgTxt.text = ld.resultPreMsgJackPot;
                returnTxt.text = ld.returnMsg;
            }
        }
    }
}
