using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaSwap : MonoBehaviour
{
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        Color color = rend.material.GetColor("_Color");
        color.a = 0.7f; // Set to whatever alpha you want
        rend.material.SetColor("_Color", color);
    }
}
