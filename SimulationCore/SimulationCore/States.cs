namespace ConsoleApp1{
    public struct StateInfo{
        public long id;
        public string name;
        // public CellType cellType; // Normal Cell, Tumor Cell, Inmune Cell, None=Space
        public string cellType;
        public string organRegion;
    }
    public struct StatesConfig{
        public Dictionary<long, StateInfo> statesInfo;
        // public StateInfo stateInfo;
        public StatesConfig(Dictionary<long, StateInfo> statesInfo = null){
            if(!(statesInfo == null))
                this.statesInfo = statesInfo;
            else
                this.statesInfo = new Dictionary<long, StateInfo>();

            // System.Console.WriteLine(this.statesInfo);
        }
    }

    public struct State{
        public StatesConfig statesConfig;
    }

    public static class StateManager{}

    // Struct para manejar la informacion de los estados de varias celdas contenidas en un chunk
    public struct ChunkState{}

    // Lo mismo de ChunkState pero contiene la info de varios Chunks
    public struct ChunksStates{}

    // Pide los estados e informacion correspondiente a cierto Chunk y sabe cuales retornar, diciendo cuales van
    // para el conjunto de AsyncCells y para el conjunto de SyncCells
    public static class ChunksStatesManager{
        public static ChunksStates GetChunksStates(Cell cell){
            return default(ChunksStates);//throw new NotImplementedException();
        }
        public static StateInfo GetCellStateFromOrganRegions(Cell cell){
            throw new NotImplementedException();
        }
        public static StateInfo GetCellState(Cell cell){
            if(cell.state < 0)
                return default(StateInfo);
            return GeneralSettings.StatesSettings.statesInfo[cell.state];
        }
    }
    
    // Interactia con el CellsChunksHandler para saber eficientemente que region buscar y como.
    public static class ChunksStatesHandler{}
}