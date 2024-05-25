using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Destroi : MonoBehaviour
{
    int moveSpeed = 350;
    public int tipo;
    private void Start()
    {
        Destroy(gameObject, 50);
    }
    private void Update()
    {
        Move();
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (tipo)
        {
            case 0:
                if (collision.collider.tag == "Aluno")
                {
                    Destroy(gameObject);
                }
                break;
            case 1:
              default:
                break;
        }
    }

    private void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }
}
