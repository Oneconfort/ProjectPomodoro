using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5;
    [SerializeField] private float maxDistance = 10f;
    float viewAngle = 110f;
    private bool isInteracting = false;
    public GameObject imageE;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameController.controller.player = this;
    }

    void FixedUpdate()
    {
        Interagir();
    }

    void Update()
    {
        if (Time.timeScale == 0 || GameController.controller.isMiniGame == true) return;
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + moveInput * Time.fixedDeltaTime * speed);

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }
        if (moveInput.z != 0)
        {
            if (moveInput.z > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    //Interacao do player com os npcs usando RaycastHit para almentar amizade
    void Interagir()
    {
        Vector3 forward = transform.forward;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, forward, maxDistance);
        bool foundNPC = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Aluno"))
            {
                Vector3 directionToHit = hit.point - transform.position;
                float angle = Vector3.Angle(forward, directionToHit);

                if (angle <= viewAngle * 0.5f)
                {
                    imageE.SetActive(true); // Mostra o ícone "E" em cima do player para interagir

                    if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
                    {
                        isInteracting = true;
                        Alunos aluno = hit.collider.GetComponent<Alunos>();

                        if (aluno != null)
                        {
                            aluno.menuInteracao.SetActive(isInteracting);
                            //50% de chance de ter minigame
                            float randomValue = Random.value;
                            if (randomValue < 0.5f)
                            {
                                GameController.controller.acabou = false;
                                GameController.controller.MiniGames();
                            }
                        }
                    }
                    foundNPC = true;
                    break; // Sai do loop assim que encontrar um NPC
                }
            }
        }

        // Se nenhum NPC for encontrado e o jogador não está interagindo, esconde "E"
        if (!foundNPC && !isInteracting)
        {
            imageE.SetActive(false);
        }
        // Se nenhum NPC foi encontrado e o jogador estava interagindo, desliga o menu de interação
        if (!foundNPC && isInteracting)
        {
            isInteracting = false;
            Alunos[] allAlunos = FindObjectsOfType<Alunos>();
            foreach (Alunos aluno in allAlunos)
            {
                aluno.menuInteracao.SetActive(false);
            }
        }
    }
}