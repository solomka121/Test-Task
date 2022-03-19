using System.Collections.Generic;

public class LocalizationData 
{
    public List<item> items;
}

[System.Serializable]
public struct item
{
    public string key;
    public string value;
}
