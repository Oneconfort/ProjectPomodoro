using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Destroi : MonoBehaviour
{
    int moveSpeed = 350;
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
        if (collision.collider.tag == "Aluno")
        {
            Destroy(gameObject);
        }
    }
    private void Move()
    {
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }
}
