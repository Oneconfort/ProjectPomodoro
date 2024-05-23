using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Bardeneiro : Alunos
{
    
    protected override void Action1()
    {
        if (state == State.IDLE)
        {
            Transform mesa = GameController.controller.GetLocal();
            state = State.Walking;
            target = mesa.transform;
            Move(mesa);
        }
        else if (Chegou(target))
        {
            state = State.QUEBRA;
            emojis[3].SetActive(true);
        }
        else if (isHappy)
        {
           Nomarlizar();
        }
    }
}
