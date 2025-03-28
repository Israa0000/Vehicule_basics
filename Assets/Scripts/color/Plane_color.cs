using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color newColor = Color.blue; 

    void Start()
    {

        Renderer rend = GetComponent<Renderer>();

        rend.material.color = newColor;
    }
}