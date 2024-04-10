using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Esses dois arrays servem para armazenar os transforms dos locais ondem acontecem as atividades
    //Locais: lugares onde vão aconter ações
    //Mesas: Assentos da cantina
    //Cadeiras: Assentos da sala

    [SerializeField]private Transform[] mesas, locais, cadeiras;
    [SerializeField] private bool[] isMesas, isLocais, isCadeiras;

    public static GameController controller;
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
        for(int i = 0; i < mesas.Length; i++)
        {
            isMesas[i] = true;
        }
        for(int j = 0; j < locais.Length; j++)
        {
            isLocais[j] = true;
        }
        for (int k = 0; k < cadeiras.Length; k++)
        {
            isCadeiras[k] = true;
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
    
    //Os metodos abaixo retornam os transforms dos locais onde vão acontecer as atividades 

    public Transform GetCadeira()
    {
        for(int i = 0; i < cadeiras.Length; i++)
        {
            if (isCadeiras[i])
            {
                isCadeiras[i] = false;
                return cadeiras[i];
            }
        }
        return null;

    }
    public Transform GetMesa()
    {
        for(int i = 0; i < mesas.Length; i++)
        {
            if (isMesas[i])
            {
                isMesas[i] = false;
                return mesas[i];
            }
        }
        return null;
    }
    public Transform GetLocal()
    {
        for (int i = 0; i < locais.Length; i++)
        {
            if (isLocais[i])
            {
                isLocais[i] = false;
                return locais[i];
            }
        }
        return null;
    }
}
