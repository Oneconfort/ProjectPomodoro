using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ObjConectado : MonoBehaviour
{
    LineRenderer linha;
    //Material mat;
    public Transform conectado;

    void Start()
    {
        conectado = transform;
        linha = GetComponent<LineRenderer>();
        Reinicio();
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
            if (GameController.controller.VerificarParCorreto(gameObject, conectado.gameObject))
            {
                // Os objetos estão conectados corretamente, mantenha a conexão
                if (GameController.controller.paresConectados == GameController.controller.totalPares)
                {
                    GameController.controller.textoLigaLiga.text = "Parabéns";
                    Invoke("FimLiga", 3f);
                }
            }
            else
            {
                // Os objetos não estão conectados corretamente, desfaça a conexão e notifique o jogador
                linha.endColor = Color.white;
                linha.startColor = Color.white;
                Invoke("Reinicio", 0.5f);
            }
            ControleSelecao.instancia.selecionado = null;
        }
    }
    void Reinicio()
    {
        conectado = transform;
    }
    public void FimLiga()
    {
        ReiniciarTodos();
        GameController.controller.FimLigaLiga();
        GameController.controller.paresConectados = 0;
    }
    public static void ReiniciarTodos()
    {
        ObjConectado[] todosObjetos = FindObjectsOfType<ObjConectado>();
        foreach (ObjConectado obj in todosObjetos)
        {
            obj.Reinicio();
        }
    }
}
