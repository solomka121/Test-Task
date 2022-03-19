using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public const string FOLDER_IN_RESOURCES = "Localization";
    public const string FILENAME_PREFIX = "locale_";

    private Dictionary<string, string> _localizedDictionary;
    [SerializeField] private TextAsset _jsonFile;
    private LocalizationData _loadedData;
    string loadedLanguage;

    public static LocalizationManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey("PLAYER_LANGUAGE"))
            loadedLanguage = PlayerPrefs.GetString("PLAYER_LANGUAGE");
        else
            loadedLanguage = LocaleHelper.GetSupportedLanguageCode();

        LoadJsonLanguageData();
    }

    private void LoadJsonLanguageData()
    {
        if(_jsonFile == null)
            LoadJsonFile();

        _loadedData = JsonUtility.FromJson<LocalizationData>(_jsonFile.text);
        PlayerPrefs.SetString("PLAYER_LANGUAGE" , loadedLanguage);

        _localizedDictionary = new Dictionary<string, string>(_loadedData.items.Count);
        _loadedData.items.ForEach(item => {
            _localizedDictionary.Add(item.key, item.value); });
    }

    private void LoadJsonFile()
    {
        string filePath = GetPathForLocale(loadedLanguage);
        _jsonFile = Resources.Load<TextAsset>(filePath);
    }

    private string GetPathForLocale(string languageCode)
    {
        string path = FOLDER_IN_RESOURCES + "/" + FILENAME_PREFIX + languageCode.ToLower();
        return path;
    }

    public void UpdateLocale(string languageCode)
    {
        loadedLanguage = languageCode;
        LoadJsonFile();
        LoadJsonLanguageData();

        LocalizedTextComponent[] texts = FindObjectsOfType<LocalizedTextComponent>(true);
        for(int i = 0; i < texts.Length; i++)
        {
            texts[i].InvalidateText();
        }
    }

    public string GetTextForKey(string localizationKey)
    {
        if (_localizedDictionary.ContainsKey(localizationKey))
        {
            return _localizedDictionary[localizationKey];
        }

        throw new UnityException(String.Format("Missing localization for key: {0} and language: {1}.", localizationKey, loadedLanguage));
    }
}
