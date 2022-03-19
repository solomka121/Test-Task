using TMPro;
using UnityEngine;

public class CubeNumbersPresenter : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private TMP_Text[] _cubeSides;

    private void Awake()
    {
        _cube.OnValueChange += ChangeNumbers;
    }

    private void ChangeNumbers(int number)
    {
        string textNumber = number.ToString();
        for (int i = 0; i < _cubeSides.Length; i++)
        {
            _cubeSides[i].text = textNumber;
        }
    }
}
