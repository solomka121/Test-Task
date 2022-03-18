using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
    public void Awake()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }
}