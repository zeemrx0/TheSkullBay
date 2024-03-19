using GoogleMobileAds.Api;
using UnityEngine;

public class AdMobPresenter : MonoBehaviour
{
#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
  private string _adUnitId = "unused";
#endif

  private InterstitialAd _interstitialAd;

  private void Start()
  {
    MobileAds.Initialize(
      (InitializationStatus initStatus) =>
      {
        Debug.Log("AdMob initialized successfully");
        LoadInterstitialAd();
      }
    );
  }

  public void LoadInterstitialAd()
  {
    // Clean up the old ad before loading a new one.
    if (_interstitialAd != null)
    {
      _interstitialAd.Destroy();
      _interstitialAd = null;
    }

    Debug.Log("Loading the interstitial ad.");

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    InterstitialAd.Load(
      _adUnitId,
      adRequest,
      (InterstitialAd ad, LoadAdError error) =>
      {
        // if error is not null, the load request failed.
        if (error != null || ad == null)
        {
          Debug.LogError(
            "interstitial ad failed to load an ad " + "with error : " + error
          );
          return;
        }

        Debug.Log(
          "Interstitial ad loaded with response : " + ad.GetResponseInfo()
        );

        _interstitialAd = ad;

        RegisterReloadHandler(_interstitialAd);
      }
    );
  }

  private void RegisterReloadHandler(InterstitialAd interstitialAd)
  {
    // Raised when the ad closed full screen content.
    interstitialAd.OnAdFullScreenContentClosed += () =>
    {
      Debug.Log("Interstitial Ad full screen content closed.");

      // Reload the ad so that we can show another as soon as possible.
      LoadInterstitialAd();
    };
    // Raised when the ad failed to open full screen content.
    interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
    {
      Debug.LogError(
        "Interstitial ad failed to open full screen content "
          + "with error : "
          + error
      );

      // Reload the ad so that we can show another as soon as possible.
      LoadInterstitialAd();
    };
  }

  public void ShowInterstitialAd()
  {
    if (_interstitialAd != null && _interstitialAd.CanShowAd())
    {
      Debug.Log("Showing interstitial ad.");
      _interstitialAd.Show();
    }
    else
    {
      Debug.LogError("Interstitial ad is not ready yet.");
    }
  }
}
