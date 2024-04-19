using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemAnimation : MonoBehaviour
{
    void Start()
    {
        LeanTween.move(gameObject, transform.position + Vector3.right * 1000, 0);
        LeanTween.move(gameObject, transform.position, 5f);
    }
}
