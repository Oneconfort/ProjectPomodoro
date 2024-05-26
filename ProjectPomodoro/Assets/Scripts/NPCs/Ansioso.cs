using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ansioso : Alunos
{
   
    protected override void Action1()
    {
        if (state == State.IDLE)
        {
            Transform mesa = GameController.controller.GetLocal();
            state = State.Walking;
            animator.SetTrigger("Andar");
            target = mesa.transform;
            Move(mesa);
        }
        else if (Chegou(target) && !isHappy)
        {
            state = State.CHORAR;
            animator.SetBool("Comer", true);
            emojis[7].SetActive(true);
        }
        else if(isHappy)
        {
            Nomarlizar();
        }
    }
}
