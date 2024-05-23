using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    //Esses dois arrays servem para armazenar os transforms dos locais ondem acontecem as atividades
    //Locais: lugares onde v�o aconter a��es
    //Mesas: Assentos da cantina
    //Cadeiras: Assentos da sala

    [SerializeField] private Transform[] mesas, locais, cadeiras;
    [SerializeField] private GameObject[] alunos;
    [SerializeField] private Target[] tarMesas, tarLoc, tarCad;

    public UnlockManifolds games;
    public GameObject[] miniGames;
    public bool acabou = true;
    public GameObject[] cameras;
    public GameObject[] paresCorretos;
    public static GameController controller;
    public UiController uiController;
    public Player player;

    public bool isIntervalo, isMiniGame = false; //Ser� usado para determinar a movimenta��o dos alunos // isMiniGame para o tempo de tem mini game
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
    private void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
    }
    private void Start()
    {
        tempoAtual = tempoTotal;
        TempoAtual = TempoTotal;
        pontosAmizade = 10 * alunos.Length;
        totalPontos = pontosAmizade;
        Organize();
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
    //Os metodos abaixo retornam os transforms dos locais onde v�o acontecer as atividades 
    public Transform GetAluno(State mood)
    {
        for (int i = 0; i < alunos.Length; i++)
        {
            if (!alunos[i].gameObject.GetComponent<Alunos>().isCalled)
            {
                Transform target = GetLocal();
                alunos[i].gameObject.GetComponent<Alunos>().isCalled = true;
                alunos[i].gameObject.GetComponent<Alunos>().target = target;
                if (mood == State.DISCUTIR)
                {
                    alunos[i].gameObject.GetComponent<Alunos>().inConflict = true;
                }
                return target;
            }
        }
        return null;
    }
    public Transform GetCadeira()
    {
        for (int i = 0; i < tarCad.Length; i++)
        {
            if (!tarCad[i].IsOcupado)
            {
                tarCad[i].IsOcupado = true;
                return tarCad[i].Location;
            }
        }
        return null;

    }
    public Transform GetMesa()
    {
        for (int i = 0; i < tarMesas.Length; i++)
        {
            if (!tarMesas[i].IsOcupado)
            {
                tarMesas[i].IsOcupado = true;
                return tarMesas[i].Location;
            }
        }
        return null;
    }
    public Transform GetLocal()
    {
        for (int i = 0; i < tarLoc.Length; i++)
        {
            if (!tarLoc[i].IsOcupado)
            {
                tarLoc[i].IsOcupado = true;
                return tarLoc[i].Location;
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
        for (int i = 0; i < tarCad.Length; i++)
        {
            tarCad[i].IsOcupado = false;
        }
    }

    public void ReiniciarMesas()
    {
        for (int i = 0; i < tarMesas.Length; i++)
        {
            tarMesas[i].IsOcupado = false;
        }
    }

    public void ReiniciarLocais()
    {
        for (int i = 0; i < tarLoc.Length; i++)
        {
            tarLoc[i].IsOcupado = false;
        }
    }

    // Metodos dos Minigames 
    public void MiniGames()
    {
        numMinigames++;

        randomIndex = UnityEngine.Random.Range(0, 3); // � exclusivo 
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
    void Organize()
    {
        tarMesas = new Target[mesas.Length];
        tarLoc = new Target[locais.Length];
        tarCad = new Target[cadeiras.Length];

        for(int i = 0; i < tarMesas.Length; i++)
        {
            tarMesas[i] = new Target(mesas[i]);
        }
        for (int i = 0; i < tarLoc.Length; i++)
        {
            tarLoc[i] = new Target(locais[i]);
        }
        for(int i = 0; i < tarCad.Length; i++)
        {
            tarCad[i] = new Target(cadeiras[i]);
        }
    }
}
