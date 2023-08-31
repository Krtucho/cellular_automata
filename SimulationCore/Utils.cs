using System.Runtime.InteropServices;

namespace ConsoleApp1{

    
    
    // Using Span for saving and loading the cells
    public static class ContentHandler{
        // public Span<Cell> cells;
        // public Span<Organ> organs;

        public static void SaveAdjacencyList(CellAdjacencyList cellAdjacencyList){
            
        }

        public static void Save(CellChunk CellChunk){
            string filePath = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory()), "test", "test.txt");//@"C:\test\test.txt";
            // Save the cells
            // Save the organs

            // CellChunk.cells = CellChunk.            
            using( var writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                var bytes = MemoryMarshal.Cast<Cell, byte>(new Span<Cell>(CellChunk.cells));
                writer.Write(bytes);
            }
        }

        public static void Load(){
            // Load the cells
            // Load the organs
            string filePath = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory()), "test", "test.txt");//@"C:\test\test.txt";
            
            var binary = File.ReadAllBytes(filePath);
            var save = MemoryMarshal.Cast<byte, Cell>(binary);
            System.Console.WriteLine(save.ToString());
        }
    }

    public class Utils{
    public int x;

    public static void SaveObject(){

    }

    public static void LoadObject(){
        
    }

    }
}