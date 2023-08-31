// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using System.IO;
// using System.Text.Json;

// public static class DataHandler
// {
//     public static class CellLoader
//     {
//         public static bool[,,] LoadCellsFromJson(string filePath)
//         {
//             string jsonString = File.ReadAllText(filePath);
//             List<Dictionary<string, object>> cellList = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);
//             int max_x = cellList.Max(cell => (int)cell["x"]);
//             int max_y = cellList.Max(cell => (int)cell["y"]);
//             int max_z = cellList.Max(cell => (int)cell["z"]);
//             bool[,,] boolCells = new bool[max_x + 1, max_y + 1, max_z + 1];
//             foreach (Dictionary<string, object> cellDict in cellList)
//             {
//                 int x = (int)cellDict["x"];
//                 int y = (int)cellDict["y"];
//                 int z = (int)cellDict["z"];
//                 int state = (int)cellDict["state"];
//                 boolCells[x, y, z] = (state >= 3);
//             }
//             return boolCells;
//         }
//     }

// }
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class DataHandler
{
    public static class CellLoader
    {
        public static bool[,,] LoadCellsFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            List<Dictionary<string, object>> cellList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
            int max_x = cellList.Max(cell => (int)cell["x"]);
            int max_y = cellList.Max(cell => (int)cell["y"]);
            int max_z = cellList.Max(cell => (int)cell["z"]);
            bool[,,] boolCells = new bool[max_x + 1, max_y + 1, max_z + 1];
            foreach (Dictionary<string, object> cellDict in cellList)
            {
                int x = (int)cellDict["x"];
                int y = (int)cellDict["y"];
                int z = (int)cellDict["z"];
                int state = (int)cellDict["state"];
                boolCells[x, y, z] = (state >= 3);
            }
            return boolCells;
        }
    }
}