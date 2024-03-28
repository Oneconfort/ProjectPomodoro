using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Encontra a c�mera principal na cena
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Garante que o canvas esteja sempre alinhado com a posi��o e rota��o da c�mera
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f; // Define a posi��o na frente da c�mera
        transform.rotation = mainCamera.transform.rotation; // Mant�m a mesma rota��o da c�mera
    }
}
