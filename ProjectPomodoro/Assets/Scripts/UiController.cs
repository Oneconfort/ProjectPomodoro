using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject uiPause, painelInicio;
    bool visivel, visivelUiPause;

    private void Start()
    {
        GameController.controller.uiController = this;
    }

    private void Update()
    {
        Pausar();

    }
    public void FecharPainelInicio()
    {
        visivel = !visivel;
        Time.timeScale = 1f;
        painelInicio.SetActive(false);
    }

    void Pausar()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            visivelUiPause = !visivelUiPause;
            uiPause.SetActive(visivelUiPause);
        }
        if (visivelUiPause)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void BotaoPause()
    {
        visivelUiPause = !visivelUiPause;
        uiPause.SetActive(visivelUiPause);

        if (visivelUiPause)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
    public void Reset()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
