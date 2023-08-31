namespace ConsoleApp1{
    public static class Simulation{
        // Settings Variables
        // States Settings json file path
        static string statesFilePath;

        // Create Dictionaries, load every info from json files
        public static void LoadSimulationParms(){
            GeneralSettings.LoadStatesSettings(statesFilePath); // Load States Settings
            GeneralSettings.LoadTransitionsSettings(); // Load TransitionSettings
            GeneralSettings.LoadOrgansPolygonsSettings(); // Load Organs Polygons Settings
        }

        public static CellularAutomaton CreateTumor(CellularAutomaton cellularAutomaton, TumorConfig tumorConfig){
            // OrganHandler.AddTumor(cellularAutomaton, tumorConfig);
            OrganHandler.AddTumorOnEstroma(cellularAutomaton, tumorConfig);
            return cellularAutomaton;
        }

        public static CellularAutomaton CreateSimulation(){

            LoadSimulationParms();

            CellularAutomaton cellularAutomaton = new();

            // Make Network Settings
            cellularAutomaton.networkSettings = NetworkSettings.BuildRandomNetworkSettings();

            // Small World Network
            cellularAutomaton.smallWorldNetwork = new SmallWorldNetwork(cellularAutomaton);

            CreateTumor(cellularAutomaton, new TumorConfig(1, 1, 1));

            //GeneralSettings.CellsCheckpoint(cellularAutomaton.Organs[0][0].regions["Estroma"].Cells);

            return cellularAutomaton;
        }

        public static void Simulate(CellularAutomaton cellularAutomaton, int iterations=1){
            for(int i = 0; i< iterations; i++){
                CellularAutomatonHandler.NextStep(cellularAutomaton, i);
            }
        }
        public static void Simulate(CellChunk cellChunk){

        }
        public static void Simulate(CellChunk cellChunk, int iterations){

        }
        public static void Simulate(CellChunk cellChunk, int iterations, int time){

        }
        public static void Simulate(CellChunk cellChunk, int iterations, int time, int speed){

        }
    }
}