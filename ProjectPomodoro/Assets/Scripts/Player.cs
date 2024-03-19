using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    float speed = 5;

    void Start()
    {
        GameController.controller.player = this;
        rb = GetComponent<Rigidbody>();
    }

   

    void Update()
    {
        if (Time.timeScale == 0) return;
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + moveInput * Time.fixedDeltaTime * speed);

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        } 
        if (moveInput.z != 0)
        {
            if (moveInput.z > 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}

