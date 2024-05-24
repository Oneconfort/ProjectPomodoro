using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
   
    Rigidbody rb;
    float speed = 13;
    [SerializeField] private float maxDistance = 10f;
    float viewAngle = 110f;
    private bool isInteracting = false;
    public GameObject imageE;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameController.controller.player = this;
    }

    void FixedUpdate()
    {
        Interagir();
      //  Estudar();
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
        float v = Mathf.Clamp(Input.GetAxis("Vertical"), -0.45f, 0.45f);
        float h = Mathf.Clamp(Input.GetAxis("Horizontal"), -0.45f, 0.45f);
        Vector3 moveInput = new Vector3(h, 0, v);
        Vector3 moveVelocity = moveInput * speed;

        rb.MovePosition(transform.position + moveVelocity * Time.fixedDeltaTime);

        animator.SetFloat("Speed", moveVelocity.magnitude);

        if (moveInput != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Smooth rotation
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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Estudo"))
        {
            imageE.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("Estudar");
                MoveParaDestino(other.transform);
            }
        }
    }

    private void MoveParaDestino(Transform destino)
    {
        transform.position = destino.position;
        transform.rotation = destino.rotation;
    }
}