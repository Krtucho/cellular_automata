public static class Converter{
    public static int[,,] Convert(bool[,,] matrix){
        int[,,] result = new int[matrix.GetLength(0), matrix.GetLength(1), matrix.GetLength(2)];
        
        for(int i = 0; i < matrix.GetLength(0); i++){
            for(int j = 0; j < matrix.GetLength(1); j++){
                for(int k = 0; k < matrix.GetLength(2); k++){
                    result[i, j, k] = matrix[i, j, k] ? 1 : 0;
                }
            }
        }
        
        return result;
    }
    public static bool[,,] Convert(int[,,] matrix){
        bool[,,] result = new bool[matrix.GetLength(0), matrix.GetLength(1), matrix.GetLength(2)];
        
        for(int i = 0; i < matrix.GetLength(0); i++){
            for(int j = 0; j < matrix.GetLength(1); j++){
                for(int k = 0; k < matrix.GetLength(2); k++){
                    result[i, j, k] = matrix[i, j, k] != 0;
                }
            }
        }
        
        return result;
    }
}