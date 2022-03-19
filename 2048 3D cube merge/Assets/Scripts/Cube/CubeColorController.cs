using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorController : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    private MeshRenderer _meshRenderer;
    private CubesColorManager _cubesColorManager;
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _cube.OnValueChange += UpdateColor;
    }

    public void Init(CubesColorManager cubesColorManager)
    {
        _cubesColorManager = cubesColorManager;
    }

    private void UpdateColor(int value)
    {
        Material materialByValue;
        if (_cubesColorManager.TryGetMaterial(value, out materialByValue))
            _meshRenderer.material = materialByValue;
        else
            Debug.LogError("No material for cubes of value " + value);
    }
}
