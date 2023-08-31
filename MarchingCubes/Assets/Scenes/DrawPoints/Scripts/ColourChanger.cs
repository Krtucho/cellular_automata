using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Color color;

    void Update() {
    color = Random.ColorHSV();  
    }
}
