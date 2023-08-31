namespace ConsoleApp1{
    public class CellType{}

    // Create a class for Cell
    public struct Cell{
        public int x;
        public int y;
        public int z;
        public Point point{get => new Point{x=x, y=y, z=z};}
        // iteration // Tells the iteration of the simulation. Used for know if in this iteration we have to update the cell
        // public List<Cell> neighbours;

        public int state; // State of the Cell
        // public Cell(int x, int y, int z, int state){
        //     this.x = x;
        //     this.y = y;
        //     this.z = z;
        //     this.state = state;
        // }
        public override string ToString(){
            return (x.ToString() + "," + y.ToString() + "," + z.ToString() +": "+ state.ToString());
        }
        public static bool operator ==(Cell a, Cell b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.state == b.state;
        }
        public static bool operator !=(Cell a, Cell b)
        {
            return !(a == b);
        }

    }

    public struct CellAdjacencyList{
        public int x;
        public int y;
        public int z;

        public HashSet<int[]> adjacentListPositions; // Lista de adyacencia de la celula
        // Crear metodos para agregar elementos a la lista de adyacencia
        public void AddNeighbour(int x, int y, int z){
            adjacentListPositions.Add(new int[] {x, y, z});
        }

        public void RemoveNeighbour(int x, int y, int z){
            adjacentListPositions.Remove(new int[] {x, y, z});
        }

        public void GetNeighbour(int x, int y, int z){}
    }

    public static class CellAdjacencyListHandler{
        public static CellAdjacencyList CreateCellAdjacencyList(int x, int y, int z){
            CellAdjacencyList cellAdjacencyList = new CellAdjacencyList{
                x=x,
                y=y,
                z=z,
                adjacentListPositions = new HashSet<int[]>()
            };
            return cellAdjacencyList;
        }

        public static void AddNeighbour(CellAdjacencyList cellAdjacencyList, int x, int y, int z){
            cellAdjacencyList.adjacentListPositions.Add(new int[] {x, y, z});
        }

        public static void RemoveNeighbour(CellAdjacencyList cellAdjacencyList, int x, int y, int z){
            cellAdjacencyList.adjacentListPositions.Remove(new int[] {x, y, z});
        }

        public static void GetNeighbour(CellAdjacencyList cellAdjacencyList, int x, int y, int z){}
    }

    public static class CellManager{
        public static Cell[,,] CreateRandomCell2DMatrix(){
            // Cell
            throw new NotImplementedException();
        }
        public static Cell[,,] CreateRandomCell3DMatrix(int dimX=3, int dimY=3, int dimZ=3){
            Cell[,,] matrix = new Cell[dimX, dimY, dimZ];

            for (int i = 0; i < dimX; i++)
                for (int j = 0; j < dimY; j++)
                    for (int k = 0; k < dimZ; k++)
                        matrix[i, j, k] = new Cell{x = i, y=j, z=k};//= GetIndex(dim1, dim2, dim3, i, j, k);
            return matrix;
        }
    }

}