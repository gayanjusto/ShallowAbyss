using Assets.Scripts.Tools;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Ads
{
    public class AdsManager : MonoBehaviour
    {
        public GameOverManager gameOverManager;
        public GameObject watchAdsBtn;
        [SerializeField]
        string gameId = "1487925";

        public void InitializeAd()
        {

            Advertisement.Initialize(gameId, true);

            StartCoroutine(ShowAdWhenReady());
        }

        IEnumerator ShowAdWhenReady()
        {
            while (!Advertisement.IsReady())
                yield return null;

            var showOptions = new ShowOptions() { resultCallback = HandleAdsResult };
            Advertisement.Show(null, showOptions);
        }

        public bool WillShowAdsButton()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                return false;
            }
            int value = RandomValueTool.GetRandomValue(0, 100);

            return value >= 75;
        }

        void HandleAdsResult(ShowResult result)
        {
            if(result == ShowResult.Finished)
            {
                gameOverManager.GiveAdsPrize();
                watchAdsBtn.SetActive(false);
                return;
            }

            if(result == ShowResult.Skipped)
            {
                return;
            }

            if(result == ShowResult.Failed)
            {
                return;
            }
        }
    }
}
