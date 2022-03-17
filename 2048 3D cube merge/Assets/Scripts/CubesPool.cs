using System.Collections.Generic;
using UnityEngine;

public class CubesPool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private CubesColors _cubesColors;
    [SerializeField] private Score _score;
    private CubesColorManager _cubesColorManager;
    [SerializeField] private Queue<Cube> _pool = new Queue<Cube>();
    [SerializeField] private int _startPoolSize = 30;
    [SerializeField] private bool _SearchCubesOnScene = true;
    [SerializeField] private Cube[] _cubesOnScene;

    private void Awake()
    {
        _cubesColorManager = new CubesColorManager(_cubesColors);
        _cubesColorManager.Init();

        if (_SearchCubesOnScene)
            _cubesOnScene = FindObjectsOfType<Cube>();
        if (_cubesOnScene.Length > 0)
        {
            foreach (Cube cube in _cubesOnScene)
            {
                AddCube(cube);
            }
        }

        for (int i = 0; i < _startPoolSize; i++)
        {
            CreateCube();
        }
    }

    private void CreateCube()
    {
        Cube cube = Instantiate(_cubePrefab);
        CubeInit(cube);
        cube.gameObject.SetActive(false);

        _pool.Enqueue(cube);
    }

    public void AddCube(Cube cube)
    {
        if (_pool.Contains(cube))
            return;

        CubeInit(cube);
    }

    private void CubeInit(Cube cube)
    {
        cube.SetPool(this);
        cube.OnCombine += _score.AddScore;
        if (cube.TryGetComponent<CubeColorController>(out CubeColorController cubeColorController))
            cubeColorController.Init(_cubesColorManager);
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
