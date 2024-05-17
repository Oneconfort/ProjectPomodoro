using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  
    void Update()
    {
        // Garante que o canvas esteja sempre alinhado com a posição e rotação da câmera
        //   transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f; // Define a posição na frente da câmera
        if (Camera.main != null)
        {

            transform.LookAt(Camera.main.transform.position); // Mantém a mesma rotação da câmera
        }
    }
}
