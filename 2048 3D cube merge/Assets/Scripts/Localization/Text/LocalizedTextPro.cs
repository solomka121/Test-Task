using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedTextPro : LocalizedTextComponent
{
    private TMP_Text textComponent;
    public override void InvalidateText()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TMP_Text>();
        }
        try
        {
            textComponent.text = LocalizationManager.Instance.GetTextForKey(localizationKey);
        }
        catch (Exception exc)
        {
            Debug.Log("error while trying to localize text : " + localizationKey);
        }
    }
}