using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
// using QuickGraph;
// using TEdge = QuickGraph.UndirectedEdge<string>;
using TVertex = System.String;
using TEdgeString = System.String;

namespace ConsoleApp1
{
    public class SmallWorldNetwork:Network
    {
        // private UndirectedGraph<TVertex, TEdge> _g = null;
        private Random _random = null;
        private NetworkSettings _networkSettings = null;
        private NetworkPropertiesData _networkProperties = null;
        private int _reconnectedEdgesAmount = 0;

        // public List<TVertex> Vertices { get { return _g.Vertices.ToList(); } }
        // public List<TEdge> Edges { get { return _g.Edges.ToList(); } }
        public double AveragePathLength { get { return _networkProperties.AveragePathLength; } }
        public double ClusteringCoefficient { get { return _networkProperties.ClusteringCoefficient; } }
        public int NetworkSizeX { get { return _networkSettings.NetworkSizeX; } }
        public int NetworkSizeY { get { return _networkSettings.NetworkSizeY; } }       
        public int NetworkDivision { get { return _networkSettings.NetworkDivision; } } 
        public double NeighbourhoodRadius { get { return _networkSettings.NeighbourhoodRadius; } }

        public SmallWorldNetwork(CellularAutomaton cellularAutomaton/*, NetworkSettings network*/)
        {
            // _g = new UndirectedGraph<TVertex, TEdge>();
            Organ organ = new Organ(cellularAutomaton.networkSettings);
            cellularAutomaton.Organs.Add(0, new List<Organ>{organ});
            
            _random = new Random();
            _networkProperties = new NetworkPropertiesData();
            _networkSettings = cellularAutomaton.networkSettings;

            int timeStart = Environment.TickCount;
            Console.WriteLine("   smallworldnetwork library: adding vertices...");
            FillVertices(organ);
            int elapsedMiliseconds = Environment.TickCount - timeStart;
            // string formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            // Console.WriteLine("   smallworldnetwork library: finished adding vertices" + formattedTime);
            // Console.WriteLine("   smallworldnetwork library: added " + _g.VertexCount + " vertices");

            timeStart = Environment.TickCount;
            Console.WriteLine("   smallworldnetwork library: adding regular edges...");
            FillRegularEdges(organ);
            elapsedMiliseconds = Environment.TickCount - timeStart;
            // formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            // Console.WriteLine("   smallworldnetwork library: finished adding regular edges" + formattedTime);
            // Console.WriteLine("   smallworldnetwork library: added " + _g.EdgeCount + " regular edges");
            if (_networkSettings.ReconnectionProbability != 0)
            {
                timeStart = Environment.TickCount;
                Console.WriteLine("   smallworldnetwork library: rewiring edges...");
                RewireEdges(organ, _networkSettings.ReconnectionProbability);
                elapsedMiliseconds = Environment.TickCount - timeStart;
                // formattedTime = Notification.TimeStamp(elapsedMiliseconds);
                // Console.WriteLine("   smallworldnetwork library: finished rewiring edges" + formattedTime);
                // Console.WriteLine("   smallworldnetwork library: added " + _reconnectedEdgesAmount + " rewired edges");
            }
            // if (_networkSettings.IsNetworkTest == true)
            // {
            //     timeStart = Environment.TickCount;
            //     Console.WriteLine("   smallworldnetwork library: determining network properties...");
            //     _networkProperties.AveragePathLength = GetAveragePathLength();
            //     _networkProperties.ClusteringCoefficient = GetClusteringCoefficient();
            //     elapsedMiliseconds = Environment.TickCount - timeStart;
            //     // formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            //     // Console.WriteLine("   smallworldnetwork library: finished determining the properties" + formattedTime);
            // }
        }
        public SmallWorldNetwork(NetworkSettings network, string networkpath)
        {
            // _g = new UndirectedGraph<TVertex, TEdge>();
            _random = new Random();
            _networkProperties = new NetworkPropertiesData();
            _networkSettings = network;

            int timeStart = Environment.TickCount;
            Console.WriteLine("   smallworldnetwork library: loading vertices...");
            LoadVertices(networkpath);
            int elapsedMiliseconds = Environment.TickCount - timeStart;
            // string formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            // Console.WriteLine("   smallworldnetwork library: finished loading vertices" + formattedTime);
            // Console.WriteLine("   smallworldnetwork library: added " + _g.VertexCount + " vertices");

            timeStart = Environment.TickCount;
            Console.WriteLine("   smallworldnetwork library: loading edges...");
            // LoadEdges(networkpath);
            elapsedMiliseconds = Environment.TickCount - timeStart;
            // formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            // Console.WriteLine("   smallworldnetwork library: finished loading edges" + formattedTime);
            // Console.WriteLine("   smallworldnetwork library: added " + _g.EdgeCount + " edges");

            // if (_networkSettings.IsNetworkTest == true)
            // {
            //     timeStart = Environment.TickCount;
            //     Console.WriteLine("   smallworldnetwork library: determining network properties...");
            //     _networkProperties.AveragePathLength = GetAveragePathLength();
            //     _networkProperties.ClusteringCoefficient = GetClusteringCoefficient();
            //     elapsedMiliseconds = Environment.TickCount - timeStart;
            //     // formattedTime = Notification.TimeStamp(elapsedMiliseconds);
            //     // Console.WriteLine("   smallworldnetwork library: finished determining the properties" + formattedTime);
            // }
        }
        public void Dispose()
        {
            _networkSettings = null;
            _networkProperties = null;
            _random = null;
            // _g.Clear();
        }

        private void LoadVertices(string networkpath)
        {
            string[] textbody = File.ReadAllLines(networkpath);
            int textbodyLength = textbody.Length;
            bool mark = false;
            bool written = false;
            for (int i = 0; i < textbodyLength; i++)
            {
                // Notification.CompletionNotification(i, textbody.Length, ref written, "      ");
                if (mark && textbody[i] != string.Empty && textbody[i] != "[Edges]")
                    // _g.AddVertex(textbody[i]);
                if (textbody[i] == "[Vertexs]")
                    mark = true;
                if (textbody[i] == "[Edges]")
                    break;
            }
            // Notification.FinalCompletionNotification("      ");
        }
        // private void LoadEdges(string networkpath)
        // {
        //     string[] textbody = File.ReadAllLines(networkpath);
        //     int textbodyLength = textbody.Length;
        //     bool mark = false;
        //     bool written = false;
        //     for (int i = 0; i < textbodyLength; i++)
        //     {
        //         // Notification.CompletionNotification(i, textbody.Length, ref written, "      ");
        //         if (mark && textbody[i] != string.Empty && textbody[i] != "[EOF]")
        //         {
        //             var vertexs = MF.GetVertexsFromEdge(textbody[i]);
        //             _g.AddEdge(new TEdge(vertexs[0], vertexs[1]));
        //         }
        //         if (textbody[i] == "[Edges]")
        //             mark = true;
        //         if (textbody[i] == "[EOF]")
        //             break;
        //     }
        //     // Notification.FinalCompletionNotification("      ");
        // }

        private void FillVertices(Organ organ)
        {
            bool written = false;
            int inf = 0;
            int sup = _networkSettings.NetworkSizeX * _networkSettings.NetworkSizeY;
            for (int i = 0; i < _networkSettings.NetworkSizeX; i++)
                for (int j = 0; j < _networkSettings.NetworkSizeY; j++)
                    for (int k = 0; k < _networkSettings.NetworkSizeZ; k++)
                    {
                        inf++;
                        organ.AddCell(new Cell{x=i, y=j, z=k, state=0});//_g.AddVertex(MF.BuildTVertexID(i, j));
                    }
                // {
                    // Notification.CompletionNotification(inf, sup, ref written, "      ");
                // }
            // Notification.FinalCompletionNotification("      ");
        }
        private void FillRegularEdges(Organ organ)
        {
            List<Cell> template = MF.GetRegularNeighboursTemplate(_networkSettings.NeighbourhoodRadius);
            List<Cell> filtered = MF.FilterRegularNeighboursTemplate(template);
            // List<TVertex> vg = _g.Vertices.ToList();
            int vgCount = organ.GetCellsCount();// vg.Count;
            int filteredCount = filtered.Count;
            bool written = false;
            
            // Switch for int i... for foreach organ in organ.GetCells()
            foreach(Cell cell in organ.GetCells()){

                // Cell vertex = organ.GetCell(i);//vg[i];
                for (int j = 0; j < filteredCount; j++)
                {
                    Cell dir = filtered[j];
                    int[] dir_pos = MF.GetTVertexID(dir);
                    // TVertex wertex = string.Empty;
                    Cell wertex = default;
                    if (_networkSettings.HasPeriodicEdges)
                        wertex = MF.GetVertexCyclic(cell, dir_pos[0], dir_pos[1], dir_pos[2], _networkSettings.NetworkSizeX, _networkSettings.NetworkSizeY, _networkSettings.NetworkSizeZ);
                    else wertex = MF.GetVertexUnCyclic(cell, dir_pos[0], dir_pos[1], dir_pos[2], _networkSettings.NetworkSizeX, _networkSettings.NetworkSizeY, _networkSettings.NetworkSizeZ);
                    if (wertex.state != -1)
                    {
                        // TEdge edge = new TEdge(vertex, wertex);
                        // _g.AddEdge(edge);
                        if(cell.point == wertex.point)
                            continue;
                        organ.AddCellConnection(cell, wertex);
                    }
                }
            }
            // for (int i = 0; i < vgCount; i++)
            // {
                // Notification.CompletionNotification(i, vg.Count, ref written, "      ");
            // }
            // Notification.FinalCompletionNotification("      ");
        }
        private void RewireEdges(Organ organ, double p)
        {
            Dictionary<Connection, Cell> reconnectionsDict = new Dictionary<Connection, Cell>();
            // List<TVertex> vg = _g.Vertices.ToList();
            int vgCount = organ.GetCellsCount();//vg.Count;
            bool written = false;

            // Switch for int i... for foreach organ in organ.GetCells()
            // for (int i = 0; i < vgCount; i++)
            // {
                // Notification.CompletionNotification(i, vg.Count, ref written, "      ");
            foreach(Cell cell in organ.GetCells()){
                Cell vertex = cell;// Changed organ.GetCell(i) for cell ... organ.GetCell(i);//vg[i];
                List<Connection> edges = organ.GetCellConnections(vertex);//_g.AdjacentEdges(vertex).ToList();
                int edgesCount = edges.Count;
                for (int j = 0; j < edgesCount; j++)
                {
                    // TEdge edge = edges[j];
                    Connection conn = edges[j];
                    if (conn.Source == vertex)
                    {
                        Cell oldtarget = conn.Target;
                        int[] vpos = MF.GetTVertexID(vertex);
                        Cell newtarget;
                        Connection newedge;
                        int x = 0, y = 0, z = 0;
                        List<Cell> regularNeighbours = new List<Cell>();
                        for (int k = 0; k < edgesCount; k++)
                        {
                            if (edges[k].Source.point == vertex.point) 
                                regularNeighbours.Add(edges[k].Target);
                            else if (edges[k].Target.point == vertex.point) 
                                regularNeighbours.Add(edges[k].Source);
                            else 
                                throw new Exception("Error in neighbourhood.");
                        }
                        double random = _random.NextDouble();
                        if (random < p)
                        {
                            do
                            {
                                x = _random.Next(_networkSettings.NetworkSizeX);
                                y = _random.Next(_networkSettings.NetworkSizeY);
                                z = _random.Next(_networkSettings.NetworkSizeZ);
                                newtarget = MF.BuildTVertexID(x, y, z);
                                newedge = new Connection(vertex, newtarget);
                            }
                            while ((vpos[0] == x && vpos[1] == y && vpos[2] == z) || (regularNeighbours.Contains(newtarget)) ||
                                   (reconnectionsDict.ContainsKey(newedge))); // changed newedge.ToString() -> newedge
                            reconnectionsDict.Add(newedge, oldtarget);
                        }
                    }
                }
            }
            // }
            List<Connection> reconnections = reconnectionsDict.Keys.ToList();
            int recCounts = reconnections.Count;
            for (int i = 0; i < recCounts; i++)
            {
                Connection key = reconnections[i];
                Cell[] origin_newtarget_pair = MF.GetVertexsFromEdge(key);
                Cell origin = origin_newtarget_pair[0];
                Cell newtarget = origin_newtarget_pair[1];
                Cell oldtarget = reconnectionsDict[key];
                Connection to_remove_edge;
                bool obtained = organ.TryGetConnection(origin, oldtarget, out to_remove_edge);//_g.TryGetEdge(origin, oldtarget, out to_remove_edge);
                if (obtained) organ.DeleteCellConnection(to_remove_edge);//_g.RemoveEdge(to_remove_edge);
                else throw new Exception("Edge to be removed not found, internal problems.");
                Connection to_add_edge = new Connection{v = origin, w = newtarget};
                bool added = organ.AddCellConnection(to_add_edge.v, to_add_edge.w);//_g.AddEdge(to_add_edge);
                if (!added) throw new Exception("Edge to be add not added, internal problems.");
            }
            _reconnectedEdgesAmount = reconnections.Count;
            // Notification.FinalCompletionNotification("      ");
        }

        // private double GetAveragePathLength()
        // {
        //     double average = 0.0;
        //     List<TVertex> vg = _g.Vertices.ToList();
        //     int vgCount = vg.Count;
        //     for (int i = 0; i < vgCount; i++)
        //     {
        //         TVertex vertex = vg[i];
        //         Dictionary<TVertex, int> pathLengths = new Dictionary<TVertex, int>();
        //         for (int j = 0; j < vgCount; j++)
        //         {
        //             TVertex v = vg[j];
        //             pathLengths.Add(v, -1);
        //         }
        //         pathLengths[vertex] = 0;
        //         Queue<TVertex> queue = new Queue<TVertex>();
        //         queue.Enqueue(vertex);
        //         while (queue.Count != 0)
        //         {
        //             TVertex currentNode = queue.Dequeue();
        //             List<TEdge> adjacentEdges = _g.AdjacentEdges(currentNode).ToList();
        //             List<TVertex> neighbours = new List<TVertex>();
        //             for (int j = 0; j < adjacentEdges.Count; j++)
        //             {
        //                 TEdge edge = adjacentEdges[j];
        //                 neighbours.Add((edge.Source == currentNode) ? edge.Target : edge.Source);
        //             }
        //             for (int j = 0; j < neighbours.Count; j++)
        //             {
        //                 TVertex neighbour = neighbours[j];
        //                 if (pathLengths[neighbour] == -1)
        //                 {
        //                     queue.Enqueue(neighbour);
        //                     pathLengths[neighbour] = pathLengths[currentNode] + 1;
        //                 }
        //             }
        //         }
        //         double average0 = 0.0;
        //         List<TVertex> pathLengthsLs = pathLengths.Keys.ToList();
        //         int path_lengths_lsCount = pathLengthsLs.Count;
        //         for (int j = 0; j < path_lengths_lsCount; j++)
        //         {
        //             TVertex v = pathLengthsLs[j];
        //             average0 += pathLengths[v];
        //         }
        //         average0 /= vg.Count;
        //         average += average0;
        //     }
        //     double result = Math.Round(average / vg.Count, 4);
        //     return result;
        // }
        // private double GetClusteringCoefficient()
        // {
        //     double clusteringCoefficient = 0.0;
        //     List<TVertex> vg = _g.Vertices.ToList();
        //     int vgCount = vg.Count;
        //     // iterate over all vertices v \in V_G
        //     for (int i = 0; i < vgCount; i++)
        //     {
        //         TVertex v = vg[i];
        //         List<TEdge> neighbourhoodEdges = new List<TEdge>();
        //         List<TVertex> neighboursV = new List<TVertex>();
        //         List<TEdge> adjacentEdgesV = _g.AdjacentEdges(v).ToList();
        //         // iterate over the adjacent edges of v, fill the neighbours list of v
        //         for (int j = 0; j < adjacentEdgesV.Count; j++)
        //         {
        //             TEdge edge = adjacentEdgesV[j];
        //             neighboursV.Add((edge.Source == v) ? edge.Target : edge.Source);
        //         }
        //         // iterate over all the neighbours of v
        //         for (int j = 0; j < neighboursV.Count; j++)
        //         {
        //             TVertex w = neighboursV[j];
        //             List<TVertex> neighboursW = new List<TVertex>();
        //             List<TEdge> adjacentEdgesW = _g.AdjacentEdges(w).ToList();
        //             // iterate over the adjacent edges of w, fill the neighbours list of w
        //             for (int k = 0; k < adjacentEdgesW.Count; k++)
        //             {
        //                 TEdge edge = adjacentEdgesW[k];
        //                 neighboursW.Add((edge.Source == w) ? edge.Target : edge.Source);
        //             }
        //             // iterate over all the neighbours of w
        //             for (int k = 0; k < neighboursW.Count; k++)
        //             {
        //                 TVertex x = neighboursW[k];
        //                 TEdge newedge = new TEdge(w, x);
        //                 // if the w's neighbour x is neighbours with v AND
        //                 // the newedge has not been added yet AND
        //                 // the newedge is not an edge between v and w
        //                 bool cond1 = neighboursV.Contains(x);
        //                 bool cond2 = !MF.IsEdgeContainedInList(newedge, neighbourhoodEdges);
        //                 bool cond3 = !MF.IsEdgeContainedInList(newedge, adjacentEdgesV);
        //                 if (cond1 && cond2 && cond3)
        //                     neighbourhoodEdges.Add(newedge);
        //             }
        //         }
        //         double possibleNumEdges = 0.0;
        //         possibleNumEdges = (neighboursV.Count * (neighboursV.Count - 1)) / (double)2;
        //         if (possibleNumEdges > 0)
        //             clusteringCoefficient += neighbourhoodEdges.Count / possibleNumEdges;
        //     }
        //     double result = Math.Round(clusteringCoefficient / vg.Count, 4);
        //     return result;
        // }

        // public List<TEdge> AdjacentEdges(TVertex vertex)
        // {
        //     return _g.AdjacentEdges(vertex).ToList();
        // }
    }
}