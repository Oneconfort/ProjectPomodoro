using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjConectado : MonoBehaviour
{
    LineRenderer linha;
    Material mat;
    Transform conectado;

    void Start()
    {
        linha = GetComponent<LineRenderer>(); 
        mat = GetComponent<MeshRenderer>().material;
        Reinicio();
    }

    void Reinicio()
    {
        conectado = transform;
        mat.color = Color.white;
        //UpdateConexao();
    }

    void FixedUpdate()
    {
        linha.SetPosition(0, transform.position);
        linha.SetPosition(1, conectado.position);
    }

    private void OnMouseDown()
    {
        if (ControleSelecao.instancia.selecionado == null)
        {
            ControleSelecao.instancia.selecionado = transform;
        }
        else if (ControleSelecao.instancia.selecionado == transform)
        {
            Reinicio();
            ControleSelecao.instancia.selecionado = null;
        }
        else
        {
            conectado = ControleSelecao.instancia.selecionado;
            ControleSelecao.instancia.selecionado = null;
            //UpdateConexao();
        }
    }
}
