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

                string adUnitId = "ca-app-pub-6801501942748732/2857848546";

                int x, y;
                AdPosition pos;
                BannerView bannerView;
                if (Screen.dpi <= 160) { bannerView = new BannerView(adUnitId, new AdSize(320, 50), AdPosition.TopLeft); }
                else { bannerView = new BannerView(adUnitId, new AdSize(300, 250), 0, 60); }

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
