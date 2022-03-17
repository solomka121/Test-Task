using UnityEngine;

public class CubeThrower : MonoBehaviour
{
    [SerializeField] private float _xBound;
    [SerializeField] private float _throwStrenght;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private Transform _ScopeLine;
    private LTDescr scaling;
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
        DiactivateScopeLine();
        _cube.transform.parent = null;
        _cube.Launch(_throwStrenght);
        _cube = null;
    }

    public void SetCube(Cube cube)
    {
        _cube = cube;
        _cube.transform.parent = transform;
        cube.transform.localPosition = Vector3.zero;
        ActivateScopeLine();
    }

    private void ActivateScopeLine()
    {
        scaling = LeanTween.scaleX(_ScopeLine.gameObject, 1f, 0.3f).setEaseOutExpo();
    }

    private void DiactivateScopeLine()
    {
        LeanTween.cancel(scaling.id);
        LeanTween.scaleX(_ScopeLine.gameObject, 0f, 0.15f).setEaseOutCirc();
    }
} 
