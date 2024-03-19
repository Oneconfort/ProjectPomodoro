using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform targetPlayer; 
    public Vector3 offset;

    void LateUpdate()
    {
        if (targetPlayer == null)
        {
            return;
        }

        transform.position = targetPlayer.position + offset;
        transform.LookAt(targetPlayer.position);
    }
}
