using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuUIHandler : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _canvas.enabled = false;

        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    public void OnRaceAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnExitToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator ShowMenuCO()
    {
        yield return new WaitForSeconds(1);

        _canvas.enabled = true;
    }

    private void OnGameStateChanged(GameManager gameManager)
    {
        if (GameManager.Instance.GetGameState() == GameStates.RaceOver)
        {
            StartCoroutine(ShowMenuCO());
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
}