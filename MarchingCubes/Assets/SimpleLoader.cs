using UnityEngine;
using System.IO;

public class SimpleLoader : MonoBehaviour
{
    [System.Serializable]
    public class ItemData
    {
        public int x;
        public int y;
        public int z;
        public int state;
    }

    public string filePath;// = "path/to/your/json/file.json"; // Ruta de tu archivo JSON
    void Start()
    {

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            ItemData[] jsonData = JsonHelper.FromJson<ItemData>(json);

            bool[,,] matrix = new bool[jsonData.Length, 1, 1];

            for (int i = 0; i < jsonData.Length; i++)
            {
                int state = jsonData[i].state;

                if (state >= 2)
                {
                    int x = jsonData[i].x;
                    int y = jsonData[i].y;
                    int z = jsonData[i].z;

                    matrix[x, y, z] = true;
                }
                else
                {
                    matrix[i, 0, 0] = false;
                }
            }

            // Utiliza la matriz "matrix" según tus necesidades
        }
        else
        {
            Debug.LogError("El archivo JSON no existe en la ruta especificada.");
        }
    }
}

// Clase de ayuda para deserializar un array de objetos JSON
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}