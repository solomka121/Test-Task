using UnityEngine;
using System.Collections;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubesPool _cubesPool;
    [SerializeField] private CubeThrower _cubeThrower;
    [SerializeField] private float _spawnSpeed;
    private Coroutine _spawnTimer;

    private void Awake()
    {
        _cubeThrower.OnThrow += StartSpawnCountdown;
        StartSpawnCountdown();
    }

    private void StartSpawnCountdown()
    {
        if (_spawnTimer == null)
            _spawnTimer = StartCoroutine(SpawnCountdown());
    }

    private IEnumerator SpawnCountdown()
    {
        yield return new WaitForSeconds(_spawnSpeed);
        SpawnCube();
        _spawnTimer = null;
    }

    private void SpawnCube()
    {
        Cube cube = _cubesPool.GetCube();
        cube.transform.localScale = Vector3.zero;
        LeanTween.scale(cube.gameObject, Vector3.one, 0.3f).setEaseOutExpo();
        _cubeThrower.SetCube(cube); 
    }
}
