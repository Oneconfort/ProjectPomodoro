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

    [SerializeField] private Transform[] mesas, locais, cadeiras;
    [SerializeField] private GameObject[] alunos;
    private bool[] isMesas, isLocais, isCadeiras;

    public UnlockManifolds games;
    public GameObject miniGames;
    
    public static GameController controller;
    public UiController uiController;
    public Player player;
    
    public bool isIntervalo, isMiniGame = false; //Será usado para determinar a movimentação dos alunos // isMiniGame para o tempo de tem mini game
    
    //intervalo
    float tempoTotal = 30f;
    private float tempoAtual;
   
    //fim de jogo
    float TempoTotal = 120f, TempoAtual;
    public Text textoDoTempo;

    void Awake()
    {
        isMesas = new bool[mesas.Length];
        isLocais = new bool[locais.Length];
        isCadeiras = new bool[cadeiras.Length];
        if (controller == null)
        {
            controller = this;
        }
        for (int i = 0; i < mesas.Length; i++)
        {
            isMesas[i] = true;
        }
        for (int j = 0; j < locais.Length; j++)
        {
            isLocais[j] = true;
        }
        for (int k = 0; k < cadeiras.Length; k++)
        {
            isCadeiras[k] = true;
        }
    }
    private void Start()
    {
        tempoAtual = tempoTotal;
        TempoAtual = TempoTotal;
    }
    private void Update()
    {
        if(isMiniGame == true) { return; } // isMiniGame para o tempo se tem mini game
        TempoIntervalo();
        Tempo();
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
        if (isIntervalo)
        {
            uiController.intervaloImagem.SetActive(true);
            ReiniciarTargets();
        }
        else
        {
            uiController.intervaloImagem.SetActive(false);
            ReiniciarTargets();
        }
    }
    void Tempo()
    {
        TempoAtual -= Time.deltaTime;

        if (TempoAtual <= 0f)
        {
            TempoAtual = 0f;
            PararJogo();

        }
        AtualizarTextoTempo();
    }
    void AtualizarTextoTempo()
    {
        int minutos = Mathf.FloorToInt(TempoAtual / 60f);
        int segundos = Mathf.FloorToInt(TempoAtual % 60f);

        string formatoTempo = string.Format("{0:00}:{1:00}", minutos, segundos);

        textoDoTempo.text = formatoTempo;
    }
    public void PararJogo()
    {
        Time.timeScale = 0.0f;
        uiController.MostrarPainelFimDeJogo();
    }
    //Os metodos abaixo retornam os transforms dos locais onde vão acontecer as atividades 
    public Transform GetVitimas()
    {
        for(int i = 0; i< alunos.Length; i++)
        {
            if (alunos[i].gameObject.GetComponent<Alunos>().stateAtual != State.DISCUTIR && !alunos[i].gameObject.GetComponent<Alunos>().isVitima)
            {
                Transform target = GetLocal();
                alunos[i].gameObject.GetComponent<Alunos>().isVitima = true;
                alunos[i].gameObject.GetComponent<Alunos>().target = target;
                return target;
            }
        }
        return null;
    }
    public Transform GetCadeira()
    {
        for (int i = 0; i < cadeiras.Length; i++)
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
        for (int i = 0; i < mesas.Length; i++)
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

    // metodos para reiniciar o uso dos locais 
    public void ReiniciarTargets()
    {
        ReiniciarCadeiras();
        ReiniciarMesas();
        ReiniciarLocais();
    }
    public void ReiniciarCadeiras()
    {
        for (int i = 0; i < cadeiras.Length; i++)
        {
            isCadeiras[i] = true;
        }
    }

    public void ReiniciarMesas()
    {
        for (int i = 0; i < mesas.Length; i++)
        {
            isMesas[i] = true;
        }
    }

    public void ReiniciarLocais()
    {
        for (int i = 0; i < locais.Length; i++)
        {
            isLocais[i] = true;
        }
    }
}
