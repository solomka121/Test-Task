using System.Collections;
using UnityEngine;

public class ThrowsCounter : MonoBehaviour
{
    [SerializeField] private CubeThrower _cubeThrower;
    [SerializeField] private int _adFrequency = 10;
    [SerializeField] private float _adShowDelay = 0.6f;
    [SerializeField] private AdModInterstitial _adModInterstitial;
    private int _throws;

    void Awake()
    {
        _cubeThrower.OnThrow += ThrowMade;
    }
     
    void ThrowMade()
    {
        _throws++;
        if(_throws >= _adFrequency)
        {
            StartCoroutine(ShowAdWithDelay());
            _throws = 0;
        }
    }

    private IEnumerator ShowAdWithDelay()
    {
        yield return new WaitForSeconds(_adShowDelay);
        _adModInterstitial.ShowAd();
    }
}
