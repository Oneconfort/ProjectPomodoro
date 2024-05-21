using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Bardeneiro : Alunos
{
    void BrinPerigo()
    {
    }
    protected override void Action1()
    {
        if (state == State.IDLE)
        {
            Transform mesa = GameController.controller.GetMesa();
            state = State.Walking;
            target = mesa.transform;
            Move(mesa);
        }
        else if (Chegou(target))
        {
            state = State.COMER;
            emojis[1].SetActive(true);
        }
        else
        {
            emojis[1].SetActive(false);
        }
    }
}
