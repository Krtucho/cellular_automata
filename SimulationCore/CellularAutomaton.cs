using System.Collections.Generic;
namespace ConsoleApp1{
    public class CellularAutomaton{
        /// Diccionario para tener todos los organos que contiene este automata celular
        // En Un princpio se deseaba hacer un grafo no dirigido, asi se tenian las interacciones entre cada organo
        // Se supone que cada organo tiene un id, el cual sera para buscar en el diccionario como key y como value
        // se tendra una lista con todos los organos a los cuales este hace referencia, es decir, se encuentran cerca.
        public Dictionary<int, List<Organ>> Organs;
        
        public NetworkSettings networkSettings;
        public SmallWorldNetwork smallWorldNetwork;

        // Contiene todos los niveles de las celulas asyncronas en la simulacion.
        // De esta forma sabemos cuales celulas actualizar primero y cuales despues
        public AsyncCellsStages asyncCellsStages;

        public CellularAutomaton(){
            Organs = new();
        }
    }

    // Create a class for simulate the process of CellularAutomaton
    public static class CellularAutomatonHandler{
        // public static void Simulate(CellChunk cellChunk){

        // }
        // public static void Simulate(CellChunk cellChunk, int iterations){

        // }
        // public static void Simulate(CellChunk cellChunk, int iterations, int time){

        // }
        // public static void Simulate(CellChunk cellChunk, int iterations, int time, int speed){

        // }
        public static string GetSet(Cell cell){
            // If the cell is in state of migration, bloodstream or metastasis. Set AsyncSet
            // Otherwise set SyncSet
            return new Random().Next(0, 2) == 0 ? "AsyncSet" : "SyncSet";
        }

        public static HashSet<Cell> ExtractAsyncCells(CellChunk cellChunk){
            return null;
        }
        // public static HashSet<Cell> ExtractSyncCells(CellChunk cellChunk){
            // Crear un HashSet
            // HashSet<int> set = new HashSet<int>();

            // // Cell's matrix
            // Cell[,,] matrix = cellChunk.Cells;
            // // Iterar a través de cada elemento en la matriz tridimensional
            // for (int x = 0; x < matrix.GetLength(0); x++)
            // {
            //     for (int y = 0; y < matrix.GetLength(1); y++)
            //     {
            //         for (int z = 0; z < matrix.GetLength(2); z++)
            //         {
            //             // Agregar el elemento a el HashSet
            //             set.Add(matrix[x, y, z]);
            //         }
            //     }
            // }

            // return set;
        //     return null;
        // }
        // public static HashSet<Cell> ExtractSyncCells(OrganRegion organRegion){
        //     // Crear un HashSet
        //     HashSet<int> set = new HashSet<int>();

        //     // Cell's matrix
        //     Cell[,,] matrix = cellChunk.Cells;
        //     // Iterar a través de cada elemento en la matriz tridimensional
        //     for (int x = 0; x < matrix.GetLength(0); x++)
        //     {
        //         for (int y = 0; y < matrix.GetLength(1); y++)
        //         {
        //             for (int z = 0; z < matrix.GetLength(2); z++)
        //             {
        //                 // Agregar el elemento a el HashSet
        //                 set.Add(matrix[x, y, z]);
        //             }
        //         }
        //     }

        //     return set;
        // }
        public static HashSet<Cell> ExtractSyncCells(OrganRegion organRegion){
            HashSet<Cell> cells = new();
            
            foreach(Cell cell in organRegion.Cells)
                if(cell.state < 3)
                    cells.Add(cell);

            return cells;
        }

        public static void UpdateAsyncCellsAtStage(AsyncCellsStage asyncCellsStage, AsyncCellsInfo asyncCellsInfo, CellularAutomaton cellularAutomaton){
            while (asyncCellsInfo.cellsCount > 0)
            {
                UpdateAsyncCellAtStage(asyncCellsStage, asyncCellsInfo, cellularAutomaton);
            }
        }

        // Implement UpdateAsyncCellsAtStage
        public static void UpdateAsyncCellAtStage(AsyncCellsStage asyncCellsStage, AsyncCellsInfo asyncCellsInfo, CellularAutomaton cellularAutomaton){
            // Select a random cell from the AsyncSet from the AsyncCellsInfo
            Cell cell = asyncCellsInfo.GetRandomCellAndRemove(asyncCellsStage);

            OrganRegion organRegion = cellularAutomaton.Organs[0][0].regions["Estroma"]; // TODO: Find Region of Cell using Organ.FindRegionForCell(Cell cell);
            OrganRegionsData organRegionsData = cellularAutomaton.Organs[0][0].regions["Estroma"].organRegionsData; // TODO: Change OrganRegionHandler.GetOrganRegion(organ, cell).organRegionsData;


            // Check for the nearest Cells that belongs to this Set and add some randommess to the order of the update
            // search every of these Cells and update their state. After this continue
            // Cell[] nearestCells;
            // if((nearestCells = CellChunksManager.GetNearestAsyncCells(cell, asyncCellsInfo, asyncCellsStage)) != null){
            //     // Update the state of the cell
            //     return;
            // }

            // If the Cell needs some other Cells to work well, call it to the CellChunksManager
            // Request chunks and other necessary metadata
            CellChunksManager.GetChunk(cell);

            // All the time we need to get access to the nearests chunks because of the borders
            CellChunksManager.GetChunks(cell, false, true);

            // If we need to get the adjacent list of the cell
            CellChunksManager.GetAdjacentListChunks(cell);

            // First, get some info about the Cell and neighbours
            NeighbourhoodInfo neighbourhoodInfo = CellChunksManager.GetNeighbourhoodInfo(cell, organRegion, organRegionsData);

            // Then Update the Cell State making a transition using a probability and params
            SimulationParams simulationParams = new(); // Simulation Params que intervienen en este proceso, 
                                                        // se busca cuales son desde algun sitio o se obtiene alguna forma de saberlo
                                                        // 
            // IProbabilities probabilities = new(); // Lo mismo que simulation params. Para buscar ambos lo mas probable es que se
                                                    // necesiten utilizar datos como el estado actual de la celda y otras cosas
            
            // Get StatesChunksInfo
            ChunksStates chunksStates = ChunksStatesManager.GetChunksStates(cell);
            StateInfo cellState = ChunksStatesManager.GetCellState(cell);

            // Call Transition.MakeTransition(...)
            TransitionManager.StartTransitionProccess(
                cell,
                cellState,
                simulationParams,
                neighbourhoodInfo,
                chunksStates
            );
        }

        
        public static void UpdateAsyncCells(AsyncCellsInfo asyncCellsInfo, AsyncCellsStages asyncCellsStages, CellularAutomaton cellularAutomaton){
            // Stages needs to be sorted
            // Sort Cell Stages in desired order+

            // Update Cells at each ordered stage
            foreach(AsyncCellsStage asyncCellsStage in asyncCellsStages.asyncCellsStages){
                UpdateAsyncCellsAtStage(asyncCellsStage, asyncCellsInfo, cellularAutomaton);
            }
            
            // Search for each Cell in CellChunk.Cells
            // foreach(Cell cell in cellChunk.cells){
            //     // Check if the cell belongs to the AsyncSet, if not, continue
            //     if(!GetSet(cell).Equals("AsyncSet")) continue;

                
            // }
        }
        public static bool UpdateSyncCellsAtStage(StateInfo state, SyncCellsInfo syncCellsInfo, CellularAutomaton cellularAutomaton){
            // Select a random cell from the AsyncSet from the AsyncCellsInfo
            Cell cell = syncCellsInfo.GetRandomCellAndRemove(state);

            // If not Cell return
            if(cell == default(Cell))
                return false;
            
            // Get OrganRegionsData to analyse neighboorhoodInfo
            OrganRegion organRegion = cellularAutomaton.Organs[0][0].regions["Estroma"]; // TODO: Find Region of Cell using Organ.FindRegionForCell(Cell cell);
            OrganRegionsData organRegionsData = cellularAutomaton.Organs[0][0].regions["Estroma"].organRegionsData; // TODO: Change OrganRegionHandler.GetOrganRegion(organ, cell).organRegionsData;

            // Check for the nearest Cells that belongs to this Set and add some randommess to the order of the update
            // search every of these Cells and update their state. After this continue
            // Cell[] nearestCells;
            // if((nearestCells = CellChunksManager.GetNearestAsyncCells(cell, asyncCellsInfo, asyncCellsStage)) != null){
            //     // Update the state of the cell
            //     return;
            // }

            // If the Cell needs some other Cells to work well, call it to the CellChunksManager
            // Request chunks and other necessary metadata
            CellChunksManager.GetChunk(cell);

            // All the time we need to get access to the nearests chunks because of the borders
            CellChunksManager.GetChunks(cell, false, true);

            // If we need to get the adjacent list of the cell
            CellChunksManager.GetAdjacentListChunks(cell);

            // First, get some info about the Cell and neighbours
            NeighbourhoodInfo neighbourhoodInfo = CellChunksManager.GetNeighbourhoodInfo(cell, organRegion, organRegionsData);

            // Then Update the Cell State making a transition using a probability and params
            SimulationParams simulationParams = new(); // Simulation Params que intervienen en este proceso, 
                                                        // se busca cuales son desde algun sitio o se obtiene alguna forma de saberlo
                                                        // 
            // IProbabilities probabilities = new(); // Lo mismo que simulation params. Para buscar ambos lo mas probable es que se
                                                    // necesiten utilizar datos como el estado actual de la celda y otras cosas
            
            // Get StatesChunksInfo
            ChunksStates chunksStates = ChunksStatesManager.GetChunksStates(cell);
            StateInfo cellState = ChunksStatesManager.GetCellState(cell);

            // Call Transition.MakeTransition(...)
            Cell modifiedCell = TransitionManager.StartTransitionProccess(
                cell,
                cellState,
                simulationParams,
                neighbourhoodInfo,
                chunksStates
            );

            if(modifiedCell == default(Cell))
                return true;

            // Modify Cell on matrix
            organRegion.UpdateCell(modifiedCell);
            Console.WriteLine("Update 1 cell: " + modifiedCell.ToString() + "\nCell on Region: "+ organRegion.SearchCell(modifiedCell).ToString());

            return true;
        }
        public static void UpdateSyncCells(CellChunk cellChunk){
            // Search for each Cell in CellChunk.Cells
            foreach(Cell cell in cellChunk.cells){
                // Check if the cell belongs to the SyncSet, otherwise continue
                if(!GetSet(cell).Equals("SyncSet")) continue;

            // If the Cell needs some other Cells to work well, call it to the CellChunksManager
                // Request chunks and other necessary metadata
                CellChunksManager.GetChunk(cell);

                // All the time we need to get access to the nearests chunks because of the borders
                CellChunksManager.GetChunks(cell, false, true);

                // If we need to get the adjacent list of the cell
                CellChunksManager.GetAdjacentListChunks(cell);

                // Make State Transitions
            }
        }

        public static void UpdateSyncCells(SyncCellsInfo syncCellsInfo, SyncCellsStage syncCellsStages, CellularAutomaton cellularAutomaton){
            // Stages needs to be sorted
            // Sort Cell Stages in desired order+

            // Update Cells at each ordered stage
            foreach(StateInfo state in syncCellsStages.statesInfo){
                while(UpdateSyncCellsAtStage(state, syncCellsInfo, cellularAutomaton))
                    continue;
            }
            // Search for each Cell in CellChunk.Cells
            // foreach(Cell cell in cellChunk.cells){
            //     // Check if the cell belongs to the AsyncSet, if not, continue
            //     if(!GetSet(cell).Equals("AsyncSet")) continue;

                
            // }
        }

        public static void UpdateCells(CellChunk cellChunk){
            // Update AsyncCells first calling UpdateAsyncCells and then UpdateSyncCells
            // UpdateAsyncCells(cellChunk);
            
            // Update SyncCells
            UpdateSyncCells(cellChunk);
            
        }

        public static void UpdateAutomatonCells(CellularAutomaton cellularAutomaton){
            HashSet<Cell> cellsSet= new();

            AsyncCellsInfo asyncCellsInfo = new(cellsSet);
            
            foreach(CellChunk cellChunk in CellChunksManager.GetAutomatonChunks(cellularAutomaton)){
                cellsSet.Concat(ExtractAsyncCells(cellChunk));
                if(AsyncCellsHandler.NeedsToCleanBuffer(asyncCellsInfo)){
                    asyncCellsInfo.CleanBuffer();
                }
            }

            AsyncCellsStages asyncCellsStages = new();
            // Update Async Cells
            UpdateAsyncCells(asyncCellsInfo, asyncCellsStages, cellularAutomaton);


            // Sync Cells Metadata
            SyncCellsStage syncCellsStage = new SyncCellsStage{statesInfo = GeneralSettings.StatesSettings.statesInfo.Values};
            HashSet<Cell> syncCellsSet = ExtractSyncCells(cellularAutomaton.Organs[0][0].regions["Estroma"]);//new();
            SyncCellsInfo syncCellsInfo = new SyncCellsInfo(syncCellsSet);

            // Update Sync Cells
            // TODO: Get Automaton Chunks
            // foreach(CellChunk cellChunk in CellChunksManager.GetAutomatonChunks(cellularAutomaton)){
            //     UpdateSyncCells(syncCellsInfo, syncCellsStage, cellularAutomaton);
            // }
            UpdateSyncCells(syncCellsInfo, syncCellsStage, cellularAutomaton);

        }
        public static void NextStep(CellularAutomaton cellularAutomaton, int step=0){
            // Update the cells
            UpdateAutomatonCells(cellularAutomaton);
            
            // Update the organs
                // Add new Connections between Cell if it's necessary
            
            // Save the cells
            SaveStep(cellularAutomaton, step);

            // Update Iteration params
        }

        // Steps of Simulation
        public static void SaveStep(CellularAutomaton cellularAutomaton, int step=0){
            string filePath = Path.Combine(GeneralSettings.simulationStepsPath, "step_"+step.ToString()+".json");
            MatrixFunctions.SaveCellsPositionsToJson(cellularAutomaton.Organs[0][0].regions["Estroma"].Cells, true, filePath);
        }
    }

    // Async Cells
    public class AsyncCellsStage{

    }
    public class AsyncCellsStages{
        public IEnumerable<AsyncCellsStage> asyncCellsStages = new List<AsyncCellsStage>();
    }
    public class AsyncCellsInfo{
        public int cellsCount;
        public string cellsInfoFilePath;
        public IEnumerable<Cell> cells;

        public AsyncCellsInfo(IEnumerable<Cell> cells){
            this.cells = cells;
            this.cellsCount = cells.Count();
            this.cellsInfoFilePath = "AsyncCellsInfo.json";
        }

        public AsyncCellsInfo(int cellsCount, string cellsInfoFilePath){
            this.cellsCount = cellsCount;
            this.cellsInfoFilePath = cellsInfoFilePath;
        }

        public void SaveAsyncCellsInfo(){
            // Save the cells info in a json file
            // return 0;
        }
        public int UpdateCount(){
            // Update the count of the cells
            return 0;
        }
        public void CleanBuffer(){
            // Clean the buffer of the cells
            UpdateCount();
            SaveAsyncCellsInfo();
        }

        public Cell GetRandomCell(AsyncCellsStage asyncCellsStage)
        {
            throw new NotImplementedException();
        }

        public Cell GetRandomCellAndRemove(AsyncCellsStage asyncCellsStage){
            throw new NotImplementedException();
        }
    }
    public static class AsyncCellsHandler{
        public static IEnumerable<Cell> GetCells(AsyncCellsInfo asyncCellsInfo){
            // Get the cells from the file
            return null;
        }

        public static bool NeedsToCleanBuffer(AsyncCellsInfo asyncCellsInfo){
            // Check if the buffer needs to be cleaned
            return false;
        }
    }

    // Sync Cells
    public class SyncCellsStage{
        public IEnumerable<StateInfo> statesInfo{get; set;}
    }
    public class SyncCellsInfo{
        public int cellsCount;
        public string cellsInfoFilePath;
        public HashSet<Cell> cells;

        public SyncCellsInfo(HashSet<Cell> cells){
            this.cells = cells;
            this.cellsCount = cells.Count();
            this.cellsInfoFilePath = "SyncCellsInfo.json";
        }
        // public SyncCellsInfo(IEnumerable<Cell> cells){
        //     this.cells = cells;
        //     this.cellsCount = cells.Count();
        //     this.cellsInfoFilePath = "SyncCellsInfo.json";
        // }

        public SyncCellsInfo(int cellsCount, string cellsInfoFilePath){
            this.cellsCount = cellsCount;
            this.cellsInfoFilePath = cellsInfoFilePath;
        }

        public void SaveAsyncCellsInfo(){
            // Save the cells info in a json file
            // return 0;
        }
        public int UpdateCount(){
            // Update the count of the cells
            return 0;
        }
        public void CleanBuffer(){
            // Clean the buffer of the cells
            UpdateCount();
            SaveAsyncCellsInfo();
        }

        public Cell GetRandomCell(SyncCellsStage SyncCellsStage)
        {
            throw new NotImplementedException();
        }

        public Cell GetRandomCellAndRemove(StateInfo state){
            Cell selectedCell = default(Cell);
            Random rand = new Random();
            int randomNumber = rand.Next(0, this.cells.Count);
            foreach(Cell cell in this.cells){
                if(cell.state == state.id && randomNumber < 50){
                    selectedCell = cell;
                    break;
                }
                randomNumber--;
            }
            cells.Remove(selectedCell);
            return selectedCell;
        }
    }

}