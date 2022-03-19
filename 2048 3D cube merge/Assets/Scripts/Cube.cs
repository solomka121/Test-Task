using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private  int _defaultvalue = 2;
    [SerializeField] private  int _startvalue = 2;
    private int _value;
    private bool _isFlying;
    private bool _canCombine = true;
    [SerializeField] private LayerMask _combineWithMask;
    [SerializeField] private float _autoCombineRadius = 2f;
    [SerializeField] private float _combineDelay;
    [SerializeField] private float _combineScaleTime; 
    private Rigidbody _rigidbody;
    private TrailRenderer _trail;
    private CubesPool _pool;

    public event System.Action<int> OnCombine;
    public event System.Action<int> OnValueChange;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();
        _value = _startvalue;
    }

    public void SetPool(CubesPool pool) => _pool = pool;

    private void Start()
    {
        OnValueChange?.Invoke(_value);
    }
    public void Launch(float strengh)
    {
        _isFlying = true;
        _trail.emitting = true;
        StartCoroutine(FlyForward(strengh));
    }

    public bool CanCombine(int cubeValue)
    {
        return _value == cubeValue;
    }

    public bool Combine(int cubeValue)
    {
        if (_isFlying)
            return false;

        if (_canCombine && _value == cubeValue)
        {
            _value += cubeValue;
            OnValueChange?.Invoke(_value);
            OnCombine?.Invoke(_value);
            VibrationController.Vibrate(60);
            StartCoroutine(CombineScale(_combineDelay / 2));
            StartCoroutine(CombineDelay());
            CombineThrow();
            return true;
        }
        return false;
    }

    private IEnumerator CombineDelay()
    {
        transform.localScale = Vector3.one * 0.8f;
        LeanTween.scale(gameObject, Vector3.one, _combineDelay).setEaseInCubic();
        _canCombine = false;
        yield return new WaitForSeconds(_combineDelay);
        _canCombine = true;
    }

    private IEnumerator CombineScale(float delay)
    {
        yield return new WaitForSeconds(delay);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, _combineScaleTime).setEaseInOutCubic().setOnComplete(CombineScaleBack);
    }

    private void CombineScaleBack()
    {
        LeanTween.scale(gameObject, Vector3.one, _combineScaleTime).setEaseOutCubic();
    }

    private void CombineThrow()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * 8f, ForceMode.Impulse);
        Debug.Log(ClosestSameCube());
        _rigidbody.AddForce(ClosestSameCube(), ForceMode.Impulse);
    }

    private Vector3 ClosestSameCube()
    {
        Collider[] cubes = Physics.OverlapSphere(transform.position, _autoCombineRadius, _combineWithMask);
        Vector3 closesCube = Vector3.one * _autoCombineRadius;
        bool foundSameCube = false;
        Debug.Log(cubes.Length);
        if (cubes.Length > 0)
        {
            foreach(Collider collider in cubes)
            {
                if(collider.TryGetComponent<Cube>(out Cube cube))
                {
                    if (cube == this)
                        continue;

                    if (cube.CanCombine(_value))
                    {
                        Vector3 direction = cube.transform.position - transform.position;
                        if (closesCube.magnitude > direction.magnitude)
                        {
                            closesCube = direction;
                            foundSameCube = true;
                        }
                    }
                }
            }
        }
        if (foundSameCube)
            return closesCube;
        return Vector3.zero;

    }

    private IEnumerator FlyForward(float strengh)
    {
        while (_isFlying)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.forward * Time.deltaTime * strengh);
            yield return null;
        }
        _trail.emitting = false;
        _rigidbody.AddForce(Vector3.forward * strengh, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (_canCombine && collision.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.Combine(_value))
            {
                Diactivate();
            }
            _isFlying = false;
        }

        if (_isFlying == false)
            return;

        else if (collision.gameObject.TryGetComponent<IStopCubeFly>(out _))
        {
            _isFlying = false;
        }
    }

    public void Reset()
    {
        _value = _defaultvalue;
        OnValueChange?.Invoke(_value);
        _rigidbody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public void Diactivate()
    {
        if(_pool != null)
        {
            _pool.ReturnCubeToPool(this);
            return;
        }
        Destroy(gameObject);
    }
}
