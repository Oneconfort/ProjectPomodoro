using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Novo Dialogo", menuName = "PDJ3_Aula/Dialogo", order = 1)]
public class Dialogo : ScriptableObject
{
    public string texto;
    public Sprite sprite; 
    public Dialogo proxDialogo;
}
