using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personagens : MonoBehaviour
{
    public float startX = 0f;
    public float endX = 5f;
    public float speed = 2f;
    public int tipo;
    private float length;
    public Slider barraPontosMiniGame;
     int pontosMiniGame = 0;

    void Start()
    {
        length = Mathf.Abs(endX - startX);
    }

    void Update()
    {
        Move();
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
        pontosMiniGame += quantidade;
        barraPontosMiniGame.value = pontosMiniGame;
        if (pontosMiniGame >= 5)
        {
            pontosMiniGame = 5;
        }
        if (pontosMiniGame <= 0)
        {
            pontosMiniGame = 0;
        }
    }
    void Move()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, length);
        float newX = startX + pingPong;

        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
}
