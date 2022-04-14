using System.Collections;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private AnimationCurve _textUpdateScaleCurve;
    private LTDescr _textUpdateScale;
    private Vector3 _startScale;
    [SerializeField] private float _textUpdateScaleSpeed;
    [SerializeField] private Vector3 _textUpdateScaleAmount;
    [SerializeField] private TMP_Text _scoreText;
    private int score;

    private void Start()
    {
        _startScale = transform.localScale;
    }

    public void AddScore(int value) 
    {
        score += value;
        UpdateText();
    }

    private void UpdateText()
    {
        StartCoroutine(Pulse());
        _scoreText.text = score.ToString();
    }

    private IEnumerator Pulse()
    {
        float timePassed = 0;
        while (timePassed < 1)
        {
            timePassed += Time.deltaTime * _textUpdateScaleSpeed;
            float _percent = _textUpdateScaleCurve.Evaluate(timePassed);
            _scoreText.transform.localScale = Vector3.Lerp(_startScale, _textUpdateScaleAmount , _percent);
            yield return new WaitForEndOfFrame();
        }
        _scoreText.transform.localScale = _startScale;
    }
}
