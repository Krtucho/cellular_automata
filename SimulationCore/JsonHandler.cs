using Newtonsoft.Json;

namespace ConsoleApp1
{
    public static class JsonHandler{
        // Crear metodo para convertir un JSonToken a un diccionario de <string, object>
        public static Dictionary<string, object> ConvertJSonToken(JsonToken jsonToken){
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonToken.ToString());
        }
        public static Dictionary<string, object> ConvertJSonToken(string jsonToken){
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonToken);
        }
        // Metodo para abrir cualquier archivo(json) y convertir su contenido en un diccionario de <string, object>
        public static Dictionary<string, object> LoadJsonFile(string jsonPath){
            // Leer el archivo JSON
            string contenidoJson = File.ReadAllText(jsonPath);

            // Deserializar el contenido JSON en un diccionario
            var diccionario = JsonConvert.DeserializeObject<Dictionary<string, object>>(contenidoJson);
            // Read the file

            return diccionario;
        }
        // Crear metodo para guardar las celulas en un archivo json
        public static void SaveCells(CellChunk cellChunk){
            
        }

        internal static Dictionary<string, object> LoadStatesSettings(string jsonPath)
        {
            // Open the file
            // Implement code for open a json file using Newtonsoft.Json

            // Leer el archivo JSON
            string contenidoJson = File.ReadAllText(jsonPath);

            // Deserializar el contenido JSON en un diccionario
            var diccionario = JsonConvert.DeserializeObject<Dictionary<string, object>>(contenidoJson);
            // Read the file

            return diccionario;
        }

        // Save Any info in Json File
        public static void SaveJsonObject(dynamic jsonObject, string jsonPath){
            var json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(jsonPath, json);
        }

        // Crear metodo para guardar los organos en un archivo json   

        // Bito
        // Para cargar contenido desde un json
        // Load First
        public static Cell[,,] Cells;
        public static void InitializeFromJson(string json)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(json);
            var graph = jsonObject.graph.graph;

            // Get the dimensions of the matrix
            var maxX = 0;
            var maxY = 0;
            var maxZ = graph.nodes.Count;

            foreach (var node in graph.nodes)
            {
                maxX = Math.Max(maxX, node.Value.metadata.label.Value<int>());
                maxY = Math.Max(maxY, node.Value.metadata.title.Value<int>());
            }

            // Initialize the matrix
            Cells = new Cell[maxX + 1, maxY + 1, maxZ];

            // Populate the matrix with cell data
            foreach (var node in graph.nodes)
            {
                var index = int.Parse(node.Name);
                var cell = new Cell
                {
                    x = node.Value.metadata.label.Value<int>(),
                    y = node.Value.metadata.title.Value<int>(),
                    z = index,
                    state = 0 // Set the default state of the cell here
                };

                Cells[cell.x, cell.y, index] = cell;
            }
        }

        // Example
        public static void TestingLoadMatrix()
        {
            string json = @"
            {
                ""digraph"": {
                    ""graph"": {
                        ""metadata"": {},
                        ""directed"": true,
                        ""nodes"": {},
                        ""edges"": []
                    }
                },
                ""graph"": {
                    ""graph"": {
                        ""directed"": false,
                        ""edges"": [
                            {
                                ""metadata"": {
                                    ""color"": ""#9a9aff"",
                                    ""hover"": ""-0.4282""
                                },
                                ""source"": 1,
                                ""target"": 3
                            },
                            {
                                ""source"": 2,
                                ""target"": 3
                            }
                        ],
                        ""metadata"": {
                            ""node_border_color"": ""black"",
                            ""node_border_size"": 2,
                            ""node_color"": ""gray"",
                            ""node_opacity"": 0.7
                        },
                        ""nodes"": {
                            ""1"": {
                                ""metadata"": {
                                    ""label"": ""1"",
                                    ""title"": ""1""
                                }
                            },
                            ""2"": {
                                ""metadata"": {
                                    ""label"": ""2"",
                                    ""title"": ""2""
                                }
                            },
                            ""3"": {
                                ""metadata"": {
                                    ""label"": ""3"",
                                    ""title"": ""3""
                                }
                            }
                        }
                    }
                }
            }";

            InitializeFromJson(json);

            // Access the cells in the matrix using CellMatrix.Cells[x, y, z]
            // For example:
            Console.WriteLine(Cells[1, 1, 1].ToString());
        }

        // Then Save
        public static void ToJsonFile(string filePath, Cell[,,] cells, int[,,] indicesMatrix)
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
                        edges = new object[] { }
                    }
                },
                graph = new
                {
                    graph = new
                    {
                        directed = false,
                        edges = new object[] { },
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

            var json = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Example
        // public static void SaveExample()
        // {
        //     // Assuming you have already populated the CellMatrix.Cells with data

        //     string filePath = "output.json";
        //     filePath =// TODO: Change Path GeneralSettings.statesPath;
        //     Cell[,,] cells = CellManager.CreateRandomCell3DMatrix();//new Cell[3, 3, 3];
        //     int[,,] indicesMatrix = MatrixFunctions.GetIndicesMatrix(cells);
        //     ToJsonFile(filePath, cells, indicesMatrix);
        //     Console.WriteLine("JSON file generated successfully.");
        // }
    }
}