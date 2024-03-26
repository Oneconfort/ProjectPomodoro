using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alunos : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] string atvAtual; //Para saber qual atividade o aluno está fazendo no momento
    [SerializeField] int index; //Place-holder
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
    }
    void Move(Transform target)
    {
        //Place-holder para movimentação, será mais complexo
        if(Vector3.Distance(transform.position, target.position) != 0.01f)
        {
           transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
    void Estudar()
    {
    }
    void Comer()
    {
    }
    void Brincar()
    {
    }
    void Discutir()
    {
    }
    

}
