using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conflituosos : Alunos
{
    protected override void Action1()
    {
        Comer();
        /*if (state == State.IDLE)
        {
            target = GameController.controller.GetLocal();
            state = State.Walking;
            GameController.controller.GetAluno(target, true);
            Move(target);
            Debug.Log("Bulling!!!");
        }
        if (Chegou(target))
        {
            if (!isHappy)
            {
                state = State.BULLYING;
                emojis[2].SetActive(true);
                DecrescerAmizade();
            }
        }*/
    }
  
}
