using System;
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
    public GameObject imageE, painelAulaMiniGame;
    Animator animator;

    int interacaoAluno = 0, interacaoEstudo =0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameController.controller.player = this;
        AnalyticsTest.instance.AddAnalytics("Game", "Start", DateTime.Now.ToString("d/M/y hh:mm"));
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
        float v = Mathf.Clamp(Input.GetAxis("Vertical"), -0.45f, 0.45f);
        float h = Mathf.Clamp(Input.GetAxis("Horizontal"), -0.45f, 0.45f);

        if (Mathf.Abs(h) > 0 && Mathf.Abs(v) > 0)
        {
            if (Mathf.Abs(h) > Mathf.Abs(v))
            {
                v = 0;
            }
            else
            {
                h = 0;
            }
        }

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
                    imageE.SetActive(true); 

                    if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
                    {
                        isInteracting = true;
                        Alunos aluno = hit.collider.GetComponent<Alunos>();

                        if (aluno != null)
                        {
                            aluno.menuInteracao.SetActive(isInteracting);
                            interacaoAluno++;
                            AnalyticsTest.instance.AddAnalytics("Player", "Interaçao com Aluno", interacaoAluno.ToString());
                            GameController.controller.acabou = false;
                            
                        }
                    }
                    foundNPC = true;
                    break; 
                }
            }
        }
        if (!foundNPC && !isInteracting)
        {
            imageE.SetActive(false);
        }
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
            if (!GameController.controller.isIntervalo)
            {
                imageE.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    animator.SetTrigger("Estudar");
                    interacaoEstudo++;
                    AnalyticsTest.instance.AddAnalytics("Player", "Interaçao Estudo", interacaoEstudo.ToString());
                    MoveParaDestino(other.transform);
                    painelAulaMiniGame.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Estudo"))
        {
            painelAulaMiniGame.SetActive(false);
        }
    }
    private void MoveParaDestino(Transform destino)
    {
        transform.position = destino.position;
        transform.rotation = destino.rotation;
    }
    private void OnDestroy()
    {
        AnalyticsTest.instance.AddAnalytics("Player", "Die", "1");
        AnalyticsTest.instance.Save();
    }
}