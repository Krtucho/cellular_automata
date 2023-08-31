// using System.Diagnostics;
// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class GraphGenerator : MonoBehaviour {

//     public Material vertexMaterial;
//     public Material edgeMaterial;

//     // private List<Vertice> vertices = new List<Vertice>();
//     // private List<Arista> aristas = new List<Arista>();

// // Listas para almacenar vértices y aristas
// List<Vertex> vertices = new List<Vertex>();
// List<Edge> edges = new List<Edge>();


// // Función para crear un vértice
// Vertex CreateVertex(Vector3 position) 
// {
//     Vertex v = new GameObject("Vertex").AddComponent<Vertex>();
//     v.transform.localPosition = position;
//     v.Index = vertices.Count;
//     // Asignar material
//     // v.Rend = v.gameObject.AddComponent<MeshRenderer>();
//     // v.Collider = v.gameObject.AddComponent<SphereCollider>();
    

//     vertices.Add(v);

//     return v; 
// }

// // Función para crear una arista
// void CreateEdge(Vertex start, Vertex end)
// { 
//     Edge e = new GameObject("Edge").AddComponent<Edge>();
//     e.Start = start;
//     e.End = end; 
//     // Asignar material
//     e.Line = e.gameObject.AddComponent<LineRenderer>();
//     // e.Collider = e.gameObject.AddComponent<BoxCollider>();
//     // e.GetComponent<BoxCollider>().radius = 0.1f;
//     // e.GetComponent<BoxCollider>().height = Vector3.Distance(e.start.position, e.end.position);
//     // e.transform.position = (e.start.position + e.end.position) / 2;
//     // e.transform.LookAt(e.end.position);
//     // a.objeto.GetComponent<MeshRenderer>().material = edgeMaterial;
//     // a.objeto.GetComponent<MeshRenderer>().material.color = a.color;
//     e.Line.positionCount = 2;
//     e.Line.SetPosition(0, start.transform.localPosition);
//     e.Line.SetPosition(1, end.transform.localPosition);


//     edges.Add(e);

    
// }



//     class Vertice {
//         public Vector3 position;
//         public GameObject objeto;
//         public Color color;

//         public Vertice(Vector3 position, GameObject objeto, Color color) {
//             this.position = position;
//             this.objeto = objeto;
//             this.color = color;
//         }
//     }

//     class Arista {
//         public Vertice inicio;
//         public Vertice fin;
//         public GameObject objeto;
//         public Color color;

//         public Arista(Vertice inicio, Vertice fin, GameObject objeto, Color color) {
//             this.inicio = inicio;
//             this.fin = fin;
//             this.objeto = objeto;
//             this.color = color;
//         }

//         public override string ToString()
//         {
//             return String.Format("{0} -> {1} Object: {objeto}, color: {color}", inicio.position, fin.position, objeto, color);
//         }
//     }

    

//     void Start() {
        
//         // Generar vértices
//         Vertex v1 = CreateVertex(Vector3.zero); 
//         Vertex v2 = CreateVertex(Vector3.right);
//         Vertex v3 = CreateVertex(Vector3.up);
//         Vertex v4 = CreateVertex(Vector3.one);

//         // Generar aristas 
//         CreateEdge(v1, v2); 
//         CreateEdge(v2, v3);
//         CreateEdge(v3, v4);
//         CreateEdge(v4, v1);

//         // // Crear vértices
//         // Vertice v1 = new Vertice(new Vector3(0, 0, 0), GameObject.CreatePrimitive(PrimitiveType.Sphere), Color.red);
//         // Vertice v2 = new Vertice(new Vector3(1, 1, 0), GameObject.CreatePrimitive(PrimitiveType.Sphere), Color.green);
//         // Vertice v3 = new Vertice(new Vector3(-1, 1, 0), GameObject.CreatePrimitive(PrimitiveType.Sphere), Color.blue);

//         // // Agregar vértices a la lista
//         // vertices.Add(v1);
//         // vertices.Add(v2);
//         // vertices.Add(v3);

//         // // Crear aristas
//         // Arista a1 = new Arista(v1, v2, new GameObject(), Color.yellow);
//         // Arista a2 = new Arista(v2, v3, new GameObject(), Color.yellow);
//         // Arista a3 = new Arista(v3, v1, new GameObject(), Color.yellow);

//         // // Agregar aristas a la lista
//         // aristas.Add(a1);
//         // aristas.Add(a2);
//         // aristas.Add(a3);

//         // // Asignar materiales a los vértices y aristas
//         // foreach (Vertice v in vertices) {
//         //     v.objeto.GetComponent<MeshRenderer>().material = vertexMaterial;
//         //     v.objeto.GetComponent<MeshRenderer>().material.color = v.color;
//         // }

//         // foreach (Arista a in aristas) {
//         //     a.objeto.AddComponent<CapsuleCollider>();
//         //     a.objeto.GetComponent<CapsuleCollider>().radius = 0.1f;
//         //     a.objeto.GetComponent<CapsuleCollider>().height = Vector3.Distance(a.inicio.position, a.fin.position);
//         //     a.objeto.transform.position = (a.inicio.position + a.fin.position) / 2;
//         //     a.objeto.transform.LookAt(a.fin.position);
//         //     a.objeto.GetComponent<MeshRenderer>().material = edgeMaterial;
//         //     a.objeto.GetComponent<MeshRenderer>().material.color = a.color;
//         //     UnityEngine.Debug.Log(a.ToString());
//         // }
//     }
// }