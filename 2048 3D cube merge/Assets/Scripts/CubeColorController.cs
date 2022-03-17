using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorController : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private Color[] _colors;
    private MeshRenderer _meshRenderer;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _cube.OnValueChange += UpdateColor;
    }

    private void UpdateColor(int value)
    {
        int colorNumber = 0;
        for (; true; colorNumber++)
        {
            value /= 2;
            if (value % 2 == 0)
                continue;
            else
                break;
        }
        _meshRenderer.material.color = _colors[colorNumber];
    }
}
