using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1
{


    public static class CellSerializer
    {
        public static void SaveCellsToJson(Cell[,,] cells, string filePath)
        {
            List<Dictionary<string, object>> cellList = new List<Dictionary<string, object>>();
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    for (int k = 0; k < cells.GetLength(2); k++)
                    {
                        Dictionary<string, object> cellDict = new Dictionary<string, object>();
                        cellDict.Add("x", cells[i, j, k].x);
                        cellDict.Add("y", cells[i, j, k].y);
                        cellDict.Add("z", cells[i, j, k].z);
                        cellDict.Add("state", cells[i, j, k].state);
                        cellList.Add(cellDict);
                    }
                }
            }
            string jsonString = JsonSerializer.Serialize(cellList);
            File.WriteAllText(filePath, jsonString);
        }

        // Connections
        // TODO: Create SaveCellsToJson method
        public static void SaveCellsToJson(OrganRegion organRegion){
            var jsonObject = GetCellsFoJsonObject(organRegion.Cells, "", MatrixFunctions.GetIndicesMatrix(organRegion.Cells));
            SaveConnectionsToJson(organRegion.Cells, CellConnectionsHandler.GetConnections(organRegion), jsonObject);
        }
        public static dynamic GetCellsFoJsonObject(Cell[,,] cells, string filePath, int[,,] indicesMatrix) 
        {
            var jsonObject = new
            {
                digraph = new
                {
                    graph = new
                    {
                        metadata = new { },
                        directed = true,
                        nodes = new Dictionary<string, object>(),
                        edges = new List<dynamic>()
                    }
                },
                graph = new
                {
                    graph = new
                    {
                        directed = false,
                        edges = new List<dynamic>(),
                        metadata = new
                        {
                            node_border_color = "black",
                            node_border_size = 2,
                            node_color = "gray",
                            node_opacity = 0.7
                        },
                        nodes = new Dictionary<string, object>()
                    }
                }
            };

            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    for (int k = 0; k < cells.GetLength(2); k++)
                    {
                        var cell = cells[i, j, k];
                        var index = indicesMatrix[i, j, k].ToString();//(k + 1).ToString();
                        jsonObject.graph.graph.nodes[index] = new
                        {
                            metadata = new
                            {
                                label = cell.ToString(),
                                title = cell.ToString()
                            }
                        };
                    }
                }
            }
            Console.WriteLine(jsonObject.GetType());
            return jsonObject;
            // var json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            // File.WriteAllText(filePath, json);
        }
        // RODO: Uncomment this part and solve get the cells matrix from OrganRegionsData type
        // public static void SaveConnectionsToJson(OrganRegionsData organRegionsData, dynamic jsonObject = null)
        // {
        //     if (jsonObject == null)
        //     {

        //     }
        //     else
        //     {
        //         IEnumerable<Connection> connections = CellConnectionsHandler.GetCellConnections(organRegionsData);
        //         int[,,] indicesMatrix = GetIndicesMatrix()
        //         foreach (connection in connections)
        //         {
        //             index =
        //             // This part goes into JsonHandler
        //             jsonObject.graph.graph.nodes[index]
        //         }
        //     }
        // }
        public static void SaveConnectionsToJson(OrganRegion organRegion, dynamic jsonObject = null)
        {
            IEnumerable<Connection> connections = CellConnectionsHandler.GetConnections(organRegion);
            Cell[,,] cells = organRegion.Cells;
            SaveConnectionsToJson(cells, connections, jsonObject);
        }
        public static void SaveConnectionsToJson(Cell[,,] cells, IEnumerable<Connection> connections, dynamic jsonObject = null)
        {
            if (jsonObject == null)
            {

            }
            else
            {
                int[,,] indicesMatrix = MatrixFunctions.GetIndicesMatrix(cells);
                List<dynamic> edgesList = new List<dynamic>();
                foreach (Connection connection in connections)
                {
                    Cell v = connection.v;
                    Cell w = connection.w;
                    // TODO: Expand dimensions for the hole organ. Now it only get limits of this OrganRegion Cell
                    try{
                    int indexV = indicesMatrix[v.x, v.y, v.z];
                    int indexW = indicesMatrix[w.x, w.y, w.z];

                    if(indexV > indexW)
                        continue;
                    // This part goes into JsonHandler
                    edgesList.Add(
                        new
                        {
                            metadata = new
                            {
                                color = "#9a9aff",
                                hover = "-0.4282"
                            },
                            source = indexV,
                            target = indexW
                        }
                    );
                    }
                    catch{
                        continue;
                    }
                }
                foreach(var item in edgesList){
                    jsonObject.graph.graph.edges.Add(item);
                }
                // jsonObject.graph.graph.edges = edgesList;
                JsonHandler.SaveJsonObject(
                    jsonObject,
                    Path.Combine(GeneralSettings.configFilePath, "states", "graph.json"));
            }
        }
        public static void SaveOrganRegionToJson()
        {
            
        }
        public static void SaveOrganToJson() { }
    }

    public static class CellLoader
    {
        public static bool[,,] LoadCellsFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            List<Dictionary<string, object>> cellList = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonString);
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