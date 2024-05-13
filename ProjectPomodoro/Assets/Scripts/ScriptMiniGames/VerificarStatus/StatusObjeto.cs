using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatusObjeto : MonoBehaviour
{
    public bool status = false;
    public UnityAction acao;
    Material mat;
   
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        mat.color = Color.red;
    }

    void OnMouseDown()
    {
        MudarStatus();
        acao();
    }

    void MudarStatus()
    {
        status = !status;
        Debug();
    }

    void Debug()
    {
        if(status)
        {
            mat.color = Color.green;
        }
        else
        {
            mat.color = Color.red;
        }
    }
}
