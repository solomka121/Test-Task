using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class RestartMenu : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private LoseLine _loseLine;
    [SerializeField] private CoverPanel _cover;
    [SerializeField] private Button _restartButton;

    void Awake()
    {
        _loseLine.OnLose += ShowUp;
        _canvasGroup = GetComponent<CanvasGroup>();
        _restartButton.onClick.AddListener(Restart);
        gameObject.SetActive(false);
    }

    private void ShowUp()
    {
        gameObject.SetActive(true);
        LeanTween.alphaCanvas(_canvasGroup, 1, 0.4f).setEaseOutCubic();
    }

    private void Restart()
    {
        StartCoroutine(RestartCourutine());
        _restartButton.interactable = false;

        PopUp[] popUps = gameObject.GetComponentsInChildren<PopUp>();
        foreach (PopUp popUp in popUps)
            popUp.PopDown();
    }

    private IEnumerator RestartCourutine()
    {
        _cover.ActivateCover();
        yield return new WaitForSeconds(_cover.fadeDuration);
        SceneManager.LoadScene(1);
    }
}
