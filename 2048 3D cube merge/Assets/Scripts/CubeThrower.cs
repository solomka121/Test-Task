using UnityEngine;
using System.Collections;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Cube _cube;
    [SerializeField] private float _throwStrenght;

   public void Throw()
    {
        if (_cube == null)
            return;
        _cube.Launch(_throwStrenght);
        _cube.transform.parent = null;
        _cube = null;
        StartCoroutine(SpawnNewCube());
    }

    private IEnumerator SpawnNewCube()
    {
        yield return new WaitForSeconds(0.25f);
            _cube = Instantiate(_prefab, transform);
    }
} 
