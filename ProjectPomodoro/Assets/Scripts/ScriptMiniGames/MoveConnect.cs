using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveConnect : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 destPosition;
    Vector3 vetDerection;
    Rigidbody2D _rb;
    bool isMoving;
    float dis;

    [HideInInspector]
    public bool isConnect;

    [Range(1, 15)]
    public float moviSpeed = 10;
    [Space(10)]
    public Transform connect;
    [Range(0.1f, 2.0f)]
    public float disMinConnect = 0.5f;

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
        _rb.gravityScale = 1;
    }

    void OnMouseDown()
    {
        startPosition = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _rb.gravityScale = 0;
        isMoving = true;
        isConnect = false;
    }

    void OnMouseDrag()
    {
        destPosition = startPosition + Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vetDerection = destPosition - transform.position;
        _rb.velocity = vetDerection * moviSpeed;
    }

    void OnMouseUp()
    {
        _rb.gravityScale = 1;
        isMoving = false;
    }

    void FixedUpdate()
    {
        if (!isMoving && !isConnect)
        {
            dis = Vector2.Distance(transform.position, connect.position);
            if (dis < disMinConnect)
            {
                _rb.gravityScale = 0;
                _rb.velocity = Vector2.zero;
                transform.position = Vector2.MoveTowards(transform.position, connect.position, 0.02f);
            }
            if (dis < 0.01f)
            {
                isConnect = true;
                transform.position = connect.position;
            }
        }
    }
}
