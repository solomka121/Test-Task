using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    private bool _playPressed;
    [SerializeField] CoverPanel _coverPanel;

    void Start()
    {
        _playButton.onClick.AddListener(StartGameWithCover);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void StartGameWithCover()
    {
        if(_playPressed == false)
        {
            StartCoroutine(FadeCoverAndStart());
            _playPressed = true;
        }
    }

    private IEnumerator FadeCoverAndStart()
    {
        _coverPanel.ActivateCover();
        yield return new WaitForSeconds(_coverPanel.fadeDuration);
        StartGame();
    }
}
