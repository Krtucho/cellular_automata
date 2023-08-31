using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehaviour : MonoBehaviour
{
    public List<GameObject> connections;
    public Material connectedMaterial;

    public void Highlight() {
    GetComponent<MeshRenderer>().material = connectedMaterial;  
    // ...
    }

    public void Influence() {
    // Resalta durante unos segundos   
    // Llama a la lógica necesaria
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
