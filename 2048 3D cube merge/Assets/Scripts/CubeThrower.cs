using UnityEngine;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private float _xBound;
    [SerializeField] private float _throwStrenght;
    [SerializeField] private float _slideSpeed;
    private Camera _mainCamera;
    private float _xPosition;
    private Cube _cube;
    private Transform _transform;

    public event System.Action OnThrow;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _transform = transform;
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                _xPosition += touch.deltaPosition.x * Time.deltaTime * _slideSpeed;
                _xPosition = Mathf.Clamp(_xPosition, -_xBound, _xBound);
                _transform.position = new Vector3(_xPosition, _transform.position.y, _transform.position.z);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                Throw();
            }
        }
    }

    public void Throw()
    {
        if (_cube == null)
            return;
        OnThrow?.Invoke();
        _cube.transform.parent = null;
        _cube.Launch(_throwStrenght);
        _cube = null;
    }

    public void SetCube(Cube cube)
    {
        _cube = cube;
        _cube.transform.parent = transform;
        cube.transform.localPosition = Vector3.zero;
    }
} 
