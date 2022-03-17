using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesColorManager
{
    public Dictionary<int, Material> Materials = new Dictionary<int, Material>();
    private CubesColors _colors;

    public CubesColorManager(CubesColors colors)
    {
        _colors = colors;
    }

    public void Init()
    {
        int cubeNumber = _colors.startNumber;
        for (int i = 0; i < _colors.cubesMaterials.Length; i++)
        {
            Materials.Add(cubeNumber, _colors.cubesMaterials[i]);
            cubeNumber *= 2;
        }
    }

    public bool TryGetMaterial(int value , out Material material)
    {
        return Materials.TryGetValue(value, out material);
    }
}
