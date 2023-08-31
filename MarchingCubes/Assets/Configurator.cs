using UnityEngine;
using UnityEngine.UI;

public class Configurator : MonoBehaviour
{
    public GridPoint A = null;
    public GridPoint B = null;
    public GridPoint C = null;
    public GridPoint D = null;
    public GridPoint E = null;
    public GridPoint F = null;
    public GridPoint G = null;
    public GridPoint H = null;
    public LineRenderer line1 = null;
    public LineRenderer line2 = null;
    public LineRenderer line3 = null;
    public LineRenderer line4 = null;
    public LineRenderer line5 = null;
    public LineRenderer line6 = null;
    public LineRenderer line7 = null;
    public LineRenderer line8 = null;
    public LineRenderer line9 = null;
    public LineRenderer line10 = null;
    public LineRenderer line11 = null;
    public LineRenderer line12 = null;
    public LineRenderer line13 = null;
    public LineRenderer line14 = null;
    public LineRenderer line15 = null;
    public LineRenderer line16 = null;
    public Material material = null;
    public Text txt = null;
    private Mesh mesh = null;
    private int config = 0;

    private void Start()
    {
        Points.A = A;
        Points.B = B;
        Points.C = C;
        Points.D = D;
        Points.E = E;
        Points.F = F;
        Points.G = G;
        Points.H = H;

        GameObject go = this.gameObject;
        mesh = MarchingCube.GetMesh(ref go, ref material);

        DrawConfig();        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            btnNextConfig();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            btnPrevConfig();
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            Points.A.On = false;
            Points.B.On = false;
            Points.C.On = false;
            Points.D.On = false;
            Points.E.On = false;
            Points.F.On = false;
            Points.G.On = false;
            Points.H.On = false;
            DrawConfig();
        }

        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            Points.A.On = !Points.A.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.B) == true)
        {
            Points.B.On = !Points.B.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.C) == true)
        {
            Points.C.On = !Points.C.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.D) == true)
        {
            Points.D.On = !Points.D.On;
            DrawConfig();
        }

        if (Input.GetKeyDown(KeyCode.E) == true)
        {
            Points.E.On = !Points.E.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.F) == true)
        {
            Points.F.On = !Points.F.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.G) == true)
        {
            Points.G.On = !Points.G.On;
            DrawConfig();
        }
        if (Input.GetKeyDown(KeyCode.H) == true)
        {
            Points.H.On = !Points.H.On;
            DrawConfig();
        }
    }
    private void DrawConfig()
    {
        MarchingCube.Clear();

        config = MarchingCube.GetCubeConfig();
        MarchingCube.IsoFaces(config);
        MarchingCube.SetMesh(ref mesh);

        txt.text = string.Format("{0} {1}", Bits.BinaryForm(config), config);

        DrawTriangleLines();
    }
    private void DrawTriangleLines()
    {
        line1.positionCount = 0;
        line2.positionCount = 0;
        line3.positionCount = 0;
        line4.positionCount = 0;
        line5.positionCount = 0;
        line6.positionCount = 0;
        line7.positionCount = 0;
        line8.positionCount = 0;
        line9.positionCount = 0;
        line10.positionCount = 0;
        line11.positionCount = 0;
        line12.positionCount = 0;
        line13.positionCount = 0;
        line14.positionCount = 0;
        line15.positionCount = 0;
        line16.positionCount = 0;

        int numTriangles = (int)(MarchingCube.vertices.Count / 3f);
        switch (numTriangles)
        {
            case 1:     // 1 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);
                break;
            case 2:     // 2 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);
                break;
            case 3:     // 3 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);
                break;
            case 4:     // 4 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);
                break;
            case 5:     // 5 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);
                break;
            case 6:     // 6 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);
                break;
            case 7:     // 7 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);
                break;
            case 8:     // 8 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);
                break;
            case 9:     // 9 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);
                break;
            case 10:     // 10 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);
                break;
            case 11:     // 11 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);
                break;
            case 12:     // 12 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);

                line12.positionCount = 3;
                line12.SetPosition(0, MarchingCube.vertices[31]);
                line12.SetPosition(1, MarchingCube.vertices[32]);
                line12.SetPosition(2, MarchingCube.vertices[33]);
                break;
            case 13:     // 13 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);

                line12.positionCount = 3;
                line12.SetPosition(0, MarchingCube.vertices[31]);
                line12.SetPosition(1, MarchingCube.vertices[32]);
                line12.SetPosition(2, MarchingCube.vertices[33]);

                line13.positionCount = 3;
                line13.SetPosition(0, MarchingCube.vertices[34]);
                line13.SetPosition(1, MarchingCube.vertices[35]);
                line13.SetPosition(2, MarchingCube.vertices[36]);
                break;
            case 14:     // 14 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);

                line12.positionCount = 3;
                line12.SetPosition(0, MarchingCube.vertices[31]);
                line12.SetPosition(1, MarchingCube.vertices[32]);
                line12.SetPosition(2, MarchingCube.vertices[33]);

                line13.positionCount = 3;
                line13.SetPosition(0, MarchingCube.vertices[34]);
                line13.SetPosition(1, MarchingCube.vertices[35]);
                line13.SetPosition(2, MarchingCube.vertices[36]);

                line14.positionCount = 3;
                line14.SetPosition(0, MarchingCube.vertices[37]);
                line14.SetPosition(1, MarchingCube.vertices[38]);
                line14.SetPosition(2, MarchingCube.vertices[39]);
                break;
            case 15:     // 15 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);

                line12.positionCount = 3;
                line12.SetPosition(0, MarchingCube.vertices[31]);
                line12.SetPosition(1, MarchingCube.vertices[32]);
                line12.SetPosition(2, MarchingCube.vertices[33]);

                line13.positionCount = 3;
                line13.SetPosition(0, MarchingCube.vertices[34]);
                line13.SetPosition(1, MarchingCube.vertices[35]);
                line13.SetPosition(2, MarchingCube.vertices[36]);

                line14.positionCount = 3;
                line14.SetPosition(0, MarchingCube.vertices[37]);
                line14.SetPosition(1, MarchingCube.vertices[38]);
                line14.SetPosition(2, MarchingCube.vertices[39]);

                line15.positionCount = 3;
                line15.SetPosition(0, MarchingCube.vertices[40]);
                line15.SetPosition(1, MarchingCube.vertices[41]);
                line15.SetPosition(2, MarchingCube.vertices[42]);
                break;
            case 16:     // 16 triangle
                line1.positionCount = 3;
                line1.SetPosition(0, MarchingCube.vertices[0]);
                line1.SetPosition(1, MarchingCube.vertices[1]);
                line1.SetPosition(2, MarchingCube.vertices[2]);

                line2.positionCount = 3;
                line2.SetPosition(0, MarchingCube.vertices[3]);
                line2.SetPosition(1, MarchingCube.vertices[4]);
                line2.SetPosition(2, MarchingCube.vertices[5]);

                line3.positionCount = 3;
                line3.SetPosition(0, MarchingCube.vertices[6]);
                line3.SetPosition(1, MarchingCube.vertices[7]);
                line3.SetPosition(2, MarchingCube.vertices[8]);

                line4.positionCount = 3;
                line4.SetPosition(0, MarchingCube.vertices[9]);
                line4.SetPosition(1, MarchingCube.vertices[10]);
                line4.SetPosition(2, MarchingCube.vertices[11]);

                line5.positionCount = 3;
                line5.SetPosition(0, MarchingCube.vertices[12]);
                line5.SetPosition(1, MarchingCube.vertices[13]);
                line5.SetPosition(2, MarchingCube.vertices[14]);

                line6.positionCount = 3;
                line6.SetPosition(0, MarchingCube.vertices[15]);
                line6.SetPosition(1, MarchingCube.vertices[16]);
                line6.SetPosition(2, MarchingCube.vertices[17]);

                line7.positionCount = 3;
                line7.SetPosition(0, MarchingCube.vertices[18]);
                line7.SetPosition(1, MarchingCube.vertices[19]);
                line7.SetPosition(2, MarchingCube.vertices[20]);

                line8.positionCount = 3;
                line8.SetPosition(0, MarchingCube.vertices[21]);
                line8.SetPosition(1, MarchingCube.vertices[22]);
                line8.SetPosition(2, MarchingCube.vertices[23]);

                line9.positionCount = 3;
                line9.SetPosition(0, MarchingCube.vertices[22]);
                line9.SetPosition(1, MarchingCube.vertices[23]);
                line9.SetPosition(2, MarchingCube.vertices[24]);

                line10.positionCount = 3;
                line10.SetPosition(0, MarchingCube.vertices[25]);
                line10.SetPosition(1, MarchingCube.vertices[26]);
                line10.SetPosition(2, MarchingCube.vertices[27]);

                line11.positionCount = 3;
                line11.SetPosition(0, MarchingCube.vertices[28]);
                line11.SetPosition(1, MarchingCube.vertices[29]);
                line11.SetPosition(2, MarchingCube.vertices[30]);

                line12.positionCount = 3;
                line12.SetPosition(0, MarchingCube.vertices[31]);
                line12.SetPosition(1, MarchingCube.vertices[32]);
                line12.SetPosition(2, MarchingCube.vertices[33]);

                line13.positionCount = 3;
                line13.SetPosition(0, MarchingCube.vertices[34]);
                line13.SetPosition(1, MarchingCube.vertices[35]);
                line13.SetPosition(2, MarchingCube.vertices[36]);

                line14.positionCount = 3;
                line14.SetPosition(0, MarchingCube.vertices[37]);
                line14.SetPosition(1, MarchingCube.vertices[38]);
                line14.SetPosition(2, MarchingCube.vertices[39]);

                line15.positionCount = 3;
                line15.SetPosition(0, MarchingCube.vertices[40]);
                line15.SetPosition(1, MarchingCube.vertices[41]);
                line15.SetPosition(2, MarchingCube.vertices[42]);

                line16.positionCount = 3;
                line16.SetPosition(0, MarchingCube.vertices[43]);
                line16.SetPosition(1, MarchingCube.vertices[44]);
                line16.SetPosition(2, MarchingCube.vertices[45]);
                break;
        }
    }
    public void btnNextConfig()
    {
        config = Mathf.Clamp(config + 1, 0, 255);
        MarchingCube.SetCubeConfig(config);
        DrawConfig();
    }
    public void btnPrevConfig()
    {
        config = Mathf.Clamp(config - 1, 0, 255);
        MarchingCube.SetCubeConfig(config);
        DrawConfig();
    }
}
