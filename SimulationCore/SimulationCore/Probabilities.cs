namespace ConsoleApp1
{// Probabilities
    public static class ProbabilitiesManager
    {
        // ProbabilitiesMetaData probabilitiesMetaData;   
        public static IProbabilities GetProbabilitiesForTransition(Transition transition)
        {
            return ProbabilitiesMetaData.TransitionProbabilities.FindTransitionProbabilitie(transition);
        }
        // public static SimulationParams
    }
    public static partial class ProbabilitiesMetaData { }
    public interface IProbabilities
    {
        // Valor minimo de tolerancia para asegurar que se pueda tener en cuenta cierta probabilidad de algun suceso.

        float TOL { get; set; }
        float probabilityValue{get; set;}

        // Calcular la probabilidad de que ocurra algun suceso
        public float CalculateProbability(Transition transition, SimulationParams simulationParams, NeighbourhoodInfo neighbourhoodInfo);
    }

    public struct TransitionProbability : IProbabilities
    {
        Func<Transition, float> probabilityFunction;

        public float TOL { get => 0.8f; set => throw new NotImplementedException(); }
        public float probabilityValue{get;set;}

        public TransitionProbability(float probabilityValue=0f)
        {
            this.probabilityValue = probabilityValue;
            this.probabilityFunction = null;
        }
        public float CalculateProbability(Transition transition, SimulationParams simulationParams, NeighbourhoodInfo neighbourhoodInfo)
        {
            if (transition.toState == 3)
            {
                if (neighbourhoodInfo.cancerCells.Count == 1)
                {
                    float f = (float)new Random().NextDouble();
                    this.probabilityValue = f;
                    return f;
                }
                else if (neighbourhoodInfo.cancerCells.Count >= 2)
                {
                    Random rand = new Random();
                    double min = 0.1; // valor mínimo del rango
                    double max = 1; // valor máximo del rango
                    double range = max - min;
                    double sample = rand.NextDouble();
                    double scaled = (sample * range) + min;
                    float f = (float)scaled;
                    // Console.WriteLine(f);
                    this.probabilityValue = f;
                    return f;
                }
            }
            return 0;
        }
    }
}