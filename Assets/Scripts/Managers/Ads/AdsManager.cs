﻿using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Ads;
using Assets.Scripts.Services.AdMob;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Ads
{
    public class AdsManager : MonoBehaviour
    {
        static string adsDataPath
        {
            get
            {
                return Application.persistentDataPath + "/adsData.dat";
            }
        }
        public GameObject gameOverPanel;
        public GameOverManager gameOverManager;
        public GameObject watchAdsBtn;

        [SerializeField]
        string gameId = "1487925";

        private void Start()
        {
            LoadAd();
            AdMobService.RequestBanner();
        }

        public void LoadAd()
        {
            Advertisement.Initialize(gameId, true);
        }
        public void ShowAd()
        {
            StartCoroutine(ShowAdWhenReady());
        }

        IEnumerator ShowAdWhenReady()
        {
            while (!Advertisement.IsReady())
                yield return null;

            var showOptions = new ShowOptions() { resultCallback = HandleAdsResult };
            Advertisement.Show(null, showOptions);
        }

        public bool WillShowBannerAds()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }

            AdMobService.ShowBannerAd();

            return true;
        }

        public bool CanShowVideoAds()
        {
            Debug.Log(Advertisement.GetPlacementState());

            if (Application.internetReachability == NetworkReachability.NotReachable
               || (Advertisement.GetPlacementState() == PlacementState.NoFill
               || Advertisement.GetPlacementState() == PlacementState.NotAvailable)
               )
            {
                return false;
            }

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
