using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Colors")]
public class CubesColors : ScriptableObject
{
    public Material[] cubesMaterials;
    public int startNumber = 2;

}
