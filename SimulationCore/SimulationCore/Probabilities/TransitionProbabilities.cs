namespace ConsoleApp1
{// Probabilities
    public static partial class ProbabilitiesMetaData{
        //public static TransitionProbabilities transitionProbabilities;
        public static class TransitionProbabilities{
            // Pasamos Transition, ya que este llevara ambos estados, el inicial y el final
            // public static Dictionary<Transition, TransitionProbabilities> transitionProbabilities = new();
            // Create an instance of TransitionProbabilitiesData in the static class TransitionProbabilities
            private static TransitionProbabilitiesData transitionProbabilitiesData = new TransitionProbabilitiesData();
            
            private static TransitionProbability transitionProbability = new TransitionProbability();

            // Load TransitionProbabilities from a json file
            public static void LoadTransitionProbabilities(){}
            public static IProbabilities FindTransitionProbabilitie(Transition transition){
                try{
                    // foreach(IPro transitionProbability in TransitionProbabilities.transitionProbabilitiesData.transitionProbabilities.Values){
                    //     foreach(var transitionFunction in transitionProbability)
                    // }
                    return transitionProbability; //(IProbabilities)transitionProbabilitiesData.transitionProbabilities[transition];
                }catch{
                    // continue;
                    return default(IProbabilities);
                }
            }

        }
    }
    //This class holds the transition probability data
    public class TransitionProbabilitiesData  
    {
        public Dictionary<int, IProbabilities> transitionProbabilities;// = new();
        public TransitionProbabilitiesData(){
            this.transitionProbabilities = new();
            // this.transitionProbabilities.Add(0, TransitionProbabilities);
            
        }
    }
}


// public static partial class ProbabilitiesMetaData
// {
//     //public static TransitionProbabilities transitionProbabilities;
//     public static class TransitionProbabilities
//     {
//         // Create an instance of TransitionProbabilitiesData in the static class TransitionProbabilities
//         private static TransitionProbabilitiesData transitionProbabilitiesData = new TransitionProbabilitiesData();

//         // Load TransitionProbabilities from a json file
//         public static void LoadTransitionProbabilities(){}

//         public static IProbabilities FindTransitionProbabilitie(Transition transition)
//         {
//             try
//             {
//                 return (IProbabilities)transitionProbabilitiesData.transitionProbabilities[transition];
//             }
//             catch
//             {
//                 // continue;
//                 return default(IProbabilities);
//             }
//         }
//     }
// }