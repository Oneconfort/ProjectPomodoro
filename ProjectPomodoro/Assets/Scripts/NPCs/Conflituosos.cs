using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conflituosos : Alunos
{
    protected override void Action1()
    {
        if (state == State.IDLE)
        {
            state = State.BULLYING;
            Transform local = GameController.controller.GetAluno(state);
            target = local.transform;
            state = State.Walking;
            Move(local);
        }
        if (Chegou(target))
        {
            if (!isHappy)
            {
                state = State.BULLYING;
                emojis[2].SetActive(true);
                DecrescerAmizade();
            }
            else
            {
                Nomarlizar();
            }
        }
    }
  
}
