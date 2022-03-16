using UnityEngine;
using UnityEngine.EventSystems; // to do with events 

public class CubeSpawner : MonoBehaviour // to do events
{
    [SerializeField] private float _xBound;
    [SerializeField] private CubeThrower _cubeThrower;
    [SerializeField] private float _slideSpeed;
    private Camera _mainCamera;
    float xThrowerMove;
    private float _cameraZPlane;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraZPlane = _mainCamera.transform.position.z - transform.position.z;
    }

    private void Start()
    {
        xThrowerMove = _cubeThrower.transform.position.x;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                xThrowerMove += touch.deltaPosition.x * Time.deltaTime * _slideSpeed;
                xThrowerMove = Mathf.Clamp(xThrowerMove, -_xBound, _xBound);
                print("After : " + xThrowerMove);
                _cubeThrower.transform.position = new Vector3(xThrowerMove, _cubeThrower.transform.position.y, _cubeThrower.transform.position.z);
            } 

            if (touch.phase == TouchPhase.Ended) 
            {
                Throw();
            }
        }
    }

    private void Throw()
    {
        _cubeThrower.Throw();
    }
}
