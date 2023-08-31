// using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;

    public string archivoDeDatos;
    public Root datos;

    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";

        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    // private void CargarDatos(){
    //     if(File.Exists(archivoDeGuardado)){
    //         string contenido = File.ReadAllText(archivoDeGuardado);
    //         datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

    //         UnityEngine.Debug.Log("Posicion Jugador : "+ datosJuego.posicion);
    //     }
    //     else
    //     {
    //         UnityEngine.Debug.Log("El archivo no existe");
    //     }
    // }

    private void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position
        };

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        UnityEngine.Debug.Log("Archivo Guardado");
    }

    // Loading Cells way 2
    public bool[,,] CargarDatos(string filePath = "")
    {
        if (filePath != "")
        {
            archivoDeDatos = filePath;
        }
        if (File.Exists(archivoDeDatos))
        {
            string contenido = File.ReadAllText(archivoDeDatos);

            // Deserializar el contenido del archivo JSON en una lista de objetos
            List<Datos> listaDatos = JsonConvert.DeserializeObject<List<Datos>>(contenido);

            // Acceder a los datos deserializados
            // foreach (Datos dato in listaDatos)
            // {
            //     int x = dato.x;
            //     int y = dato.y;
            //     int z = dato.z;
            //     int state = dato.state;

            //     // Realizar las operaciones necesarias con los datos
            //     // ...

            //     // Imprimir los datos
            //     UnityEngine.Debug.Log("x: " + x + ", y: " + y + ", z: " + z + ", state: " + state);
            // }
            int maxX = 0;
            int maxY = 0;
            int maxZ = 0;

            // Obtener los valores máximos de x, y, z
            foreach (var item in listaDatos)
            {
                maxX = Mathf.Max(maxX, item.x);
                maxY = Mathf.Max(maxY, item.y);
                maxZ = Mathf.Max(maxZ, item.z);
            }

            // Crear la matriz tridimensional
            bool[,,] matriz = new bool[maxX + 1, maxY + 1, maxZ + 1];

            // Asignar los valores a la matriz
            foreach (var item in listaDatos)
            {
                int x = item.x;
                int y = item.y;
                int z = item.z;
                int state = item.state;

                if (state > 2)
                {
                    matriz[x, y, z] = true;
                }
                else
                {
                    matriz[x, y, z] = false;
                }
            }

            // Imprimir la matriz
            // for (int z = 0; z <= maxZ; z++)
            // {
            //     for (int y = 0; y <= maxY; y++)
            //     {
            //         for (int x = 0; x <= maxX; x++)
            //         {
            //             UnityEngine.Debug.Log("Matriz[" + x + "," + y + "," + z + "] = " + matriz[x, y, z].ToString());
            //         }
            //     }
            // }
            return matriz;
        }
        else
        {
            UnityEngine.Debug.Log("El archivo no existe");
        }
        // if (File.Exists(archivoDeDatos))
        // {
        //     string contenido = File.ReadAllText(archivoDeDatos);
        //     datos = JsonConvert.DeserializeObject<Root>(contenido);
        //     // datos = JsonUtility.FromJson<Root>(contenido);

        // }
        // else
        // {
        //     UnityEngine.Debug.Log("El archivo no existe");
        // }
        return null;
    }



}
// Loading Cells
// private void CargarDatos()
// {
//     if (File.Exists(archivoDeGuardado))
//     {
//         string contenido = File.ReadAllText(archivoDeGuardado);
//         try
//         {
//             // Tu código aquí
//             Root root = JsonConvert.DeserializeObject<Root>(contenido);
//             // Root root = JsonUtility.FromJson<Root>(contenido);
//             UnityEngine.Debug.Log(root);
//             UnityEngine.Debug.Log(root.graph);
//             UnityEngine.Debug.Log(root.graph.edges);
//             UnityEngine.Debug.Log(root.graph.edges.Count);
//             UnityEngine.Debug.Log(root.graph.nodes);
//             UnityEngine.Debug.Log(root.graph.edges[0]);
//             UnityEngine.Debug.Log(root.graph.nodes["2"]);
//         }
//         catch (TypeLoadException ex)
//         {
//             UnityEngine.Debug.Log(ex.Message);
//         }

//         // Ahora puedes acceder a los datos en root
//         //Por ejemplo: 
//         // UnityEngine.Debug.Log("Posicion Jugador : " + root.digraph.graph.edges[0].source);
//     }
//     else
//     {
//         UnityEngine.Debug.Log("El archivo no existe");
//     }
// }
// 2nd way[Serializable]
public class Root
{
    public Dictionary<string, Data> datos;
}

[Serializable]
public class Data
{
    public int x;
    public int y;
    public int z;
    public int state;
}


// Clase para representar los datos en el archivo JSON
public class Datos
{
    public int x;
    public int y;
    public int z;
    public int state;
}



// Loading Cells
// [Serializable]
// public class Metadata
// {
//     public string color;
//     public string hover;
//     public string label;
// }

// [Serializable]
// public class Edge
// {
//     public Metadata metadata;
//     public int source;
//     public int target;
// }

// [Serializable]
// public class GraphMetadata
// {
//     public string node_border_color;
//     public string node_border_size;
//     public string node_color;
//     public string node_opacity;
// }

// [Serializable]
// public class NodeMetadata
// {
//     public string label;
//     public string title;
// }

// [Serializable]
// public class Node
// {
//     public NodeMetadata metadata;
// }

// [Serializable]
// public class Graph
// {
//     public bool directed;
//     public List<Edge> edges;
//     public GraphMetadata metadata;
//     public Dictionary<string, Node> nodes;
// }

// [Serializable]
// public class Digraph
// {
//     public Graph graph;
// }

// [Serializable]
// public class Root
// {
//     public Digraph digraph;
//     public Graph graph;
// }
