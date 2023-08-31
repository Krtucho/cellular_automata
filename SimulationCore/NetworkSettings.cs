
namespace ConsoleApp1
{
    public class NetworkPropertiesData
    {
        public double AveragePathLength { get; set; }
        public double ClusteringCoefficient { get; set; }

        public NetworkPropertiesData()
        {
            AveragePathLength = 0;
            ClusteringCoefficient = 0;
        }
    }

    public class NetworkSettings
    {
        public static int _networkSizeX{get; set;}
        public static int _networkSizeY{get; set;}
        public static int _networkSizeZ{get; set;}
        public int NetworkSizeX { get; private set; }
        public int NetworkSizeY { get; private set; }
        public int NetworkSizeZ { get; private set; }
        public int NetworkDivision { get; set; }
        public double ReconnectionProbability { get; private set; }
        public double NeighbourhoodRadius { get; private set; }
        public bool IsNetworkTest { get; private set; }
        public bool HasPeriodicEdges { get; private set; }

        public NetworkSettings(int sizex=10, int sizey=10, int sizez=10, int div=0, double p=0, double r=0, bool test=false, bool periodic=false)
        {
            NetworkSizeX = sizex;
            NetworkSizeY = sizey;
            NetworkSizeZ = sizez;
            NetworkDivision = div;

            if(p <= 0)
                ReconnectionProbability = 1 * Math.Pow(10, -2);
            else
                ReconnectionProbability = p;

            if(r <= 0)
                NeighbourhoodRadius = Math.Sqrt(2);
            else
                NeighbourhoodRadius = r;

            IsNetworkTest = test;
            HasPeriodicEdges = periodic;
        }
        // public static void BuildNetwork()
        // {
        //     Console.WriteLine("main: creating network...");
        //     int time_start = Environment.TickCount;
        //     NetworkSettings _networkSettings = new NetworkSettings(_gridSizeX, _gridSizeY, _gridDivision, _p, _r, false, _periodic);
        //     _wattsNetwork = new SmallWorldNetwork(_networkSettings);
        //     int time_elapsedmiliseconds = Environment.TickCount - time_start;
        //     string formatted_time = Notification.TimeStamp(time_elapsedmiliseconds);
        //     Console.WriteLine("main: finished creating network" + formatted_time);
        // }
        public static NetworkSettings BuildRandomNetworkSettings(){

            return new NetworkSettings();
        }

        public static (int, int) LoadStaticParams(){
            return (_networkSizeX, _networkSizeY); 
        }
    }

    public class NetworkSettingsManager{}
}