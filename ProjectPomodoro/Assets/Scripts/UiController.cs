using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.FullSerializer;

public class UiController : MonoBehaviour
{
    public GameObject uiPause, painelInicio, intervaloImagem, painelCreditos, painelOpcoes, painelDerrota, canvas;
    bool visivel, visivelUiPause;
    public Slider Master, Music, VFX;
    
    private void Start()
    {
        if (GameController.controller != null)
        {

            GameController.controller.uiController = this;
        }
        SetValores();
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
    void SetValores()
    {
        AudioController.audioController.audio.GetFloat("Master", out float auxiliar);
        if (Master != null) Master.value = auxiliar;
        AudioController.audioController.audio.GetFloat("Music", out float auxiliar2);
        if (Music != null) Music.value = auxiliar2;
        AudioController.audioController.audio.GetFloat("VFX", out float auxiliar3);
        if (VFX != null) VFX.value = auxiliar3;
    }
    public void ChangeAllVolume()
    {
        AudioController.audioController.ChangeAllVolume(Master.value);
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
