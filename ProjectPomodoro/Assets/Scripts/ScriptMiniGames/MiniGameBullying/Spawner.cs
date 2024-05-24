using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner spawner;
    private float h;
    float moveSpeed = 300;
    public GameObject[] objectToSpawn;
    public GameObject[] imagem;
    public Transform parentTransform;
    bool gerado = false;
    int randomIndex;
  
    private void Start()
    {
        randomIndex = Random.Range(0, 2); // É exclusivo 

    }
    private void Update()
    {
        Move();
        SpawnObject();
    }

    private void Move()
    {
        h = Input.GetAxis("Horizontal");
        if ((h < 0 && transform.position.x > 66f) || (h > 0 && transform.position.x < 1540f))
        {
            transform.position += Vector3.right * h * Time.deltaTime * moveSpeed;
        }

    }
    void SpawnObject()
    {
        if (gerado == false)
        {
            randomIndex = Random.Range(0, 2); // É exclusivo 
            gerado = true;
        }
        if (randomIndex == 0)
        {
            imagem[1].SetActive(false);
            imagem[0].SetActive(true);
        }
        if (randomIndex == 1)
        {
            imagem[0].SetActive(false);
            imagem[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
             gerado = false;
            GameObject spawnedObject = Instantiate(objectToSpawn[randomIndex], transform.position, Quaternion.identity);
            if (parentTransform != null)
            {
                spawnedObject.transform.SetParent(parentTransform);
            }
        }
    }

}
