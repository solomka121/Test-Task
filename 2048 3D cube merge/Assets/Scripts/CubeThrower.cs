using UnityEngine;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private Transform _cube;

   public void Throw()
    {
        _cube.transform.Translate(Vector3.forward);
    }
} 
