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

public enum State { Walking, ESTUDAR, DISCUTIR, COMER, IDLE, BRINCAR, VITIMA};
public class Alunos : MonoBehaviour
{
    NavMeshAgent agentAluno;
    
    public Transform target;
    private bool desenfilerou = false;
    private bool discussaoConcluida = false;
    public bool isVitima;
    private float min = 1f;
    public State state;
    public State stateAtual;
    public GameObject[] emojis;
    public GameObject menuInteracao;
    int amizade = 10;
    public Slider barraAmizade;

    private Action actAtual;
    private Queue<Action> IntervaloActs = new Queue<Action>();


    private void Start()
    {
        state = State.IDLE;
        stateAtual = State.IDLE;
        isVitima = false;
        agentAluno = GetComponent<NavMeshAgent>();
        EnqueueTasks();
        if (barraAmizade != null)
        {
            barraAmizade.maxValue = amizade;
            barraAmizade.value = barraAmizade.maxValue;
        }
    }

    void FixedUpdate()
    {
        if (GameController.controller.isMiniGame == true) { return; }
        if (GameController.controller.isIntervalo)
        {
            if (!isVitima)
            {
                if (!desenfilerou)
                {
                    actAtual = IntervaloActs.Dequeue();
                    state = State.IDLE;
                    DeslisgarEmojis();
                    desenfilerou = true;
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
                SejaVitima();
            }
        }
        else
        {
            if (desenfilerou || isVitima)
            {
                desenfilerou = false;
                isVitima = false;
                discussaoConcluida = false;
                DeslisgarEmojis();
                state = State.IDLE;
            }
            Estudar();
        }
        MudarEmoji();
    }

    //Metodo para criar as filas de afazeres de cada aluno, provavelmente vou mudar para os script de cada aluno
    void EnqueueTasks()
    {
        Action[] ativs = { Comer, Discutir, Brincar };
        int rnd1 = Random.Range(0, ativs.Length);
        int rnd2 = Random.Range(0, ativs.Length);
        if (rnd1 == rnd2)
        {
            rnd2 = (rnd1 + 1) % ativs.Length;
        }
        IntervaloActs.Enqueue(ativs[rnd1]);
        IntervaloActs.Enqueue(ativs[rnd2]);

    }
    void Move(Transform target)
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
    }
    void Estudar()
    {
        if (state == State.IDLE)
        {
            Transform cadeira = GameController.controller.GetCadeira();
            target = cadeira.transform;
            state = State.Walking;
            Move(cadeira);
        }
        else if (Chegou(target))
        {
            state = State.ESTUDAR;
            emojis[0].SetActive(true);
        }
        else
        {
            emojis[0].SetActive(false);
        }

    }
    void Comer()
    {
        if (state == State.IDLE)
        {
            Transform mesa = GameController.controller.GetMesa();
            state = State.Walking;
            target = mesa.transform;
            Move(mesa);
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
            Transform local = GameController.controller.GetLocal();
            target = local.transform;
            state = State.Walking;
            Move(local);
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
            stateAtual = State.DISCUTIR;
            Transform local = GameController.controller.GetVitimas();
            target = local.transform;
            state = State.Walking;
            Move(local);
            discussaoConcluida = false;
        }
        if (Chegou(target) && !discussaoConcluida)
        {
            state = State.DISCUTIR;
            emojis[3].SetActive(true);
            amizade--;
            barraAmizade.value = amizade;
            if (amizade < 0)
            {
                amizade = 0;
            }
            discussaoConcluida = true;
        }
       
    }
    void SejaVitima()
    {
        if (!Chegou(target) && !discussaoConcluida)
        {
            Move(target);
            state = State.Walking;
            DeslisgarEmojis();
        }
        if (Chegou(target) && !discussaoConcluida)
        {
            state = State.VITIMA;
            emojis[3].SetActive(true);
            amizade--;
            barraAmizade.value = amizade;
            if (amizade < 0)
            {
                amizade = 0;
            }
            discussaoConcluida = true;
        }
    }
    //Metodo para verificar se o personagem chegou ao destino
    bool Chegou(Transform target)
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
