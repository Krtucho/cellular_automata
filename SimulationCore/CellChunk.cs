namespace ConsoleApp1{
    // Clase para guardar matriz con ciertas celdas de organo
    public struct CellChunk{ // TODO: Change CellChunk to Chunk
        // Variable que almacena el limite mas a la izquierda 
        // en x, el chunk es una caja cuadrada que tiene un punto inicial
        /// <summary>
        /// Limite izquierdo en x
        /// </summary>
        public int x; // Limite izquierdo en x
        public int y; // Limite superior en y
        public int z; //  Limite de tope de profundidad en z
        public int width;
        public int height;
        public int depth;
        public Cell[] cells;
    }

    public static class CellChunkHandler{
        public static CellChunk CreateCellChunk(int x, int y, int z, int width, int height, int depth){
            CellChunk CellChunk = new CellChunk{
                x=x,
                y=y,
                z=z,
                width=width,
                height=height,
                depth=depth,
                cells = new Cell[width*height*depth]
            };
            return CellChunk;
        }

        public static void AddCell(CellChunk CellChunk, Cell cell){}
    }

    // Manage the chunks distribution of the entire Organ
    public static class CellChunksManager{
        // Static variables
        public static int width;
        public static int height;
        public static int depth;
        public static int chunksDivision;


        private static int GetCellIndex(int x, int y, int z)
        {
            return x + y * width + z * width * height;
        }

        public static Cell GetCell(int x, int y, int z){
            // Get the CellChunk where the Cell is
            CellChunk cellChunk = GetChunk(new Cell{x=x, y=y, z=z});
            // Get the Cell from the CellChunk
            return cellChunk.cells[GetCellIndex(x, y, z)];
        }

        public static CellChunk[] GetAutomatonChunks(CellularAutomaton cellularAutomaton){
            // Get every Chunk for every Organ
            //cellularAutomaton.Organs;
            return new CellChunk[]{};
        }

        // Get CellChunk for some Cell who has neighbours on that Chunk
        public static CellChunk GetChunk(Cell cell){return default(CellChunk);}
        // Get CellChunk for Adjacent List
        public static CellChunk[] GetAdjacentListChunks(Cell cell){
            return new CellChunk[] {default(CellChunk)};
        }
        // Get Chunks near cell, including the cell chunk, and the border chunks
        public static CellChunk[] GetChunks(Cell cell, bool includeCellChunk, bool includeBorderChunks){
            return new CellChunk[] {default(CellChunk)};
        }
        // Get nearest AsyncCells to Cell
        // Follow a BFS Strategy finding the nearest AsyncCells
        public static Cell[] GetNearestAsyncCells(Cell cell, AsyncCellsInfo asyncCellsInfo, AsyncCellsStage asyncCellsStage)
        {
            return new Cell[] {default(Cell)};
        }
        /// <summary>
        /// Get Info about neighbours of cell. Scope means hoy far is the range of the search.\n
        /// Ex: Region, search neighbours  of the cell inside this Region. OrganRegion, only search neighbours of the cell on the same OrganRegion.
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="organRegion">Scope of the search, in this case is a Region of an Organ (OrganRegion)</param>
        /// <returns></returns>
        public static NeighbourhoodInfo GetNeighbourhoodInfo(Cell cell, OrganRegion organRegion, OrganRegionsData organRegionsData){
            return new NeighbourhoodInfo(organRegion.SearchCell(cell), organRegion, organRegionsData);
        }

        // Generate Empty Chunks with Cells and size
    
        // Generate Chunks with Cells and size
        public static CellChunk[] GenerateChunks(Cell[] cells, int width, int height, int depth, int chunksDivision){
            // Dividir en pedazos de cuadrados con ancho, largo y alto iguales a chunksDivision
            // Crear un CellChunk por cada pedazo
            if(chunksDivision > width || chunksDivision > height || chunksDivision > depth){
                throw new Exception("ChunksDivision must be less than width, height and depth");
            }

            List<CellChunk> cellChunks = new List<CellChunk>();//CellChunk[] cellChunks = new CellChunk[width*height*depth];

            for(int i = 0; i< width; i+=chunksDivision){
                for(int j = 0; j< height; j+=chunksDivision){
                    for(int k = 0; k< depth; k+=chunksDivision){
                        CellChunk cellChunk = CellChunkHandler.CreateCellChunk(i, j, k, i + chunksDivision < width ? width : width-i, chunksDivision, chunksDivision);
                        // Asignar las celdas a cada CellChunk
                        cellChunks.Add(cellChunk);

                    }
                }
            }

            // Asignar las celdas a cada CellChunk
            // Retornar los CellChunks

            
            return cellChunks.ToArray();//new CellChunk[] {default(CellChunk)};
        }



    }

    
}