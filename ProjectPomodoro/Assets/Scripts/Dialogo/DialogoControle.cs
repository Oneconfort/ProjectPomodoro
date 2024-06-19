using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoControle : MonoBehaviour
{
    public GameObject painelDialogo;
    public Image image;
    public Text texto;
    int contador = 0;
    public Dialogo dialogo;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            image.sprite = dialogo.sprite;
            texto.text = dialogo.texto;
            dialogo = dialogo.proxDialogo;
            contador++;
        }

        if( contador == 7)
        {
            painelDialogo.SetActive(false);
            GameController.controller.isMiniGame = false;
            GameController.controller.acabou = true;
        }
    }
}
