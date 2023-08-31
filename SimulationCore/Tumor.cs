namespace ConsoleApp1
{
    public class TumorConfig{
        public int dimX;
        public int dimY;
        public int dimZ;
        public int tumorDefaultState;
        public TumorConfig(int dimX = 1000, int dimY = 1000, int dimZ = 1000, int tumorDefaultState=3){
            this.dimX = dimX;
            this.dimY = dimY;
            this.dimZ = dimZ;
            this.tumorDefaultState = tumorDefaultState;
        }
    }
}