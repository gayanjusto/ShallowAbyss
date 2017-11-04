using GoogleMobileAds.Api;
using UnityEngine;

namespace Assets.Scripts.Services.AdMob
{
    public class AdMobService
    {
        static BannerView _bannerView;


        public static void RequestBanner()
        {
            //test banner id: "ca-app-pub-3940256099942544/6300978111";

            string adUnitId = "ca-app-pub-6801501942748732/2857848546";

            BannerView bannerView;

            if (Screen.dpi <= 160)
            {
                bannerView = new BannerView(adUnitId, new AdSize(320, 50), AdPosition.TopLeft);
            }
            else
            {
                bannerView = new BannerView(adUnitId, new AdSize(300, 250), 0, 50);
            }

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            bannerView.LoadAd(request);

            bannerView.Hide();

            _bannerView = bannerView;
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
