using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MiniGameBaderneiro : MonoBehaviour
{
    public Canvas canvas;
    public GameObject imagePrefab; // Prefab da imagem a ser spawnada
    float spawnInterval = 0.9f; // Intervalo de tempo entre spawns
    private float timer;


    private int score = 0; // Variável para armazenar a pontuação
    public Text scoreText; // Referência ao texto da pontuação

    void Start()
    {
        GameController.controller.acabou = false;
        score = 10;
        timer = spawnInterval;
        UpdateScoreText(); 
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnImage();
            timer = spawnInterval;
        }
        if (score <= 0)
        {
            GameController.controller.acabou = true;
            score = 10;
            GameController.controller.miniGames[5].SetActive(false);
        }
    }

    void SpawnImage()
    {
        GameObject newImage = Instantiate(imagePrefab, canvas.transform);

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        Vector2 randomPosition = new Vector2(
            Random.Range(0, canvasRect.rect.width),
            Random.Range(0, canvasRect.rect.height));


        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, randomPosition, canvas.worldCamera, out Vector2 localPoint);
        newImage.GetComponent<RectTransform>().localPosition = localPoint;

        newImage.GetComponent<Button>().onClick.AddListener(() => OnImageClicked(newImage));
    }

    void OnImageClicked(GameObject clickedImage)
    {
        Destroy(clickedImage);
        score--; 
        UpdateScoreText(); 
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Bolinhas: " + score;
        }
       
    }
}