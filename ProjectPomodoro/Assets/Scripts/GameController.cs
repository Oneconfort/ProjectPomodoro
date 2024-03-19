using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static GameController controller;
    public UiController uiController;
    public Player player;

    void Awake()
    {
        if (controller == null)
        {
            controller = this;
        }
        Time.timeScale = 0.0f;
    }


}
