using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] protected bool _popUpOnEnable = true;
    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject, startScale, 0.6f).setEaseSpring();
    }

    public void PopDown()
    {
        LeanTween.scale(gameObject, Vector2.zero , 0.8f).setEaseInCubic();
    }
}
