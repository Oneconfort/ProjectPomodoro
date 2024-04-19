using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    //LeanTween.ação
    //rotation;
    //Scale;
    //move;
    //color;
    //alpha
    public RectTransform[] obj;
    
    void Start()
    {
        //LeanTween.rotate(obj, 90, 2.0f);
        //LeanTween.color(obj, Color.blue, 2f);
        //LeanTween.color(obj, Color.white, 4f).setDelay(0.5f);
        //LeanTween.scale(obj, new Vector3(1f, 2f, 3f), 2f).setDelay(0.5f);
        //LeanTween.move(obj, new Vector3(122f, 222f, 322f), 2f);

        LeanTween.move(gameObject, transform.position + Vector3.left * 1000, 0);
        LeanTween.move(gameObject, transform.position, 0.5f);
        //LeanTween.rotate(GameObject, 11520f, 2.0f).setLoopPingPong();

        for (int i = 0; i < obj.Length; i++)
        {
            LeanTween.scale(obj[i], new Vector3(0, 0, 0), 0);
            LeanTween.scale(obj[i], new Vector3(1, 1, 1), 1f).setDelay(i * 0.5f).setEaseOutBounce();
        }

        //for (int i = 0; i < obj.Length; i++)
        //{
        //    LeanTween.scale(obj[i], new Vector3(0, 0, 0), 0);
        //    LeanTween.scale(obj[i], new Vector3(1, 1, 1), 0.5f).setDelay(i * 0.5f);
        //    LeanTween.color(obj[i], Color.blue, 2f).setLoopPingPong();
        //    LeanTween.color(obj[i], Color.white, 4f).setDelay(0.5f).setLoopPingPong();
        //}
    }
}
