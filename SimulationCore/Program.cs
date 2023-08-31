// // See https://aka.ms/new-console-template for more information
// // Console.WriteLine("Hello, World!");

using System;
using System.Diagnostics;
using TVertex = System.String;

namespace ConsoleApp1{
    public class Program{
        // params
        private static double _p = 1 * Math.Pow(10, -2);
        private static double _r = Math.Sqrt(2);
        
        static void Main(string[] args){

            // Testing Get Index From Node in matrix with dimensions: dim1, dim2, dim3
            // MatrixFunctions.GetNodeIndexFunction();

            // Testing Saving States of Cell To Json File
            // JsonHandler.SaveExample();
            // MatrixFunctions.SaveCellsPositionsToJson();

            CellularAutomaton cellularAutomaton = Simulation.CreateSimulation();
            CellSerializer.SaveCellsToJson(cellularAutomaton.Organs[0][0].regions["Estroma"]);
            // MatrixFunctions.SaveCellsPositionsToJson(cellularAutomaton.Organs[0][0].regions["Estroma"].Cells);
            MatrixFunctions.SaveCellsPositionsToJson(cellularAutomaton.Organs[0][0].regions["Estroma"].Cells, true);

            Simulation.Simulate(cellularAutomaton, 100);
            // List<TVertex> template = MF.GetRegularNeighboursTemplate(_r);
            // List<TVertex> ftemplate = MF.FilterRegularNeighboursTemplate(template);

            // int[,] matrix = new int[1,2];
            
            // Cell cell1 = new Cell{x=0, y=0, z=0};
            // Cell cell2 = new Cell{x=0, y=1, z=0};
            // Cell cell3 = new Cell{x=0, y=1, z=1};
            // CellChunk cellChunk = new CellChunk{
            //     x=0, 
            //     y=0,
            //     z=0,
            //     cells = new Cell[]{cell1, cell2, cell3}
            //     };
            // ContentHandler.Save(cellChunk);
            // ContentHandler.Load();
        }
    }
}
    // class Sudoku{
    //     public static int Dimension = 9;

    //     public static void imprimir(int[,] tablero){
    //         for(int i = 0; i < Dimension; i++){
    //             if(i % 3 == 0)
    //                 Console.Write(" ");

    //         for(int j = 0; j < Dimension; j++){

    //             if(j % 3 == 0)
    //                 Console.Write(" ");

    //             Console.ForegroundColor = ConsoleColor.Yellow;
    //             Console.Write(tablero[i, j]);
    //             }
    //         Console.WriteLine();
    //         }
    //     }

    //     public static Boolean resolver(int[,] tablero){
    //         for(int i = 0; i < Dimension; i++){
    //             for(int j = 0; j < Dimension; j++){
    //                 if (tablero[i, j] != 0)
    //                     continue;
    //                 else{
    //                     for (int k = 1; k <= 9; k++){
    //                         if(esPosibleInsertar(tablero, i, j, k))
    //                         {
    //                             tablero[i, j] = k;
    //                             Boolean b = resolver(tablero);

    //                             if(b)
    //                                 return true;

    //                             tablero[i, j] = 0;
    //                         }
    //                     }

    //                     return false;
    //                 }
    //             }
    //         }

    //         Console.WriteLine();
    //         Console.ForegroundColor = ConsoleColor.White;
    //         Console.WriteLine("Solucion encontrada");
    //         imprimir(tablero);
    //         return true;
    //     }

    //     public static Boolean esPosibleInsertar(int[,] tablero, int i, int j, int valor){
    //         for (int a = 0; a < Dimension; a++){
    //             if(a != i && tablero[a, j] == valor)
    //                 return false;
    //         }

    //         for(int a = 0; a < Dimension; a++){
    //             if(a != j && tablero[i, a] == valor)
    //                 return false;
    //         }

    //         int y = (i / 3) * 3;
    //         int x = (j / 3) * 3;

    //         for(int a = 0; a < Dimension / 3; a++){
    //             for(int b = 0; b < Dimension / 3; b++){
    //                 if(a != i && b != j && tablero[y + a, x + b] == valor){
    //                     return false;
    //                 }
    //             }
    //         }
    //         return true;
    //     }
    // }

    
            // Utils a = new Utils();
            // System.Console.WriteLine(a.x);

            // Steps
            // Load and Save
            // int[,] tablero = new int[,]{
            //     {0,7,0,  0,0,0,  0,8,0},
            //     {0,5,8,  6,0,0,  0,0,1},
            //     {0,0,3,  1,4,0,  0,0,0},

            //     {9,0,6,  0,5,0,  3,0,0},
            //     {0,0,0,  0,0,0,  0,0,0},
            //     {0,0,5,  0,2,0,  1,0,7},

            //     {0,0,0,  0,6,5,  7,0,0},
            //     {3,0,0,  0,0,1,  9,2,0},
            //     {0,4,0,  0,0,0,  0,1,0},
            // };

            // int[,] tablero = new int[,]{
            //     {1,0,0,  0,0,0,  0,2,6},
            //     {0,0,3,  0,0,9,  0,0,0},
            //     {8,0,0,  0,0,2,  5,0,4},

            //     {0,4,0,  0,8,0,  0,0,0},
            //     {0,0,7,  0,0,4,  6,0,0},
            //     {0,0,0,  0,0,0,  1,0,2},

            //     {5,9,4,  0,0,6,  0,0,0},
            //     {0,0,0,  4,0,0,  9,0,8},
            //     {0,0,0,  0,5,7,  0,0,0},
            // };

            //  int[,] tablero = new int[,]{
            //     {2,5,8,  0,7,0,  0,3,9},
            //     {4,0,6,  0,0,3,  0,1,0},
            //     {0,9,0,  0,0,0,  2,7,4},

            //     {0,2,3,  0,0,6,  0,0,7},
            //     {0,0,9,  0,2,0,  3,0,0},
            //     {0,0,5,  0,0,0,  1,4,0},

            //     {0,0,0,  7,0,8,  0,0,1},
            //     {6,0,0,  0,5,2,  0,0,0},
            //     {0,0,0,  3,1,0,  0,6,0},
            // };

            // int[,] tablero = new int[,]{
            //     {0,7,0,  0,0,0,  0,0,0},
            //     {0,8,0,  4,0,0,  0,7,0},
            //     {0,0,0,  0,5,3,  0,4,0},

            //     {0,0,9,  1,0,0,  7,3,0},
            //     {7,0,0,  0,0,2,  0,0,0},
            //     {0,0,0,  0,0,4,  9,0,2},

            //     {5,0,0,  0,0,9,  8,0,0},
            //     {0,0,4,  5,0,0,  0,0,0},
            //     {1,0,3,  0,2,0,  0,0,4},
            // };

			// int[,] tablero = new int[,]{
            //     {1,0,0,  4,7,0,  0,8,0},
            //     {0,0,0,  0,6,2,  0,0,4},
            //     {0,0,4,  0,0,0,  0,0,0},

            //     {8,0,1,  0,0,5,  9,2,0},
            //     {0,0,0,  7,2,0,  0,0,1},
            //     {0,5,0,  0,0,0,  0,0,0},

            //     {0,2,0,  0,0,3,  0,0,0},
            //     {9,0,0,  0,0,0,  3,0,0},
            //     {0,0,0,  0,0,0,  1,0,7},
            // };


            /* Save from this
            int[,] tablero = new int[,]{
                {0,2,0,  5,0,6,  0,1,0},
                {6,0,3,  1,7,9,  0,0,0},
                {0,1,0,  3,0,0,  0,0,0},

                {0,0,1,  0,0,2,  3,4,0},
                {3,4,9,  0,1,0,  0,2,6},
                {2,0,6,  4,0,7,  8,0,0},

                {0,0,0,  6,5,8,  0,0,0},
                {5,0,8,  7,4,3,  0,6,0},
                {7,6,0,  0,0,1,  0,0,0},
            };


            Console.WriteLine("El Juego a resolver es: ");
            Sudoku.imprimir(tablero);

            if(!Sudoku.resolver(tablero))
                Console.WriteLine("El Sudoku no tiene solucion");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Presione la tecla Enter para salir");
            Console.Read();

            To this */

            // Console.WriteLine("Hello World");

            // Test();
            // StopwatchUsingMethod();
            // sdk.Display();
            // var timer = new Stopwatch();

            // timer.Start();
            // sdk.Solve(sdk.NextAvailableCell(), new Point());
            // timer.Stop();

            // Console.WriteLine("\n\n\nSolved Grid"); 
            // //...you can use the time taken here
        // }




//         static void Test(){
//             int[] a = new int[10];
            
//             for(int i=0; i < a.Length; i++){
//                 // Console.WriteLine(a[i]);
//             }
//         }


// //...
//         static void StopwatchUsingMethod()
//         {
//         //A: Setup and stuff you don't want timed
//         var timer = new Stopwatch();
//         timer.Start();

//         //B: Run stuff you want timed
//         timer.Stop();

//         TimeSpan timeTaken = timer.Elapsed;
//         string foo = "Time taken: " + timeTaken.ToString(@"m\:ss\.fff"); 
//         Console.WriteLine(foo);
//         }
//     }
// }

// C# Program for Implementing BinaryReader
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace binary_writer
// {
// 	internal class Program
// 	{
// 		static void Main(string[] args)
// 		{

//             // string tempPath = "..{0}Data{0}uploads{0}{{filename}}", Path.DirectorySeparatorChar;
//             // Console.WriteLine("..{0}Data{0}uploads{0}{{filename}}", Path.DirectorySeparatorChar);
            
//             Console.WriteLine(Path.GetFullPath(Directory.GetCurrentDirectory()));
// 			// string filePath = Path.Combine("..", "test", "test.txt");//@"C:\test\test.txt";
// 			string filePath = Path.Combine(Path.GetFullPath(Directory.GetCurrentDirectory()), "test", "test.txt");//@"C:\test\test.txt";

// 			//write in the stream
// 			using (BinaryWriter bw = new BinaryWriter(File.Open(filePath,
// 									FileMode.Create), Encoding.UTF8, false))
// 			{
// 				bw.Write(76);
// 				bw.Write(1.2);
// 				bw.Write('a');
// 				bw.Write("GeeksForGeeks");
// 				bw.Write(false);
// 			}
// 			if (File.Exists(filePath))
// 			{
// 				using (BinaryReader br = new BinaryReader(File.Open(filePath,
// 										FileMode.Open), Encoding.UTF8))
// 				{
// 					//Reads the data to the stream
// 					Console.WriteLine("int value is " + br.ReadInt32());
// 					Console.WriteLine("Double value is " + br.ReadDouble());
// 					Console.WriteLine("Char value is " + br.ReadChar());
// 					Console.WriteLine("value of string is " + br.ReadString());
// 					Console.WriteLine("for boolean value is " + br.ReadBoolean());
// 					Console.Read();

// 				}

// 			}
			
// 		}
// 	}
// }
