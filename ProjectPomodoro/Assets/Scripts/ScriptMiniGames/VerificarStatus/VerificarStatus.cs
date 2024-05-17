using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListaStatus
{
    public StatusObjeto obj;
    public bool estado;
}
public class VerificarStatus : MonoBehaviour
{
    public ListaStatus[] statusListas;

    void Start()
    {
        foreach (ListaStatus status in statusListas)
        {
            status.obj.acao = UpdateStatus;
        }   
    }

    void UpdateStatus()
    {
        bool vitoria = true;
        foreach (ListaStatus estatus in statusListas)
        {
            if (estatus.obj.status != estatus.estado) vitoria = false;
        }
        if(vitoria)
        {

        }
    }
}
