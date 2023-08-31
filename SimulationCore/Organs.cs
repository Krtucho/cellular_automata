using System.Dynamic;
using System;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Drawing;
using System.Data.SqlTypes;
    namespace ConsoleApp1{

    public struct Point {
        public int x;
        public int y;
        public int z;

        // Redefine el operador de suma
        public static Point operator +(Point p1, Point p2) {
            return new Point { x = p1.x + p2.x, y = p1.y + p2.y, z = p1.z + p2.z };
        }

        // Redefine el operador de resta
        public static Point operator -(Point p1, Point p2) {
            return new Point { x = p1.x - p2.x, y = p1.y - p2.y, z = p1.z - p2.z };
        }
        public override string ToString(){
            return (x.ToString() + "," + y.ToString() + "," + z.ToString());
        }
        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public static bool operator <(Point p1, Point p2)
        {
            return (p1.x < p2.x) && (p1.y < p2.y) && (p1.z < p2.z);
        }

        public static bool operator >(Point p1, Point p2)
        {
            return (p1.x > p2.x) && (p1.y > p2.y) && (p1.z > p2.z);
        }

        public static bool operator <=(Point p1, Point p2)
        {
            return (p1.x == p2.x || p1.x < p2.x) && (p1.y == p2.y || p1.y < p2.y) && (p1.z == p2.z || p1.z < p2.z);
        }

        public static bool operator >=(Point p1, Point p2)
        {
            return p1 == p2 || p1 > p2;
        }
    }                   
    public struct Edge{
        public Point A;
        public Point B;
    }
    public interface Face{}
    public struct Triangle:Face{
        public Edge A;
        public Edge B;
        public Edge C;
    }
    public struct Square:Face{
        public Edge A;
        public Edge B;
        public Edge C;
    }
    public interface Polygon{
        // public string name;
        // public List<Face> faces; 
        int Scale{get; set;}
        Point InnerLimits{get; set;}
        Point OuterLimits{get; set;}
        public OrganRegion GenerateOrganRegion(string name="DefaultRegionName");
    }

    public struct CubePolygon: Polygon{

        public int scale{get; set;} //{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Scale {get => scale; set => scale = value;}
        public Point innerLimits;
        public Point outerLimits;
        public Point InnerLimits { get => innerLimits; set => innerLimits = value; }
        public Point OuterLimits { get => outerLimits; set => outerLimits = value; }

        public CubePolygon(int scale = 10)
        {
            this.scale = scale;
            this.innerLimits = new Point{ x = 0, y = 0, z = 0 };
            this.outerLimits = new Point{ x = scale, y = scale, z = scale };
        }


        public OrganRegion GenerateOrganRegion(string name="DefaultRegionName"){
            return new OrganRegion(outerLimits-innerLimits, name);
        }
        
    }
    public struct Connection{
        public Cell v;
        public Cell w;
        public Cell Source;
        public Cell Target;
        
        public Connection(Cell v, Cell w){
            this.v = v;
            this.w = w;
            this.Source = v;
            this.Target = w;
        }

        public Connection(Point v, Point w){
            this.v = new Cell{x = v.x, y = v.y, z = v.z};
            this.w = new Cell{x = w.x, y = w.y, z = w.z};//w;
            this.Source = this.v;
            this.Target = this.w;
        }

        public override string ToString()
        {
            return "Source: " + this.v.ToString() + "Target: " + this.w.ToString();
        }

        
    }

    public class RegionDescription{
        public List<Point> Bounds;
        public string Name;
        public string Shape;
        public string Description;
        public Polygon polygon;

        public RegionDescription(string name, string shape, int scale=10, Point inner=new Point(), Point outer=new Point()){
            this.Shape = shape;
            this.Name = name;
            if (GeneralSettings.OrgansPolygonsSettings.ContainsKey(shape)){
                this.polygon = GeneralSettings.OrgansPolygonsSettings[shape].Invoke();
                this.polygon.Scale = scale;
                this.polygon.InnerLimits = inner;
                this.polygon.OuterLimits = outer;
            }
        }
        public OrganRegion GenerateOrganRegion(string name="DefaultRegionName"){
            return polygon.GenerateOrganRegion(name);
        }
    }
    public class OrganSchema{
        public Dictionary<string, RegionDescription> Regions;


        public OrganSchema(){
            this.Regions = new();
        }
        
    }

   

    // Create a class for Organ
    public class Organ{
        public OrganSchema organSchema;
        public Dictionary<string, OrganRegion> regions;
        public Organ(OrganRegion[] Regions, Network netowork){

        }
        public  Organ(NetworkSettings networkSettings){
            // Por defecto se creara un organo compuesto por epitelio, luego lumen y x ultimo
            // Estroma. La capa del Lumen sera la menos gruesa
            // La forma(Shape) que tendra este organo x defecto sera un cubo
            this.organSchema = new();

            // Se divide la dimension total en los siguientes porcentajes:
            // - 30% Epitelio
            // - 10% Lumen
            // - 60% Estroma
            (Point epitelio, Point lumen, Point estroma) = ExtractRegionLimits(networkSettings.NetworkSizeX, networkSettings.NetworkSizeY, networkSettings.NetworkSizeZ);
            
            // Se crea el esquema del organo
            this.organSchema.Regions.Add("Epitelio", new RegionDescription("Epitelio", "Cube", 10, lumen, epitelio));
            this.organSchema.Regions.Add("Lumen", new RegionDescription("Lumen", "Cube", 10, estroma, lumen));
            this.organSchema.Regions.Add("Estroma", new RegionDescription("Estroma", "Cube", 10, default(Point), estroma));

            // Se crea el organo
            this.regions = new Dictionary<string, OrganRegion>();

            // Se crea el epitelio
            this.regions.Add("Epitelio", this.organSchema.Regions["Epitelio"].GenerateOrganRegion("Epitelio"));//new OrganRegion(epitelio);
            this.regions.Add("Lumen",  this.organSchema.Regions["Lumen"].GenerateOrganRegion("Lumen")); //new OrganRegion(lumen);
            this.regions.Add("Estroma", this.organSchema.Regions["Estroma"].GenerateOrganRegion("Estroma"));//new OrganRegion(estroma);


        }
        public static (int, int, int) ExtractPercentages(int totalDimension, int percentage1, int percentage2, int percentage3){
            // double number = 100; // El número que deseas dividir
            // double percentage1 = 30; // Porcentaje de la primera parte
            // double percentage2 = 50; // Porcentaje de la segunda parte
            // double percentage3 = 20; // Porcentaje de la tercera parte

            // Calcula las tres partes basadas en los porcentajes dados
            double part1 = totalDimension * percentage1 / 100;
            double part2 = totalDimension * percentage2 / 100;
            double part3 = totalDimension * percentage3 / 100;

            // Redondea cada parte al entero más cercano
            int intPart1 = (int)Math.Round(part1);
            int intPart2 = (int)Math.Round(part2);
            int intPart3 = (int)Math.Round(part3);

            // Imprime los resultados
            Console.WriteLine("Parte 1: {0}", intPart1);
            Console.WriteLine("Parte 2: {0}", intPart2);
            Console.WriteLine("Parte 3: {0}", intPart3);
            
            return (intPart1, intPart2, intPart3);
        }
        public static (Point, Point, Point) ExtractRegionLimits(int gridSizeX, int gridSizeY, int gridSizeZ){
            (int xEpitelio, int xLumen, int xEstroma) = ExtractPercentages(gridSizeX, 30, 10, 60);
            (int yEpitelio, int yLumen, int yEstroma) = ExtractPercentages(gridSizeY, 30, 10, 60);
            (int zEpitelio, int zLumen, int zEstroma) = ExtractPercentages(gridSizeZ, 30, 10, 60);

            (Point epitelio, Point lumen, Point estroma) = (
                new Point{x = xEpitelio+xLumen+xEstroma, y=yEpitelio+yLumen+yEstroma, z=zEpitelio+zLumen+zEstroma}, 
                new Point{x = xLumen+xEstroma, y = yLumen+yEstroma, z = zLumen+zEstroma}, 
                new Point{x = xEstroma, y = yEstroma, z = zEstroma}
                );

            return (epitelio, lumen, estroma);
        }
        public Tuple<int, int, int> GetCellLocation(Cell cell){
            return null;
        }
        public int GetCellsCount(){
            int count = 0;
            foreach(OrganRegion region in this.regions.Values){
                count += region.Cells.Length;
            }
            return count;
        }
        public bool AddCell(Point point, int state){
            return false;
        }
        public bool AddCell(Cell cell){
            // Find Cell Region
            OrganRegion organRegion = OrganRegionHandler.GetOrganRegion(this, cell);
            if(organRegion == null)
                return false;
            // Add Cell into Region
            RegionDescription regionDescription = organSchema.Regions[organRegion.name];
            if(organSchema.Regions[organRegion.name].Name == "Lumen")
                cell.state = 0;
            if(organSchema.Regions[organRegion.name].Name == "Epitelio")
                cell.state = 1;
            if(organSchema.Regions[organRegion.name].Name == "Estroma")
                cell.state = 2;
            
            return organRegion.AddCell(regionDescription, cell);
            // return false;
        }
        public bool DeleteCell(){
            // Find Cell Region
            // Delete Cell into Region
            return false;
        }
        public bool UpdateCell(){
            // Find Cell Region
            // Update Cell into Region
            return false;
        }
        public bool SearchCell(){
            // Find Cell Region
            // Search Cell into Region
            return false;
        }
        public IEnumerable<Cell> GetCells(){
             foreach(OrganRegion region in this.regions.Values)
                foreach(Cell cell in region.Cells)
                    yield return cell;
        }
        public Cell GetCell(int index){
            // Find Cell Region
            // Get Cell into Region
            return default;
        }

        // Cell Conections
        public List<Connection> GetCellConnections(Cell cell){
             // Find Cell Region
            OrganRegion organRegion = OrganRegionHandler.GetOrganRegion(this, cell);
            if(organRegion == null)
                return new List<Connection>();
            // Get Cell Connections into Region
            return organRegion.organRegionsData.GetCellConnections(cell);
            // return null;
        }
        public bool TryGetConnection(Cell origin, Cell oldtarget, out Connection to_remove_edge){
            to_remove_edge = default;
            List<Connection> connections = GetCellConnections(origin);
            foreach (Connection connection in connections)
            {
                if(connection.Target.point == oldtarget.point)
                {
                    to_remove_edge = connection;
                    return true;
                }
            }
            return false;
        }
        public bool AddCellConnection(Cell cellA, Cell cellB){
            // Find if CellA and CellB are inside the limits
            // Find Cell Region
            OrganRegion organRegion = OrganRegionHandler.GetOrganRegion(this, cellA);
            if(organRegion == null)
                return false;
            // Add Connection on Cell into Region
            return organRegion.organRegionsData.AddCellConnection(cellA, cellB);
        }
        public bool AddCellConnection(Point pointA, Point pointB){
            // Find Cell Region
            // Add Cell Connection into Region
            return false;
        }
        public bool DeleteCellConnection(Cell cellA, Cell cellB){
            // Find Cell Region
            // Delete Cell Connection into Region
            return false;
        }
        public bool DeleteCellConnection(Connection connection){
            return false;
        }
    }

    // Create a Graph class for represent organ regions
    // Create Node class
    // public class Node{
    //     public int Id;
    //     public string Name;
    //     public string Description;
    //     public List<Edge> Edges;
    // }

    // Create Edge class
    // public class Edge{
    //     public int Id;
    //     public string Name;
    //     public string Description;
    //     public Node NodeA;
    //     public Node NodeB;
    // }

    // public class Graph{
    //     public List<Node> Nodes;
    //     public List<Edge> Edges;
    // }


    // !!! Cada estado debe de contener detalles sobre a que Region pertenece y el tipo de Celula que es al encontrarse en ese estado
    // Clase referente a cierta region del organo, puede referirse al Tejido Epitelial, Lumen, Estroma, etc...
    public class OrganRegion{
        int maxCells = int.MaxValue;

        public OrganRegionsData organRegionsData;

        // Represent Regions with a 3D array
        public Cell[,,] Cells;
        public OrganRegionConnections Connections;
        public string name;
        public OrganRegion(Cell[,,] cells, OrganRegionConnections connections, string name="DefaultOrganRegion"){
            this.Cells = cells;
            this.Connections = connections;
            this.name = name;
            this.organRegionsData = new();

        }
        public OrganRegion(int gridSizeX, int gridSizeY, int gridSizeZ, string name="DefaultOrganRegion"){
            if(gridSizeX <= 0 || gridSizeY <= 0 || gridSizeZ <= 0)
                throw new ArgumentException("Grid size must be greater than 0");
            if(gridSizeX*gridSizeY*gridSizeZ > maxCells)
                {
                    SaveOnDisk();
                }
            this.Cells = new Cell[gridSizeX, gridSizeY, gridSizeZ];
            this.Connections = new OrganRegionConnections();
            this.name = name;
            this.organRegionsData = new();
        }
        public OrganRegion(Point gridSizes, string name = "DefaultOrganName"): this(gridSizes.x, gridSizes.y, gridSizes.z, name){}

        private void SaveOnDisk()
        {
            throw new NotImplementedException();
        }

        // Obtener posicion relativa dadas ciertas coordenadas desplazadas
        public Point Shift(RegionDescription regionDescription, Cell cell){
            Point innerLimit = regionDescription.polygon.InnerLimits;
            return cell.point - innerLimit;
        }

        // Create methods for adding, serching, updating and deleting a new item into 3D cell
        public bool AddCell(RegionDescription  regionDescription, Cell cell){
            Point location = Shift(regionDescription, cell);
            try{
                Cells[location.x, location.y, location.z] = cell;
                return true;
            }
            catch{
                return false;
            }
        }
        public Cell SearchCell(Cell cell){
            try{
                return this.Cells[cell.x, cell.y, cell.z];
            }catch{
                return default(Cell);
            }
        }
        public bool UpdateCell(Cell cell){
            try{
                this.Cells[cell.x, cell.y, cell.z] = cell;
                return true;
            }
            catch{
                return false;
            }

        }
        

        // Represent Region with a Graph
        // public OrganRegion(Graph graph){
        //     // Create a Graph
        // }
        public OrganRegion(){}
    }

    public class Lumen:OrganRegion{}
    public class Epitelio:OrganRegion{}

    public class OrganRegionsData{
        Dictionary<Point, List<Connection>> connections;
        public OrganRegionsData(){
            this.connections = new Dictionary<Point, List<Connection>>();
        }


        public List<Connection> GetCellConnections(Cell cell){
            try{
                return this.connections[cell.point];
            }
            catch{
                return new List<Connection>();
            }
        }
        public IEnumerable<Connection> GetConnections(){
            foreach(var value in this.connections.Values){
                foreach(var connection in value){
                    yield return connection;
                }
            }
        }

        public bool AddConnectionIntoList(Cell source, Cell target, List<Connection> sourceList){
                foreach(Connection connection in sourceList){
                    if(connection.Target == target){
                        return true;
                    }
                }
                sourceList.Add(new Connection(source, target));
                return true;
        }
        // public bool AddCellConnection(Point pointA, Point pointB){
        //     try{
        //         if(!this.connections.ContainsKey(pointA)){
        //             this.connections.Add(pointA, new List<Connection>());
        //         }
        //         if(!this.connections.ContainsKey(pointB)){
        //             this.connections.Add(pointB, new List<Connection>());
        //         }
                
        //         List<Connection> cellAConnections = this.connections[pointA];
        //         List<Connection> cellBConnections = this.connections[pointB];
        //         AddConnectionIntoList(pointA, pointB, cellAConnections);
        //         AddConnectionIntoList(pointB, pointA, cellBConnections);

        //         return true;
        //     }
        //     catch{
        //         return false;
        //     }
        // }
        public bool AddCellConnection(Cell cellA, Cell cellB){
            try{
                if(!this.connections.ContainsKey(cellA.point)){
                    this.connections.Add(cellA.point, new List<Connection>());
                }
                if(!this.connections.ContainsKey(cellB.point)){
                    this.connections.Add(cellB.point, new List<Connection>());
                }
                
                List<Connection> cellAConnections = this.connections[cellA.point];
                List<Connection> cellBConnections = this.connections[cellB.point];
                AddConnectionIntoList(cellA, cellB, cellAConnections);
                AddConnectionIntoList(cellB, cellA, cellBConnections);

                return true;
            }
            catch{
                return false;
            }
        }
    }
    public static class OrganRegionHandler{
        public static OrganRegion GetOrganRegion(Organ organ, Cell cell){
            foreach((var regionName, var region) in organ.organSchema.Regions){
                if(region.polygon.InnerLimits <= cell.point && cell.point <= region.polygon.OuterLimits){
                    // return region;
                    try{
                        return organ.regions[regionName];
                    }
                    catch{
                        System.Console.WriteLine(organ.regions.Keys);
                        break;
                    }
                }
            }
            // if(organ.organSchema.Regions.)
            return null;//new();
        }

        public static bool AddTumorOnRegion(OrganRegion organRegion, TumorConfig tumorConfig){
            int amount = organRegion.Cells.Length;
            (int sizeX, int sizeY, int sizeZ) = (organRegion.Cells.GetLength(0)/2, 
                                                organRegion.Cells.GetLength(1)/2,
                                                organRegion.Cells.GetLength(2)/2);

            Random seed = new Random();
            
            (int targetX, int targetY, int targetZ) = (seed.Next(sizeX), seed.Next(sizeY), seed.Next(sizeZ));

            Cell cellToUpdate = organRegion.Cells[targetX, targetY, targetZ];
            cellToUpdate.state = tumorConfig.tumorDefaultState;
            organRegion.UpdateCell(cellToUpdate);
            Console.WriteLine(cellToUpdate.ToString());

            for(int i = 0; i < 2; i++){
                for(int j = 0; j < 2; j++){
                    for(int k = 0; k < 1; k++){
                        try{
                            cellToUpdate = organRegion.Cells[targetX+i, targetY+j, targetZ+k];
                            cellToUpdate.state = tumorConfig.tumorDefaultState;
                            organRegion.UpdateCell(cellToUpdate);
                            Console.WriteLine(cellToUpdate.ToString());
                        }
                        catch{
                            continue;
                        }
                    }
                }
            }
            return true;
        }
    }

    // Clase para representar si es posible cruzar desde una region hacia otra y como. 
    // Para el caso del paso desde el lumen hacia el estroma se crea una via de traspaso
    // A traves del sistema circulatorio, pasando al estado vascular. Por tanto, se iran creando
    // nuevas conexiones entre ambas regiones
    public class OrganRegionConnections{
        public Point point; // Can be changed for public Cell cell;
        public List<Connection> AdjacencyList;

        public OrganRegionConnections(Point point = default(Point)){
            this.AdjacencyList = new List<Connection>();
            this.point = point;
        }
    }
    public class CellConnections{}
    public static class CellConnectionsHandler{
        public static IEnumerable<Connection> GetConnections(Organ organ, Cell cell){
            throw new NotImplementedException();
        }
        public static IEnumerable<Connection> GetConnections(OrganRegion organRegion){
            return organRegion.organRegionsData.GetConnections();
        }
        public static IEnumerable<Connection> GetConnections(OrganRegionsData organ, Cell cell){
            throw new NotImplementedException();
        }
    }

    public static class OrganHandler
    {
        internal static void AddTumor(CellularAutomaton cellularAutomaton, TumorConfig tumorConfig)
        {
            Organ organ = cellularAutomaton.Organs[0][0];

            OrganRegionHandler.AddTumorOnRegion(organ.regions["Epitelio"], tumorConfig);
        }
        internal static void AddTumorOnEstroma(CellularAutomaton cellularAutomaton, TumorConfig tumorConfig)
        {
            Organ organ = cellularAutomaton.Organs[0][0];

            OrganRegionHandler.AddTumorOnRegion(organ.regions["Estroma"], tumorConfig);
        }
        public static Organ GetOrgan(List<Organ> organs, Cell cell){
            throw new NotImplementedException();
        }
    }
}