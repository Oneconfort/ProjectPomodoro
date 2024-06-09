using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conflituosos : Alunos
{
    bool chamei = false;
    protected override void Action1()
    {
        //Comer();
        if (state == State.IDLE)
        {
            isCalled = true;
            target = GameController.controller.GetLocal();
            Move(target);
            Debug.Log("Bulling!!!");
        }
        if (Chegou(target))
        {
            if (!chamei)
            {
                GameController.controller.GetAluno(target, true);
                chamei = true;
            }
            else
            if (!isHappy)
            {
                animator.SetBool("Brigar", true);
                DecrescerAmizade();
                state = State.BULLYING;
                emojis[2].SetActive(true);
            }
            else
            {
                Nomarlizar();
            }
        }
    }
  
}
