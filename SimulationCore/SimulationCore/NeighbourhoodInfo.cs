
namespace ConsoleApp1{// Informacion sobre los alrededores de cierta celula
    public class NeighbourhoodInfo{
        public Cell cell{get;set;}
        public List<Cell> cancerCells{get;set;}
        public NeighbourhoodInfo(Cell cell, OrganRegion organRegion, OrganRegionsData organRegionsData){
            List<Connection> connections = organRegionsData.GetCellConnections(cell);
            this.cancerCells = new();
            foreach(Connection connection in connections){
                Cell w = organRegion.SearchCell(connection.w);
                if(w != default(Cell) && w.state >= 3){
                    cancerCells.Add(w);
                }
            }
        }
    }
}