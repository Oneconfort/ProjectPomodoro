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

public class Alunos : MonoBehaviour
{
    NavMeshAgent agentAluno;

    // [SerializeField] float speed;
    public GameObject[] emojis;
    private bool desenfilerou = false;

    int amizade = 10;
    public Slider barraAmizade;

    private Action actAtual;
    private Queue<Action> IntervaloActs = new Queue<Action>();


    private void Start()
    {
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
        if (GameController.controller.isIntervalo)
        {
            if (!desenfilerou)
            {
                actAtual= IntervaloActs.Dequeue();
                desenfilerou= true;
            }
            actAtual();
        }
        else
        {
            desenfilerou = false;   
            Estudar();
        }
        MudarEmoji();
    }

    //Metodo para criar as filas de afazeres de cada aluno, provavelmente vou mudar para os script de cada aluno
    void EnqueueTasks()
    {
        Action[] ativs = { Comer, Discutir, Brincar };
        int rnd1 = Random.Range(0, ativs.Length);
        int rnd2= Random.Range(0, ativs.Length);
        if (rnd1 == rnd2)
        {
            rnd2 = (rnd1+1)% ativs.Length;
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
        else if(amizade >= 4) 
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

    void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("Aluno"))
            {
                hit.collider.GetComponent<Alunos>().AumentarAmizade(1);
            }
        }
    }
    void AumentarAmizade(int quantidade)
    {
        amizade += quantidade;
        barraAmizade.value = amizade;
        if (amizade >= 10)
        {
            amizade = 10;
        }
    }
    void Estudar()
    {
        Transform cadeira= GameController.controller.GetCadeira();
        Move(cadeira);
        
        if (cadeira.Equals(transform.position))
        {
            emojis[0].SetActive(true);
        }
        else
        {
            emojis[0].SetActive(false);
        }

    }
    void Comer()
    {
        Transform mesa = GameController.controller.GetMesa();
        Move(mesa);
        if (Vector3.Distance(mesa.position, transform.position)<0.5f)
        {
            emojis[1].SetActive(true);
        }
        else
        {
            emojis[1].SetActive(false);
        }

    }
    void Brincar()
    {
        Transform local= GameController.controller.GetLocal();
        Move(local);
        if (local.Equals(transform.position))
        {
            emojis[2].SetActive(true);
        }
        else
        {
            emojis[2].SetActive(false);
        }
    }
    void Discutir()
    {
        Transform local= GameController.controller.GetLocal();
        Move (local);
        if (local.Equals(transform.position))
        {
            emojis[3].SetActive(true);
            amizade--;
            barraAmizade.value = amizade;
            if (amizade < 0)
            {
                amizade = 0;
            }
        }
        else
        {
            emojis[3].SetActive(false);
        }
    }
}
