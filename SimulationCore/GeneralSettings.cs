using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ConsoleApp1{
    // Contiene todas las configuraciones de todo el algoritmo de simulacion. Asi como la parte de las diferentes estructuras
    // utilizadas a lo largo de la ejecucion de todo el programa
    public static class GeneralSettings{
        // static string rootFilePath = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory()), "test", "test.txt");
        static string rootFilePath = Path.GetFullPath(Directory.GetCurrentDirectory());
        public static string configFilePath = Path.Combine(rootFilePath, "config");
        public static string statesPath = Path.Combine(configFilePath, "states", "states.json"); // States
        static string transitionsPath = Path.Combine(configFilePath, "transitions", "transitions.json"); // Transitions
        static string probabilitiesPath = Path.Combine(configFilePath, "probabilites", "probabilites.json"); // Probabilites

        // Cells
        static string cellsPath = Path.Combine(configFilePath, "cells", "states.json");

        // Simulation
        public static string simulationStepsPath = Path.Combine(rootFilePath, "simulation", "steps");

        // Configuraciones de las celulas
        // Contiene los distintos tipos de Celulas que intervienen en la simulacion que se lleve a cabo
        public static Dictionary<string, int> CellsSettings = new();

        // Configuraciones de las regiones de los organos
        public static Dictionary<string, int> OrgansSettings = new();
            // Configuraciones de los poligonos de los organos
        public static Dictionary<string, Func<Polygon>> OrgansPolygonsSettings = new();

        // Configuraciones de los estados
        // Cada estado debe de tener informacion sobre que tipo de Celula(CellType) sera y en que region(OrganRegion) se encontrara
        // public static Dictionary<string, object> statesSettingsDict;
        public static StatesConfig StatesSettings;// = new StatesConfig();

        // TODO: Lo ideal seria tener como llaves una tupla de FromState y ToState, pero luego se vera todo eso...
        public static Dictionary<long, Transition> TransitionsSettings;

        public static Dictionary<int, IProbabilities> ProbabilitesSettings;

        //
        public static Dictionary<string, object> LoadStatesSettingsFromJson(string jsonPath){
            return JsonHandler.LoadStatesSettings(jsonPath);
            // return statesSettingsDict;
        }

        // Carga las configuraciones de los estados desde un archivo json
        public static void LoadStatesSettings(string jsonPath=""){
            // StatesSettings
            if(jsonPath == String.Empty)
                jsonPath = statesPath;

            StatesSettings = new StatesConfig(null);
            Dictionary<string, object> statesSettingsDict = LoadStatesSettingsFromJson(statesPath);
            // }

            // var temp = statesSettingsDict["states"];
            JArray statesSettings = (JArray)statesSettingsDict["states"];
            foreach (var stateSetting in statesSettings)
            {
                System.Console.WriteLine(stateSetting);
                Dictionary<string, object> tempStateSetting = JsonConvert.DeserializeObject<Dictionary<string, object>>(stateSetting.ToString());
                System.Console.WriteLine(tempStateSetting["id"]);
                System.Console.WriteLine(StatesSettings);
                System.Console.WriteLine(StatesSettings.statesInfo);
                StatesSettings.statesInfo.Add((long)(tempStateSetting["id"]), 
                                                    new StateInfo{
                                                        id = (long)tempStateSetting["id"],
                                                        name = (string)tempStateSetting["name"], 
                                                        cellType = (string)tempStateSetting["cellType"], 
                                                        organRegion = (string)tempStateSetting["organRegion"]
                                                        });
            }
        }

        // Configuraciones de las regiones de los organos
            // Configuraciones de los poligonos de los organos
        public static void LoadOrgansPolygonsSettings(){
            OrgansPolygonsSettings = new Dictionary<string, Func<Polygon>>();
            OrgansPolygonsSettings.Add("Cube", () => new CubePolygon());
        }

        // Hacer CheckPoint del estado de todas las celulas
        public static void CellsCheckpoint (Cell[,,] cells, string pathName=""){
            if(pathName != ""){
                // Do something...
            }
            CellSerializer.SaveCellsToJson(cells, cellsPath);
        }

        // Configuraciones de las transiciones
        // Carga las configuraciones de las transiciones desde un archivo json
        public static void LoadTransitionsSettings(string jsonPath=""){
            // StatesSettings
            if(jsonPath == String.Empty)
                jsonPath = transitionsPath;

            TransitionsSettings = new();
            Dictionary<string, object> transitionsSettingsDict = LoadStatesSettingsFromJson(jsonPath); //new StatesConfig(null);
            // }

            // var temp = statesSettingsDict["states"];
            JArray transitionsSettings = (JArray)transitionsSettingsDict["transitions"];
            foreach (var transitionSetting in transitionsSettings)
            {
                System.Console.WriteLine(transitionSetting);
                Dictionary<string, object> tempTransitionSetting = JsonConvert.DeserializeObject<Dictionary<string, object>>(transitionSetting.ToString());
                System.Console.WriteLine(tempTransitionSetting["id"]);
                // System.Console.WriteLine(StatesSettings);
                // System.Console.WriteLine(StatesSettings.statesInfo);
                TransitionsSettings.Add((long)(tempTransitionSetting["id"]), 
                                                    new Transition{
                                                        fromState = (long)tempTransitionSetting["fromState"], 
                                                        toState = (long)tempTransitionSetting["toState"]
                                                        });
            }
        }
        // Configuraciones de las probabilidades para cada transicion
        // Configuraciones 
    }
}