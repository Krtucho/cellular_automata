using Newtonsoft.Json;

namespace ConsoleApp1
{
    public static class MatrixFunctions
    {
        public static void GetNodeIndex()
        {
            // Definir las dimensiones de la matriz
            int dim1 = 3;
            int dim2 = 3;
            int dim3 = 3;

            // Crear la matriz tridimensional
            int[,,] matriz = new int[dim1, dim2, dim3];

            // Llenar la matriz con valores consecutivos
            int indice = 0;
            for (int i = 0; i < dim1; i++)
            {
                for (int j = 0; j < dim2; j++)
                {
                    for (int k = 0; k < dim3; k++)
                    {
                        matriz[i, j, k] = indice++;
                    }
                }
            }

            // Obtener el índice correspondiente a una posición dada
            int x = 1; // Posición en la dimensión 1
            int y = 2; // Posición en la dimensión 2
            int z = 1; // Posición en la dimensión 3

            int indicePosicion = matriz[x, y, z];

            Console.WriteLine("El índice correspondiente a la posición ({0}, {1}, {2}) es: {3}", x, y, z, indicePosicion);

            // Esperar a que el usuario presione una tecla para salir
            Console.ReadKey();
        }

        public static void VisualizeMatrix(Cell[,,] matriz)
        {
            int xLength = matriz.GetLength(0);
            int yLength = matriz.GetLength(1);
            int zLength = matriz.GetLength(2);

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    for (int k = 0; k < zLength; k++)
                    {
                        var cell = matriz[i, j, k];
                        System.Console.WriteLine(cell.GetType());
                        Console.WriteLine($"Cell[{i},{j},{k}] - state: {cell.state}, x: {cell.x}, y: {cell.y}, z: {cell.z}");

                    }
                }
            }
        }

        public static void VisualizeMatrix(int[,,] matriz)
        {
            int xLength = matriz.GetLength(0);
            int yLength = matriz.GetLength(1);
            int zLength = matriz.GetLength(2);

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    for (int k = 0; k < zLength; k++)
                    {
                        var cell = matriz[i, j, k];
                        System.Console.WriteLine(cell.GetType());
                        Console.WriteLine($"Cell[{i},{j},{k}] - index: {cell}");

                    }
                }
            }
        }

        public static void GetNodeIndexFunction()
        {
            Cell[,,] cells = new Cell[3, 3, 3];
            VisualizeMatrix(cells);
            int dim1 = 3;
            int dim2 = 3;
            int dim3 = 3;

            int x = 1; // Posición en la dimensión 1
            int y = 2; // Posición en la dimensión 2
            int z = 1; // Posición en la dimensión 3

            int[,,] indexMatrix = new int[dim1, dim2, dim3];
            for (int i = 0; i < dim1; i++)
                for (int j = 0; j < dim2; j++)
                    for (int k = 0; k < dim3; k++)
                        indexMatrix[i, j, k] = GetIndex(dim1, dim2, dim3, i, j, k);

            VisualizeMatrix(indexMatrix);

            int indice = GetIndex(dim1, dim2, dim3, x, y, z);

            Console.WriteLine("El índice correspondiente a la posición ({0}, {1}, {2}) es: {3}", x, y, z, indice);

            // Esperar a que el usuario presione una tecla para salir
            Console.ReadKey();
        }

        static int GetIndex(int dim1, int dim2, int dim3, int x, int y, int z)
        {
            int index = x * (dim2 * dim3) + y * dim3 + z;
            return index;
        }
        public static int[,,] GetIndicesMatrix(Cell[,,] matrix)
        {
            int dim1 = matrix.GetLength(0);
            int dim2 = matrix.GetLength(1);
            int dim3 = matrix.GetLength(2);

            int[,,] indicesMatrix = new int[matrix.GetLength(0), matrix.GetLength(1), matrix.GetLength(2)];

            for (int i = 0; i < dim1; i++)
                for (int j = 0; j < dim2; j++)
                    for (int k = 0; k < dim3; k++)
                        indicesMatrix[i, j, k] = GetIndex(dim1, dim2, dim3, i, j, k);

            return indicesMatrix;
        }

        public static int[,,] GetIndicesMatrix(OrganRegion organRegion)
        {
            int dim1 = organRegion.Cells.GetLength(0);
            int dim2 = organRegion.Cells.GetLength(1);
            int dim3 = organRegion.Cells.GetLength(2);

            int[,,] indicesMatrix = new int[dim1, dim2, dim3];

            for (int i = 0; i < dim1; i++)
                for (int j = 0; j < dim2; j++)
                    for (int k = 0; k < dim3; k++)
                        indicesMatrix[i, j, k] = GetIndex(dim1, dim2, dim3, i, j, k);

            return indicesMatrix;
        }

        // Save 3D Positions
        public static string ConvertToJson(Cell[,,] matrix, int[,,] indicesMatrix)
        {
            var jsonDict = new System.Collections.Generic.Dictionary<int, Point>();

            int xLength = matrix.GetLength(0);
            int yLength = matrix.GetLength(1);
            int zLength = matrix.GetLength(2);

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    for (int k = 0; k < zLength; k++)
                    {
                        var cell = indicesMatrix[i, j, k];
                        var point = matrix[i, j, k].point;
                        jsonDict.Add(cell, point);
                    }
                }
            }

            string json = JsonConvert.SerializeObject(jsonDict, Formatting.Indented);
            return json;
        }
        public static string ConvertToJson(Cell[,,] matrix, int[,,] indicesMatrix, bool list)
        {
            var jsonDict = new System.Collections.Generic.List<Cell>();

            int xLength = matrix.GetLength(0);
            int yLength = matrix.GetLength(1);
            int zLength = matrix.GetLength(2);

            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    for (int k = 0; k < zLength; k++)
                    {
                        var cell = matrix[i, j, k];
                        // var point = matrix[i, j, k].point;
                        jsonDict.Add(cell);
                    }
                }
            }

            string json = JsonConvert.SerializeObject(jsonDict, Formatting.Indented);
            return json;
        }
        public static void SaveCellsPositionsToJson(Cell[,,] cells = null, bool list = false, string filePath = "")
        {
            // Cell[,,] matriz = new Cell[2, 2, 2]
            // {
            // {
            //     { new Cell { x = 10, y = 911346, z = 2, state = 1 }, new Cell { x = 20, y = 911347, z = 3, state = 2 } },
            //     { new Cell { x = 30, y = 911348, z = 4, state = 3 }, new Cell { x = 40, y = 911349, z = 5, state = 4 } }
            // },
            // {
            //     { new Cell { x = 50, y = 911350, z = 6, state = 5 }, new Cell { x = 60, y = 911351, z = 7, state = 6 } },
            //     { new Cell { x = 70, y = 911352, z = 8, state = 7 }, new Cell { x = 80, y = 911353, z = 9, state = 8 } }
            // }
            // };

            if (cells == null)
            {
                cells = CellManager.CreateRandomCell3DMatrix();
            }
            if (list)
            {
                int[,,] indicesMatrix = GetIndicesMatrix(cells);
                string json = ConvertToJson(cells, indicesMatrix, list);
                if (filePath != "")
                {
                    File.WriteAllText(
                        filePath, json);
                }
                else
                {
                    File.WriteAllText(
                        Path.Combine(
                            GeneralSettings.configFilePath, "states", "positionsList.json"), json);
                }
            }
            else
            {
                int[,,] indicesMatrix = GetIndicesMatrix(cells);
                string json = ConvertToJson(cells, indicesMatrix);
                File.WriteAllText(
                    Path.Combine(
                        GeneralSettings.configFilePath, "states", "positions.json"), json);
            }
        }

    }
}