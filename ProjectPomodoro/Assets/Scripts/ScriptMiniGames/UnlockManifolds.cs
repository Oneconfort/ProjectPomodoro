using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManifolds : MonoBehaviour
{
    public List<Button> buttonsList;// lista dos botoes 
    public List<Button> shuffledButtons;// lista dos botoes embaralhado
    public TextMeshProUGUI textMesh;
    int counter = 0;
 
    public void Start()
    {
      RestartGame();
    }

    public void RestartGame()
    {
        textMesh.text = "Conte at� 10";
        counter = 0;
        shuffledButtons = buttonsList.OrderBy(a => Random.Range(0, 100)).ToList();//randomizar os numeros
        for (int i = 1; i < 11; i++)
        {
            shuffledButtons[i - 1].GetComponentInChildren<Text>().text = i.ToString();// testo correto dos numeros
            shuffledButtons[i - 1].interactable = true;//pode clicar em todos os botoes
            shuffledButtons[i - 1].image.color = new Color(177f / 255f, 220f / 255f, 233f / 255f, 1f); // cor inicial
        }
    }

    public void PressButton(Button button)
    {
        if (int.Parse(button.GetComponentInChildren<Text>().text) - 1 == counter)
        {
            counter++;
            button.interactable = false;// desativa o botao clicado
            button.image.color = Color.green;//se acertou
            if (counter == 10)//todos os botoes pressionados
            {
                textMesh.text = "Parab�ns!";
                Invoke("FimMiniGame", 3f);
            }
        }
        else
        {
            StartCoroutine(presentResult(false));// perde se nao clicou na sequencia
        }
    }

    public void FimMiniGame()
    {
        GameController.controller.acabou = true;
        RestartGame();
        counter = 0;
    }

    public IEnumerator presentResult(bool success)
    {
        if (!success) // se perdeu
        {
            foreach (var button in shuffledButtons)
            {
                button.image.color = Color.red;//cor vermelha se erro
                button.interactable = false;// para de interagir com o botao
            }
        }
        yield return new WaitForSeconds(3f);// espera 3 segundos
        RestartGame();// reseta
    }
}
