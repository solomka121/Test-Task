using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageChange : MonoBehaviour
{
    [SerializeField] private Button _english;
    [SerializeField] private Button _russian;
    [SerializeField] private Button _ukranian;

    void Start()
    {
        _english.onClick.AddListener(SetEnglishLanguage);
        _russian.onClick.AddListener(SetRussianLanguage);
        _ukranian.onClick.AddListener(SetUkranianLanguage);
    }

    private void SetEnglishLanguage()
    {
        LocalizationManager.Instance.UpdateLocale(ApplicationLocale.EN);
    }
    private void SetRussianLanguage()
    {
        LocalizationManager.Instance.UpdateLocale(ApplicationLocale.RU);
    }
    private void SetUkranianLanguage()
    {
        LocalizationManager.Instance.UpdateLocale(ApplicationLocale.UA);
    }
}
