using UnityEngine.UIElements;
using System.Collections;
using UnityEngine;

public class CoverPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;
    public float fadeDuration { get => _fadeDuration; }
    [SerializeField] private bool _playOnStart;

    
    void Awake()
    {
        if (_playOnStart)
        {
            _canvasGroup.alpha = 1;
            ActivateCover();
        }
    }

    public void ActivateCover()
    {
        if (_canvasGroup.alpha < 1)
            LeanTween.alphaCanvas(_canvasGroup, 1, _fadeDuration).setEaseInCubic();
        else
            LeanTween.alphaCanvas(_canvasGroup , 0, _fadeDuration).setEaseOutCubic();
    }

}
