using System;
using System.Collections;
using System.Collections.Generic;
using AppodealAds.Unity.Common;
using UnityEngine;

public class InterstitialAdListener : IInterstitialAdListener
{
    public event Action<bool> OnAdEnd = delegate { };
    

    public void onInterstitialLoaded(bool isPrecache)
    {
        
    }
    public void onInterstitialFailedToLoad()
    {
        
    }
    public void onInterstitialShowFailed()
    {
        OnAdEnd(false);
    }
    public void onInterstitialShown()
    {
       
    }
    public void onInterstitialClosed()
    {
        OnAdEnd(false);
    }
    public void onInterstitialClicked()
    {
        OnAdEnd(true);
    }
    public void onInterstitialExpired()
    {
        OnAdEnd(false);
    }
}
