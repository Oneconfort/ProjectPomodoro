using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStatus : MonoBehaviour
{
    public bool finish;
    float cronometer;

    MoveConnect[] obj;

    void Start()
    {
        cronometer = 0;
        finish = false;
        obj = FindObjectsOfType<MoveConnect>();
    }

    void Update()
    {
        cronometer += Time.deltaTime;
        if (cronometer >= 0.2f)
        {
            cronometer = 0;
            int plus = 0;
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i].isConnect)
                {
                    plus++;
                }
            }
            if (plus >= obj.Length)
            {
                finish = true;
            }
        }
    }
}
