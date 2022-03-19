using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : LocalizedTextComponent
{
    private Text textComponent;
    public override void InvalidateText()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }
        try
        {
            textComponent.text = LocalizationManager.Instance.GetTextForKey(localizationKey);
        }
        catch (Exception exc)
        {
            
        }
    }
}