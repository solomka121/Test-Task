using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private  int _defaultvalue;
    private int _value;
    private bool _isFlying;
    private bool _canCombine = true;
    [SerializeField] private float _combineDelay;
    [SerializeField] private AnimationCurve _combineSizeCurve;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private TrailRenderer _trail;
    private CubesPool _pool;

    public event System.Action<int> OnCombine;
    public event System.Action<int> OnValueChange;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _value = _defaultvalue;
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

    public bool Combine(int cubeValue)
    {
        if (_isFlying)
            return false;

        if (_canCombine && _value == cubeValue)
        {
            _value += cubeValue;
            OnValueChange?.Invoke(_value);
            LeanTween.scale(gameObject, Vector3.zero, 0.6f).setEase(_combineSizeCurve);
            StartCoroutine(CombineDelay());
            CombineThrow();
            return true;
        }
        return false;
    }

    private IEnumerator CombineDelay()
    {
        _canCombine = false;
        yield return new WaitForSeconds(_combineDelay);
        _canCombine = true;
    }

    private void CombineThrow()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
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
