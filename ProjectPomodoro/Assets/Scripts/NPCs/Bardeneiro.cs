using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bardeneiro : Alunos
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
            state = State.QUEBRA;
            animator.SetBool("Brigar", true);
            GameController.controller.isConflito = true;
            emojis[7].SetActive(true);
        }
        else if (isHappy)
        {
           Nomarlizar();
        }
    }
}
