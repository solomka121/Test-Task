using UnityEngine;
using UnityEngine.UI;

public abstract class LocalizedTextComponent : MonoBehaviour
{
    [SerializeField]
    protected string localizationKey = default;

    public string LocalizationKey
    {
        get { return localizationKey; }
    }

    private void Awake()
    {
        InvalidateText();
    }

    public void SetLocalizationKey(string key)
    { 
        localizationKey = key;
        InvalidateText();
    }

    public abstract void InvalidateText();

}
