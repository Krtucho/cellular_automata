using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGenerator : MonoBehaviour
{
    public GameObject pointPrefab; 
    public int numberOfPoints;

    void Start() {
        for (int i = 0; i < numberOfPoints; i++) {

            // Instantiate prefab and make it a child of this object
            GameObject point = Instantiate(pointPrefab, transform);

            // Rename point 
            point.name = "Point " + i;   
            
            // Set random position
            point.transform.position = Random.insideUnitSphere * 5;

            // Set color (example)
            point.GetComponent<MeshRenderer>().material.color 
            = Random.ColorHSV();  
            
            // Assign scripts            
            point.AddComponent<PointBehaviour>();
        }
    }
    // public int numberOfPoints;

    // void Start () {
    // for (int i = 0; i < numberOfPoints; i++) {
    //     GameObject point = new GameObject("Point " + i);
        
    //     // Assign scripts
    //     point.AddComponent<ColourChanger>();
    //     point.AddComponent<PointBehaviour>();
        
    //     // Set position
    //     point.transform.position = Random.insideUnitSphere * 5;
    // }
    // }
    // Update is called once per frame
    void Update()
    {
        
    }
}
