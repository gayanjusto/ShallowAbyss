using GoogleMobileAds.Api;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Services.AdMob
{
    public class AdMobService
    {
        static BannerView _bannerView;

        public static void RequestBanner()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {

                string adUnitId = "ca-app-pub-3940256099942544/6300978111";

                var bannerSizer = new AdSize(200, 200);
                // Create a 320x50 banner at the top of the screen.
                BannerView bannerView = new BannerView(adUnitId, bannerSizer, 5, 60);
                // Create an empty ad request.
                AdRequest request = new AdRequest.Builder().Build();
                // Load the banner with the request.
                bannerView.LoadAd(request);
                bannerView.Hide();
                _bannerView = bannerView;
            }
        }

        public static void ShowBannerAd()
        {
            if (_bannerView != null)
            {
                _bannerView.Show();
            }
        }
        static void HandleFailedRequest(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("Interstitial Failed to load: " + args.Message);
            // Handle the ad failed to load event.
        }

        public static void RemoveBannerAd()
        {
            if (_bannerView != null)
            {
                _bannerView.Destroy();
            }
        }
    }
}
