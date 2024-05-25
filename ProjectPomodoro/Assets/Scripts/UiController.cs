using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;

public class UiController : MonoBehaviour
{
    public GameObject uiPause, painelInicio, intervaloImagem, painelCreditos, painelOpcoes, painelDerrota, canvas;
    bool visivel, visivelUiPause;
    public Slider All, Music, VFX;
    //public Slider[] barraPontosMiniGame;
    private void Start()
    {
        if (GameController.controller != null)
        {

            GameController.controller.uiController = this;
        }

    }

    private void Update()
    {
        if (GameController.controller != null)
        {
            if (GameController.controller.isMiniGame == true) { return; }
        }
        Pausar();
    }
    public void FecharPainelInicio()
    {
        // visivel = !visivel;
         Time.timeScale = 1f;
        //painelInicio.SetActive(false);
        SceneManager.LoadScene("Fase_Final");
    }
   

    void Pausar()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            visivelUiPause = !visivelUiPause;
            uiPause.SetActive(visivelUiPause);
            if (!visivelUiPause)
            {
                Time.timeScale = 1.0f;
            }
        }
        if (visivelUiPause)
        {
            Time.timeScale = 0.0f;
        }
    }

    public void MostrarPainelFimDeJogo()
    {
        visivel = !visivel;
        painelDerrota.SetActive(true);
       // Time.timeScale = 0.0f;
    }

    public void PainelMenu()
    {
        visivel = !visivel;
        painelInicio.SetActive(true);
        painelCreditos.SetActive(false);
        painelOpcoes.SetActive(false);
    }

    public void PainelOpcoes()
    {
        visivel = !visivel;
        painelInicio.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void PainelCreditos()
    {
        visivel = !visivel;
        painelInicio.SetActive(false);
        painelCreditos.SetActive(true);
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
    public void Reset(int resetar)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(resetar);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ChangeAllVolume()
    {
        AudioController.audioController.ChangeAllVolume(All.value);
    }
    public void ChangeMUsicVolume()
    {
        AudioController.audioController.ChangeMusicVolume(Music.value);
    }
    public void ChangeVFXVolume()
    {
        AudioController.audioController.ChangeVFXVolume(VFX.value);
    }
}
