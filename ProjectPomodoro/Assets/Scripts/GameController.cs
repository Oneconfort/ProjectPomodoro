using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController controller;
    public Transform[] mesas, locais;
    public UiController uiController;
    public Player player;
    public bool isIntervalo; //Será usado para determinar a movimentação dos alunos
    
    float tempoTotal = 30f;
    private float tempoAtual;
    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        Time.timeScale = 0.0f;
    }
    private void Start()
    {
        tempoAtual = tempoTotal;
    }
    private void Update()
    {
        TempoIntervalo();

    }
    void TempoIntervalo()
    {
        tempoAtual -= Time.deltaTime;

        if (tempoAtual <= 0f)
        {
            tempoAtual = 0f;
            isIntervalo = !isIntervalo; // altera entre true e false
            tempoAtual = tempoTotal;
        }
        if(isIntervalo)
        {
            uiController.intervaloImagem.SetActive(true);
        }
        else
        {
            uiController.intervaloImagem.SetActive(false);
        }
    }
}
