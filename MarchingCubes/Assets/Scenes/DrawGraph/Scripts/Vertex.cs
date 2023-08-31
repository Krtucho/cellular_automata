using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Clase Vertex 
public class Vertex : MonoBehaviour
{
    public int Index;
    public MeshRenderer Rend;
    public SphereCollider Collider;

    void OnMouseDown() 
    {
        Rend.material.color = Color.red; // Cambiar a rojo al hacer clic
    }
}
