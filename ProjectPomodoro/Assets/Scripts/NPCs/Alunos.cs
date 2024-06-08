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

public enum State { Walking, ESTUDAR, DISCUTIR, COMER, IDLE, BRINCAR, VITIMA, BRAVO, BULLYING, QUEBRA, CHORAR};
public abstract class Alunos : MonoBehaviour
{
    NavMeshAgent agentAluno;

    public Transform target;

    private bool desenfilerou = false;
    private bool normalizou = false;
    protected bool isHappy = false;
    public bool isCalled;
    public bool inConflict;

    private float min = 1.5f;
    public State state;
    public GameObject[] emojis;
    public GameObject menuInteracao;
    public  int amizade = 10;
    public Slider barraAmizade;

    public Action actAtual;
    [SerializeField] private Queue<Action> IntervaloActs = new Queue<Action>();
    protected Animator animator;

    float tempoTotal = 20f; // a cada 20 segundos vai diminuir a amizade
    private float tempoAtual;
    protected abstract void Action1();


    private void Start()
    {
        state = State.IDLE;
        animator = GetComponent<Animator>();
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
        if (GameController.controller.isMiniGame == true ) { return; }
        if (!GameController.controller.isIntervalo)
        {
            if (!desenfilerou)
            {
                actAtual = IntervaloActs.Dequeue();
                desenfilerou = true;
                isCalled = false;
                inConflict = false;
                isHappy = false;
                normalizou = false;
                DeslisgarEmojis();
                ResetTree();
            }
            Estudar();
        }
        else 
        {
            if (desenfilerou)
            {
                ResetTree();
                DeslisgarEmojis();
                desenfilerou = false;
            }
            actAtual();
        }
        MudarEmoji();
      //  DecrescerAmizade();
    }

    //Metodo para criar as filas de afazeres de cada aluno, provavelmente vou mudar para os script de cada aluno
    void EnqueueTasks()
    {
        Action[] ativs = { Action1, Comer, Brincar, Action1, Action1 };
        int rnd1 = Random.Range(0, ativs.Length);
        int rnd2 = Random.Range(0, ativs.Length);
        if (rnd1 == rnd2)
        {
            rnd2 = (rnd1 + 1) % ativs.Length;
        }
        IntervaloActs.Enqueue(ativs[rnd1]);
        IntervaloActs.Enqueue(ativs[rnd2]);

    }
    public void Move(Transform target)
    {
        state = State.Walking;
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
    public void Parar()
    {
        if (!isHappy)
        {
            GameController.controller.isConflito = false;
            isHappy = true;
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
        if (amizade > 0)
        {
            GameController.controller.AtualizarPontos(quantidade);
        }
    }

    public void DecrescerAmizade() // diminui a amizade com o tempo
    {
       /* tempoAtual -= Time.deltaTime;

        if (tempoAtual <= 0f)
        {
            tempoAtual = 0f;
            //AumentarAmizade(-1);
            tempoAtual = tempoTotal;
        }*/
        AumentarAmizade(-1);
    }

    void Estudar()
    {
        if (state == State.IDLE)
        {
            target = GameController.controller.GetCadeira();
            animator.SetTrigger("Andar");
            Move(target);
        }
        else if (Chegou(target))
        {
            state = State.ESTUDAR;
            transform.eulerAngles = new Vector3(0, 0, 0);
            animator.SetBool("Estudar", true);
            emojis[0].SetActive(true);
        }
    }
    protected void Comer()
    {
        if (state == State.IDLE)
        {
            target = GameController.controller.GetMesa();
            animator.SetTrigger("Andar");
            Move(target);
        }
        else if (Chegou(target))
        {
            state = State.COMER;
            animator.SetBool("Comer", true);
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
            animator.SetTrigger("Andar");
            Move(target);
        }
        if (Chegou(target))
        {
            state = State.BRINCAR;
            animator.SetBool("Brincar", true);
            emojis[2].SetActive(true);
        }
        else
        {
            emojis[2].SetActive(false);
        }
    }
    public void Discutir()

    {
        if (state == State.IDLE)
        {
            isCalled = true;
            target = GameController.controller.GetLocal();
            Move(target);
            DeslisgarEmojis();

        }else if (Chegou(target) && !isHappy)
        {
            if (state == State.Walking)
            {
                if (!isCalled)
                {
                    GameController.controller.GetAluno(target, false);
                }
            }
            state = State.DISCUTIR;
            emojis[3].SetActive(true);
            DecrescerAmizade();
        }
        else
        {
            Nomarlizar();
        }
    }
    public void SejaVitima()
    {
        if (state != State.Walking && state != State.CHORAR && !isHappy)
        {
            Move(target);
            DeslisgarEmojis();
        }
        if (Chegou(target) && !isHappy)
        {
            state = State.CHORAR;
            emojis[6].SetActive(true);
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
            target = GameController.controller.GetMesa();
            normalizou = true;
        }
        else if (!Chegou(target))
        {
            animator.SetTrigger("Andar");
            DeslisgarEmojis();
            Move(target);
        }
        else
        {
            state = State.COMER;
            animator.SetBool("Comer", false);
            emojis[1].SetActive(true);
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
    protected void ResetTree()
    {
        state = State.IDLE;
        animator.SetTrigger("Acabou");
        animator.SetBool("Brigar", false);
        animator.SetBool("Estudar", false);
        animator.SetBool("Chorar", false);
        animator.SetBool("Brincar", false);
        animator.SetBool("Comer", false);
    }
}
