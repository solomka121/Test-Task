using System.Collections.Generic;
using UnityEngine;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Queue<Cube> _pool = new Queue<Cube>();
    [SerializeField] private int _startPoolSize = 20;

    private void Awake()
    {
        for (int i = 0; i < _startPoolSize; i++)
        {
            CreateCube();
        }
    }

    private void CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        cube.SetPool(this);
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }

    public Cube GetCube()
    {
        Cube cube = _pool.Dequeue();
        cube.gameObject.SetActive(true);
        return cube;
    }

    public void ReturnCubeToPool(Cube cube)
    {
        cube.Reset();
        _pool.Enqueue(cube);
        cube.gameObject.SetActive(false);
    }
}
