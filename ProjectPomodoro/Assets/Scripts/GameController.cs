using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    public GameObject[] miniGames;
    public bool acabou = true;
    public GameObject[] cameras;
    public GameObject[] paresCorretos;
    public static GameController controller;
    public UiController uiController;
    public Player player;

    public bool isIntervalo, isMiniGame = false; //Será usado para determinar a movimentação dos alunos // isMiniGame para o tempo de tem mini game
    public int pontosAmizade, numMinigames;
    public Slider barraPontosAmizade;
    //intervalo
    float tempoTotal = 30f;
    private float tempoAtual;
    int totalPontos;
    //fim de jogo
    float TempoTotal = 120f, TempoAtual;
    public Text textoDoTempo, textoPontos, textoMinigames, textoLigaLiga;
    //minigameLigaLiga
    int randomIndex;
    public int paresConectados = 0, totalPares = 3;

    public GameObject[] emogiFimGame;
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
        pontosAmizade = 10 * alunos.Length;
        totalPontos = pontosAmizade;
    }
    private void Update()
    {
        VerificarFechar();
        if (isMiniGame == true) { return; } // isMiniGame para o tempo se tem mini game
        TempoIntervalo();
        Tempo();
    }
    public void AtualizarPontos(int quantidade)
    {
        pontosAmizade += quantidade;
        barraPontosAmizade.value = pontosAmizade;
        if (pontosAmizade >= totalPontos)
        {
            pontosAmizade = totalPontos;
        }
        if (pontosAmizade <= 0)
        {
            pontosAmizade = 0;
        }
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
        textoMinigames.text = "Mini jogos:" + numMinigames;
        textoPontos.text = "Total de pontos:" + (pontosAmizade * numMinigames);
        if ((pontosAmizade * numMinigames) >= 500)
        {
            emogiFimGame[0].SetActive(true);
        }
        else if ((pontosAmizade * numMinigames) >= 300 && (pontosAmizade * numMinigames) < 500)
        {
            emogiFimGame[1].SetActive(true);
        }
        else
        {
            emogiFimGame[2].SetActive(true);
        }
        uiController.MostrarPainelFimDeJogo();
    }
    //Os metodos abaixo retornam os transforms dos locais onde vão acontecer as atividades 
    public Transform GetVitimas()
    {
        for (int i = 0; i < alunos.Length; i++)
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

    // Metodos dos Minigames 
    public void MiniGames()
    {
        numMinigames++;

        randomIndex = UnityEngine.Random.Range(0, 3); // É exclusivo 
        miniGames[randomIndex].SetActive(true);
        isMiniGame = true;

        if (randomIndex == 1)
        {
            uiController.canvas.SetActive(false);
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
        }
        else if (acabou == true)
        {
            miniGames[randomIndex].SetActive(false);
            acabou = false;
            isMiniGame = false;
            //aluno.AumentarAmizade(quantidade);
        }
    }
    public void VerificarFechar()
    {
        if (acabou == true && miniGames != null)
        {
            isMiniGame = false;
            miniGames[randomIndex].SetActive(false);
        }
    }

    public bool VerificarParCorreto(GameObject objeto1, GameObject objeto2)
    {
        if (((paresCorretos[0] == objeto1 && paresCorretos[1] == objeto2) || (paresCorretos[0] == objeto2 && paresCorretos[1] == objeto1)) || ((paresCorretos[2] == objeto1 && paresCorretos[3] == objeto2) || (paresCorretos[2] == objeto2 && paresCorretos[3] == objeto1)) || ((paresCorretos[4] == objeto1 && paresCorretos[5] == objeto2) || (paresCorretos[4] == objeto2 && paresCorretos[5] == objeto1)))
        {
            paresConectados++;
            return true;
        }
        return false;
    }
    public void FimLigaLiga()
    {
        acabou = true;
        textoLigaLiga.text = "Liga Liga";
        uiController.canvas.SetActive(true);
        cameras[1].SetActive(false);
        cameras[0].SetActive(true);
    }
}
