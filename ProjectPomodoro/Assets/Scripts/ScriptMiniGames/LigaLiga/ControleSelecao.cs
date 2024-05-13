using UnityEngine;

public class ControleSelecao : MonoBehaviour
{
    public Transform selecionado;
    public static ControleSelecao instancia;

    void Start()
    {
        instancia = this;
    }
}
