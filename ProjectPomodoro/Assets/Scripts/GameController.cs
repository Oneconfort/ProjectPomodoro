using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController controller;
    public Transform[] mesas, locais;
    public UiController uiController;
    public Player player;
    public bool isIntervalo; //Ser� usado para determinar a movimenta��o dos alunos
    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        Time.timeScale = 0.0f;
    }
     void Update()
    {

    }
}
