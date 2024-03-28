using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Transactions;

public class Alunos : MonoBehaviour
{
    NavMeshAgent agentAluno;

    // [SerializeField] float speed;
    [SerializeField] string atvAtual; //Para saber qual atividade o aluno está fazendo no momento
    [SerializeField] int index; //Place-holder
    public GameObject[] emojis;
    private bool chegouAoDestino = false, chegouAoDestinoBrincar = false, chegouAoDestinoBrigar = false;

    int amizade = 10;
    public Slider barraAmizade;

    public delegate void Task();
    private List<Task> tasks = new List<Task>();

    private void Start()
    {
        agentAluno = GetComponent<NavMeshAgent>();

        StartTasks();
        if (barraAmizade != null)
        {
            barraAmizade.maxValue = amizade;
            barraAmizade.value = barraAmizade.maxValue;
        }
    }

    private void Update()
    {
        if (GameController.controller.isIntervalo)
        {
            Move(GameController.controller.locais[index]);
        }
        else
        {
            Move(GameController.controller.mesas[index]);
        }

        ExecutarTasks();
        MudarEmoji();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cadeira"))
        {
            chegouAoDestino = true;
        }
        if (other.CompareTag("Brincar"))
        {
            chegouAoDestinoBrincar = true;
        }
        if (other.CompareTag("Brigar"))
        {
            chegouAoDestinoBrigar = true;
            Discutir();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cadeira"))
        {
            chegouAoDestino = false;
        }
        if (other.CompareTag("Brincar"))
        {
            chegouAoDestinoBrincar = false;
        }
        if (other.CompareTag("Brigar"))
        {
            chegouAoDestinoBrigar = false;
            Discutir();
        }
    }
    void ExecutarTasks()
    {
        foreach (Task task in tasks)
        {
            task();
        }
    }
    void StartTasks()
    {
        tasks.Add(new Task(Estudar));
        tasks.Add(new Task(Comer));
        tasks.Add(new Task(Brincar));
        //  tasks.Add(new Task(Discutir));
    }

    void Move(Transform target)
    {
        //Place-holder para movimentação, será mais complexo
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
        if (GameController.controller.isIntervalo == false && chegouAoDestino == true)
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
        if (GameController.controller.isIntervalo == true && chegouAoDestino == true)
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
        if (GameController.controller.isIntervalo == true && chegouAoDestinoBrincar == true)
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
        if (GameController.controller.isIntervalo == false && chegouAoDestinoBrigar == true)
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
