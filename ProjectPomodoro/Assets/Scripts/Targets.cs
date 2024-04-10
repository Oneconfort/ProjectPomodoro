using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    Transform target;
    public bool isOcupado;
    public Targets(Transform _target)
    {
        target = _target;
        isOcupado = false;
    }
    public Transform Target
    {
        get { return target; }
        set { target = value; }
    }   
}
