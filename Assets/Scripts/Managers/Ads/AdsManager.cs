using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Ads;
using Assets.Scripts.Services.AdMob;
using Assets.Scripts.Tools;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Ads
{
    public class AdsManager : MonoBehaviour
    {
        static string adsDataPath {
            get
            {
                return Application.persistentDataPath + "/adsData.dat";
            }
        }
        public GameOverManager gameOverManager;
        public GameObject watchAdsBtn;

        ApplicationDataReader<LastSeenAdsData> appDataReader;

        [SerializeField]
        string gameId = "1487925";

        private void Start()
        {
            AdMobService.RequestBanner();
        }
        public void ShowAdsQuitBtn()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Application.Quit();
            }

            appDataReader = new ApplicationDataReader<LastSeenAdsData>();
            var data = appDataReader.LoadData(adsDataPath);

            if (data == null || data.CanShowAd())
            {
                var newDate = DateTime.Today;
                if(data == null)
                {
                    data = new LastSeenAdsData();
                }

                data.day = newDate.Day;
                data.month= newDate.Month;
                data.year = newDate.Year;

                appDataReader.SaveData(data, adsDataPath);

                InitializeAdForQuit();
            }else
            {
                Application.Quit();
            }
        }

        public void InitializeAdForQuit()
        {
            GameObject.Find("BtnQuit").GetComponent<Button>().interactable = false;
            Advertisement.Initialize(gameId, true);

            StartCoroutine(ShowAdWhenReadyForQuit());
        }

        public void InitializeAd()
        {
            Advertisement.Initialize(gameId, true);

            StartCoroutine(ShowAdWhenReady());
        }

        IEnumerator ShowAdWhenReadyForQuit()
        {
            while (!Advertisement.IsReady())
                yield return null;

            var showOptions = new ShowOptions() { resultCallback = HandleAdsResultForQuit };
            Advertisement.Show(null, showOptions);
        }

        IEnumerator ShowAdWhenReady()
        {
            while (!Advertisement.IsReady())
                yield return null;

            var showOptions = new ShowOptions() { resultCallback = HandleAdsResult };
            Advertisement.Show(null, showOptions);
        }

        public bool WillShowAds(int playerScore)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }

            AdMobService.ShowBannerAd();
            
            return true;
        }

        void HandleAdsResultForQuit(ShowResult result)
        {
            Application.Quit();
        }

        void HandleAdsResult(ShowResult result)
        {
            if (result == ShowResult.Finished)
            {
                gameOverManager.GiveAdsPrize();
                watchAdsBtn.SetActive(false);
                return;
            }

            if (result == ShowResult.Skipped)
            {
                return;
            }

            if (result == ShowResult.Failed)
            {
                return;
            }
        }
    }
}
