using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ansioso : Alunos
{
    void Chorar()
    {

    }
    protected override void Action1()
    {
        if (state == State.IDLE)
        {
            Transform mesa = GameController.controller.GetLocal();
            state = State.Walking;
            target = mesa.transform;
            Move(mesa);
        }
        else if (Chegou(target) && !isHappy)
        {
            state = State.CHORAR;
            emojis[6].SetActive(true);
        }
        else if(isHappy)
        {
            Nomarlizar();
        }
    }
}
