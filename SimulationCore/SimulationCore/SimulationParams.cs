namespace ConsoleApp1{
// Params of the Simulation
    // Manejador estatico de parametros de la simulacion
    // Encargado de cargar y contener los parametros que intervienen en la simulacion
    public static class SimulationParamsHandler{
        // Extraer ciertos parametros de simulacion que intervienen en ciertas tareas
        // Muchas veces no necesitamos quedarnos con todos los parametros
        public static SimulationParams GetSimulationParams(){
            throw new NotImplementedException();
        }

        // Utilizado por lo general para pruebas, Parametros de simulacion que deben de ser utiles
        // para resolver tareas comunes
        public static SimulationParams defaultSimulationParams;
    }
    // Parametros de la simulacion que tienen relacion con las probabilidades de que ocurra cierto evento.
    // Ex: Dados ciertos parametros con tales valores, estos influyen en algun proceso dando como resultado
    // una probabilidad p. Se deben de crear las formulas de estas probabilidades con una funcion lambda o delegado
    // Luego una Transicion sera el resultado del empleo de estas probabilidades a partir de un estado y cierta informacion
    // sobre los alrededores de la Celula que se pase
    // -- Se pueden simular ciertos procesos siguiendo esta idea, el primer proceso sera el de Transicion. Pero tambien se puede
    // simular el proceso de que una celula tumoral llegue a un vaso sanguinea
    public class SimulationParams{

    }
}