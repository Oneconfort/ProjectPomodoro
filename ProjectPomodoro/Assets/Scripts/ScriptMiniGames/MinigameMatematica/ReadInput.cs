using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI textMeshProUGUI;
    private string input, operador;
    public Text[] texts;
    private int resultado, intValue, valor1, valor2, contador;

    void Start()
    {
        textMeshProUGUI.text = "Matemática";
        inputField.ActivateInputField();
        Reinicio();
    }
    void Update()
    {
        VerificarResultado();
    }
    public void VerificarResultado()
    {
        if (intValue == resultado)
        {
            contador++;
            inputField.text = "";
            Reinicio();
        }
    }
    public void LerString(string s)
    {
        input = s;
        intValue = int.Parse(input);
    }
    public void Reinicio()
    {
        inputField.ActivateInputField();

        valor1 = Random.Range(1, 11);
        valor2 = Random.Range(1, 11);

        int randomValue = Random.Range(0, 4);

        switch (randomValue)
        {
            case 0:
                operador = "+";
                resultado = valor1 + valor2;
                break;
            case 1:
                operador = "-";
                resultado = valor1 - valor2;
                break;
            case 2:
                operador = "*";
                resultado = valor1 * valor2;
                break;
            case 3:
                operador = ":";
                if (valor1 < valor2)
                {
                    int aux = valor1;
                    valor1 = valor2;
                    valor2 = aux;
                    resultado = valor1 / valor2;
                }
                break;
            default:
                operador = "+";
                resultado = valor1 + valor2;
                break;
        }
        texts[0].text = valor1.ToString();
        texts[1].text = valor2.ToString();
        texts[2].text = operador;

        if (contador >= 4)
        {
            textMeshProUGUI.text = "Parabéns";
            Invoke("FimGame", 1.5f);
        }
    }
    public void FimGame()
    {
        textMeshProUGUI.text = "Matemática";
        contador = 0;
        Reinicio();
        GameController.controller.acabou = true;
    }
}
