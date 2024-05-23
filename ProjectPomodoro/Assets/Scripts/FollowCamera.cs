using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  
    void Update()
    {
        // Garante que o canvas esteja sempre alinhado com a posi��o e rota��o da c�mera
        //   transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f; // Define a posi��o na frente da c�mera
        if (Camera.main != null)
        {

            transform.LookAt(Camera.main.transform.position); // Mant�m a mesma rota��o da c�mera
        }
    }
}
