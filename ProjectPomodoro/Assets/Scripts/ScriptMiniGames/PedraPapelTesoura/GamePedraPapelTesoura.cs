using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePedraPapelTesoura : MonoBehaviour
{

    public Button buttonPedra;
    public Button buttonPapel;
    public Button buttonTesoura;
    public GameObject[] Resposta, escolha, imagens, imagensAnim;
    public Text resultText;

    private enum Choice { Pedra, Papel, Tesoura }
    private Choice playerChoice;
    private Choice aiChoice;

    void Start()
    {
        resultText.text = "Pedra, Papel e Tesoura ";
        buttonPedra.onClick.AddListener(() => OnPlayerChoice(Choice.Pedra));
        buttonPapel.onClick.AddListener(() => OnPlayerChoice(Choice.Papel));
        buttonTesoura.onClick.AddListener(() => OnPlayerChoice(Choice.Tesoura));
    }

    void OnPlayerChoice(Choice choice)
    {
        playerChoice = choice;
        aiChoice = (Choice)Random.Range(0, 3);

        ShowAIChoice();
        DetermineWinner();
    }
    void ShowAIChoice()
    {
        switch (aiChoice)
        {
            case Choice.Pedra:
                Resposta[0].SetActive(true);
                break;
            case Choice.Papel:
                Resposta[1].SetActive(true);
                break;
            case Choice.Tesoura:
                Resposta[2].SetActive(true);
                break;
        }
        for (int i = 0; i < imagens.Length; i++)
        {
            imagens[i].SetActive(false);
           
        }
        imagensAnim[0].SetActive(false);
        imagensAnim[1].SetActive(false);
    }
    void DetermineWinner()
    {
        if (playerChoice == aiChoice)
        {
            resultText.text = "Empate! Ambos escolheram " + playerChoice.ToString();
        }
        else if ((playerChoice == Choice.Pedra && aiChoice == Choice.Tesoura) ||
                 (playerChoice == Choice.Papel && aiChoice == Choice.Pedra) ||
                 (playerChoice == Choice.Tesoura && aiChoice == Choice.Papel))
        {
            resultText.text = "Você venceu! Você escolheu " + playerChoice.ToString() + " e seu amigo escolheu " + aiChoice.ToString();
        }
        else
        {
            resultText.text = "Você perdeu! Você escolheu " + playerChoice.ToString() + " e seu amigo escolheu " + aiChoice.ToString();
        }
        Invoke("FimGame", 5f);
    }
    void FimGame()
    {
        for (int i = 0; i < Resposta.Length; i++)
        {
            Resposta[i].SetActive(false);
            imagens[i].SetActive(true);
            escolha[i].SetActive(false);
        }
        imagensAnim[0].SetActive(true);
        imagensAnim[1].SetActive(true);
        resultText.text = "Pedra, Papel e Tesoura ";
        GameController.controller.miniGames[6].SetActive(false);
        GameController.controller.acabou = true;
    }
}