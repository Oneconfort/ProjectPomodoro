using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Personagens : MonoBehaviour
{
    public float startX = 0f;
    public float endX = 5f;
    public float speed = 2f;
    public int tipo;
    private float length;
  
    public Text texto;

    void Start()
    {
     
        length = Mathf.Abs(endX - startX);
    }

    void Update()
    {
        Move();
        DelayFimGameBullying();
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Coracao":
                if (tipo == 0)
                {
                    AtualizarPontosMiniGame(1);
                }
                if (tipo == 1)
                {
                    AtualizarPontosMiniGame(-1);
                }
                break;
            case "DesLike":
                if (tipo == 1)
                {
                    AtualizarPontosMiniGame(1);
                }
                if (tipo == 0)
                {
                    AtualizarPontosMiniGame(-1);
                }
                break;
        }
    }
    public void AtualizarPontosMiniGame(int quantidade)
    {
        GameController.controller.pontosMiniGamebullying += quantidade;
        //GameController.controller.uiController.barraPontosMiniGame[1].value = GameController.controller.pontosMiniGamebullying;
        //GameController.controller.uiController.barraPontosMiniGame[0].value = GameController.controller.pontosMiniGamebullying;
        if (GameController.controller.pontosMiniGamebullying >= 10)
        {
            GameController.controller.pontosMiniGamebullying = 10;
        }
        if (GameController.controller.pontosMiniGamebullying <= 0)
        {
            GameController.controller .pontosMiniGamebullying = 0;
        }
    }
    void Move()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, length);
        float newX = startX + pingPong;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    void DelayFimGameBullying()
    {
        if (GameController.controller.pontosMiniGamebullying >= 10)
        {
            texto.text = "Parabéns!";
            Invoke("FimGameBullying", 1f);
        }
    }
    void FimGameBullying()
    {
        GameController.controller.pontosMiniGamebullying = 0;
     //   GameController.controller.uiController.barraPontosMiniGame[0].value = 0;
       // GameController.controller.uiController.barraPontosMiniGame[1].value = 0;
        texto.text = "Mini Game";
        GameController.controller.acabou = true;
        GameController.controller.miniGames[4].SetActive(false);
    }
}
