namespace ConsoleApp1
{
    // TramsitionManager
    public static class TransitionManager
    {
        public static (bool, float, IProbabilities, Transition) CanMakeTransition(Cell cell, StateInfo cellState, SimulationParams simulationParams/*, Probabilities probabilities*/, Transition transition, NeighbourhoodInfo neighbourhoodInfo)
        {
            // Get Probabilities for this Transition
            IProbabilities probabilities = ProbabilitiesManager.GetProbabilitiesForTransition(transition);

            // Extract Relevant Param for prababilities to calculate
            // Skip this step, do it only if needed

            // Calculate Probabilitie with SimulationParams
            float probability = probabilities.CalculateProbability(transition, simulationParams, neighbourhoodInfo);
            return (probability >= probabilities.TOL, probability, probabilities, transition);
        }

        public static IEnumerable<Transition> GetPossibleTransitions(Cell cell, StateInfo cellState)
        {
            IEnumerable<Transition> transitions = GeneralSettings.TransitionsSettings.Values;

            foreach (Transition transition in transitions)
                if (transition.fromState == cellState.id)
                    yield return transition;
        }

        public static List<(bool, float, IProbabilities, Transition)> CalculateProbabilities(IEnumerable<Transition> transitions, Cell cell, StateInfo cellState, SimulationParams simulationParams, NeighbourhoodInfo neighbourhoodInfo)
        {
            List<(bool, float, IProbabilities, Transition)> result = new();
            foreach (Transition transition in transitions)
                result.Add(CanMakeTransition(cell, cellState, simulationParams, transition, neighbourhoodInfo));
            return result;
        }

        public static Cell StartTransitionProccess(Cell cell, StateInfo cellState, SimulationParams simulationParams, NeighbourhoodInfo neighbourhoodInfo, ChunksStates chunkStates)
        {
            // Get Possible Transitions
            IEnumerable<Transition> transitions = GetPossibleTransitions(cell, cellState);

            // Calculate Probabilities
            List<(bool, float, IProbabilities, Transition)> probabilities = CalculateProbabilities(transitions, cell, cellState, simulationParams, neighbourhoodInfo);

            // If there is not any prob that TOLerate its value return, this Cell cannot make a Transition

            // Get The Maximum Probability
            float maximum = 0;
            int probabilityIndex = 0;
            Transition actualTransition = default(Transition);
            IProbabilities actualProbability = null;
            int index = 0;
            foreach (var probability in probabilities)
            {
                if (maximum < probability.Item2)
                {
                    maximum = Math.Max(maximum, probability.Item2);
                    probabilityIndex = index;
                    actualProbability = probability.Item3;
                    actualTransition = probability.Item4;
                }
                index++;
            }

            // Make Transition if Maxmim Prob can TOLerate that
            if(actualProbability == null || (actualProbability != null && actualProbability.probabilityValue < actualProbability.TOL))
                return default(Cell);
            // MakeTransition();
            return MakeTransition(cell, cellState, simulationParams, actualProbability, actualTransition);
        }

        public static Cell MakeTransition(Cell cell, StateInfo cellState, SimulationParams simulationParams, IProbabilities probabilities, Transition transition)
        {
            cell.state = (int)transition.toState;
            return cell;
        }
    }
    // Transition
    public struct Transition
    {
        public long fromState;
        public long toState;
    }
}