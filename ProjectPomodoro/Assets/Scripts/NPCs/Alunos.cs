using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Transactions;
using System;
using System.Linq;

public enum State { Walking, ESTUDAR, DISCUTIR, COMER, IDLE, BRINCAR, VITIMA, BRAVO, BULLYING };
public abstract class Alunos : MonoBehaviour
{
    NavMeshAgent agentAluno;

    public Transform target;

    private bool desenfilerou = false;
    private bool normalizou = false;
    protected bool isHappy = false;
    public bool isCalled;
    public bool inConflict;

    private float min = 1f;
    public State state;
    public State stateAtual;
    public GameObject[] emojis;
    public GameObject menuInteracao;
    int amizade = 10;
    public Slider barraAmizade;

    private Action actAtual;
    private Queue<Action> IntervaloActs = new Queue<Action>();


    float tempoTotal = 20f; // a cada 20 segundos vai diminuir a amizade
    private float tempoAtual;
    protected abstract void Action1();


    private void Start()
    {
        state = State.IDLE;
        stateAtual = State.IDLE;
        isCalled = false;
        inConflict = false;
        agentAluno = GetComponent<NavMeshAgent>();
        EnqueueTasks();
        tempoAtual = tempoTotal;
        if (barraAmizade != null)
        {
            barraAmizade.maxValue = amizade;
            barraAmizade.value = barraAmizade.maxValue;
        }
    }

    void FixedUpdate()
    {
        //if (GameController.controller.isMiniGame == true) { return; }
        if (GameController.controller.isIntervalo)
        {
            if (!isCalled)
            {
                if (!desenfilerou)
                {
                    actAtual = IntervaloActs.Dequeue();
                    state = State.IDLE;
                    DeslisgarEmojis();
                    desenfilerou = true;
                    isHappy = false;
                }
                actAtual();
            }
            else
            {
                if (!desenfilerou)
                {
                    actAtual = IntervaloActs.Dequeue();
                    desenfilerou = true;
                }
                if (inConflict)
                {
                    Brigar();
                }
                else
                {
                    SejaVitima();
                }
            }
        }
        else
        {
            if (desenfilerou || isCalled)
            {
                desenfilerou = false;
                isCalled = false;
                inConflict = false;
                isHappy = false;
                normalizou=false;
                DeslisgarEmojis();
                state = State.IDLE;
            }
            Estudar();
        }
        MudarEmoji();
        DecrescerAmizade();
    }

    //Metodo para criar as filas de afazeres de cada aluno, provavelmente vou mudar para os script de cada aluno
    void EnqueueTasks()
    {
        Action[] ativs = { Comer, Discutir, Brincar, Action1};
        int rnd1 = Random.Range(0, ativs.Length);
        int rnd2 = Random.Range(0, ativs.Length);
        if (rnd1 == rnd2)
        {
            rnd2 = (rnd1 + 1) % ativs.Length;
        }
        IntervaloActs.Enqueue(ativs[rnd1]);
        IntervaloActs.Enqueue(ativs[rnd2]);

    }
    protected void Move(Transform target)
    {
        agentAluno.SetDestination(target.transform.position);
    }
    void MudarEmoji()
    {
        if (amizade >= 7)
        {
            emojis[4].SetActive(true);
            emojis[5].SetActive(false);
            emojis[6].SetActive(false);
        }
        else if (amizade >= 4)
        {
            emojis[6].SetActive(false);
            emojis[4].SetActive(false);
            emojis[5].SetActive(true);
        }
        else
        {
            emojis[5].SetActive(false);
            emojis[6].SetActive(true);
        }

    }
    void DeslisgarEmojis()
    {
        for (int i = 0; i < emojis.Length; i++)
        {
            emojis[i].SetActive(false);
        }
    }

    public void AumentarAmizade(int quantidade)
    {
        if (quantidade > 0)
        {
            isHappy = true;
        }
        amizade += quantidade;
        barraAmizade.value = amizade;
        if (amizade >= 10)
        {
            amizade = 10;
        }
        if (amizade <= 0)
        {
            amizade = 0;
        }
        if (amizade > 0)
        {
            GameController.controller.AtualizarPontos(quantidade);
        }
    }

    public void DecrescerAmizade() // diminui a amizade com o tempo
    {
        tempoAtual -= Time.deltaTime;

        if (tempoAtual <= 0f)
        {
            tempoAtual = 0f;
            AumentarAmizade(-1);
            tempoAtual = tempoTotal;
        }
    }

    void Estudar()
    {
        if (state == State.IDLE)
        {
            target = GameController.controller.GetCadeira();
            state = State.Walking;
            Move(target);
        }
        else if (Chegou(target))
        {
            state = State.ESTUDAR;
            emojis[0].SetActive(true);
        }
    }
    void Comer()
    {
        if (state == State.IDLE)
        {
            target = GameController.controller.GetMesa();
            state = State.Walking;
            Move(target);
        }
        else if (Chegou(target))
        {
            state = State.COMER;
            emojis[1].SetActive(true);
        }
        else
        {
            emojis[1].SetActive(false);
        }

    }
    void Brincar()
    {
        if (state == State.IDLE)
        {
            target = GameController.controller.GetLocal();
            state = State.Walking;
            Move(target);
        }
        if (Chegou(target))
        {
            state = State.BRINCAR;
            emojis[2].SetActive(true);
        }
        else
        {
            emojis[2].SetActive(false);
        }
    }
    void Discutir()
    {
        if (state == State.IDLE)
        {
            state = State.DISCUTIR;
            Transform local = GameController.controller.GetAluno(state);
            target = local.transform;
            state = State.Walking;
            Move(local);
        }
        if (Chegou(target) && !isHappy)
        {
            state = State.DISCUTIR;
            DecrescerAmizade();
            emojis[3].SetActive(true);
        }
        else
        {
          Nomarlizar();
        }

    }
    void Brigar()
    {
        if (!Chegou(target) && !isHappy)
        {
            Move(target);
            state = State.Walking;
            DeslisgarEmojis();
        }
        if (Chegou(target) && !isHappy)
        {
            state = State.BRAVO;
            emojis[3].SetActive(true);
            DecrescerAmizade();
        }
        else
        {
            Nomarlizar();
        }
    }
    void SejaVitima()
    {
        if (!Chegou(target) && !isHappy)
        {
            Move(target);
            state = State.Walking;
            DeslisgarEmojis();
        }
        if (Chegou(target) && !isHappy)
        {
            state = State.VITIMA;
            emojis[3].SetActive(true);
            DecrescerAmizade();
        }
        else
        {
            Nomarlizar();
        }
    }
    protected void Nomarlizar()
    {
        if (!normalizou)
        {
            target = GameController.controller.GetLocal();
            normalizou = true;
        }
        else if (!Chegou(target))
        {
            state = State.Walking;
            DeslisgarEmojis();
            Move(target);
        }
        else
        {
            state = State.COMER;
            emojis[0].SetActive(true);
        }
    }
    //Metodo para verificar se o personagem chegou ao destino
    protected bool Chegou(Transform target)
    {
        if (Vector3.Distance(target.position, transform.position) < min)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
