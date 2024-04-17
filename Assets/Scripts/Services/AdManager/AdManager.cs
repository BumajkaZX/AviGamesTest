namespace AviGamesTest.Services.Ad
{
    using System;
    using System.Collections.Generic;
    using AppodealAds.Unity.Api;
    using AppodealAds.Unity.Common;
    using UI;

    public class AdManager : IAdRequest, IAppodealInitializationListener, IDisposable
    {
        private InterstitialAdListener _interstitialCallback = new InterstitialAdListener();
        
        public event Action<bool> OnAdEnd = delegate { };

        private MockAdView _mockAdView;

        public AdManager(MockAdView adView)
        {
            int adTypes = Appodeal.INTERSTITIAL;
            string appKey = "TestKey";
            //Appodeal.setTesting(true);
            //Appodeal.initialize(appKey, adTypes);
            _mockAdView = adView;
        }

        public void ShowAd(AdType adType)
        {
            //Appodeal.show((int)adType);
            OnAdEnd(true);
            _mockAdView.Show();
        }
        
        public void onInitializationFinished(List<string> errors)
        {
            _interstitialCallback.OnAdEnd += OnAdEnd;
        }
        
        public void Dispose()
        {
            // Appodeal.setInterstitialCallbacks(_interstitialCallback);
            _interstitialCallback.OnAdEnd -= OnAdEnd;
        }
    }
}