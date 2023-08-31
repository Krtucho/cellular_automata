using UnityEngine;
using UnityEngine.UI;

// PURPOSE: make grid of points for marchingcube

public class Grid : MonoBehaviour
{
    #region --- event ---
    public delegate void GridCreated(Vector3 gridsize);
    public static event GridCreated OnGridCreated;
    #endregion

    public Vector3 GridSize = new Vector3(3, 3, 3);
    public Text txt = null;

    private void Start()
    {
        MarchingCube.grd = new GridPoint[(int)GridSize.x, (int)GridSize.y, (int)GridSize.z];
        MakeGrid();
    }
    private void Update()
    {
        //moving the highlight
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            Vector3 p = CurrentHighlight();
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                HighlightOutline(new Vector3(p.x, p.y, p.z + 1));
                DisplayCurrentConfig();
            }
            else
            {
                HighlightOutline(new Vector3(p.x, p.y + 1, p.z));
                DisplayCurrentConfig();
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            Vector3 p = CurrentHighlight();
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
                HighlightOutline(new Vector3(p.x, p.y, p.z - 1));
                DisplayCurrentConfig();
            }
            else
            {
                HighlightOutline(new Vector3(p.x, p.y - 1, p.z));
                DisplayCurrentConfig();
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            Vector3 p = CurrentHighlight();
            HighlightOutline(new Vector3(p.x - 1, p.y, p.z));
            DisplayCurrentConfig();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            Vector3 p = CurrentHighlight();
            HighlightOutline(new Vector3(p.x + 1, p.y, p.z));
            DisplayCurrentConfig();
        }
    }
    private void MakeGrid()
    {
        Transform points = this.transform.Find("points");

        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    go.name = string.Format("P{0}.{1}.{2}", x, y, z);
                    go.transform.parent = points;
                    go.transform.localPosition = new Vector3(x, y, z);
                    go.GetComponent<Collider>().isTrigger = true;
                    Rigidbody rb = go.AddComponent<Rigidbody>();
                    rb.useGravity = false;
                    MarchingCube.grd[x, y, z] = go.AddComponent<GridPoint>();
                    MarchingCube.grd[x, y, z].On = false;
                }
            }
        }

        if (OnGridCreated != null)
        {
            OnGridCreated(GridSize);
        }
    }
    public void GridOutline()
    {
        Vector3 p = this.transform.position;
        float x = GridSize.x - 1;
        float y = GridSize.y - 1;
        float z = GridSize.z - 1;
        LineRenderer line = null;

        //ABCD
        line = this.transform.Find("line0").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + x, p.y + 0, p.z + 0));
        line.SetPosition(2, new Vector3(p.x + x, p.y + y, p.z + 0));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + y, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + 0));

        //EFGH
        line = this.transform.Find("line1").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + z));
        line.SetPosition(1, new Vector3(p.x + x, p.y + 0, p.z + z));
        line.SetPosition(2, new Vector3(p.x + x, p.y + y, p.z + z));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + y, p.z + z));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + z));

        //EAGC
        line = this.transform.Find("line2").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + 0, p.y + 0, p.z + z));
        line.SetPosition(2, new Vector3(p.x + 0, p.y + y, p.z + z));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + y, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + 0));

        //FBHD
        line = this.transform.Find("line3").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + x, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + x, p.y + 0, p.z + z));
        line.SetPosition(2, new Vector3(p.x + x, p.y + y, p.z + z));
        line.SetPosition(3, new Vector3(p.x + x, p.y + y, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + x, p.y + 0, p.z + 0));
    }
    public void HighlightOutline(Vector3 p)
    {
        /*
         *  E ----- F
         *  |       |
         *  | A ----- B
         *  | |     | |
         *  G |---- H |
         *    |       |
         *    C ----- D     p is the C corner
         */

        GridPoint gp = null;
        LineRenderer line = null;

        //ABCD EFGH
        gp = this.transform.Find("A").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 0, p.y + 1, p.z + 0);
        gp = this.transform.Find("B").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 1, p.y + 1, p.z + 0);
        gp = this.transform.Find("C").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 0, p.y + 0, p.z + 0);
        gp = this.transform.Find("D").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 1, p.y + 0, p.z + 0);

        gp = this.transform.Find("E").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 0, p.y + 1, p.z + 1);
        gp = this.transform.Find("F").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 1, p.y + 1, p.z + 1);
        gp = this.transform.Find("G").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 0, p.y + 0, p.z + 1);
        gp = this.transform.Find("H").GetComponent<GridPoint>();
        gp.Position = new Vector3(p.x + 1, p.y + 0, p.z + 1);

        //ABCD
        line = this.transform.Find("line4").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + 1, p.y + 0, p.z + 0));
        line.SetPosition(2, new Vector3(p.x + 1, p.y + 1, p.z + 0));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + 1, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + 0));

        //EFGH
        line = this.transform.Find("line5").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + 1));
        line.SetPosition(1, new Vector3(p.x + 1, p.y + 0, p.z + 1));
        line.SetPosition(2, new Vector3(p.x + 1, p.y + 1, p.z + 1));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + 1, p.z + 1));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + 1));

        //EAGC
        line = this.transform.Find("line6").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 0, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + 0, p.y + 0, p.z + 1));
        line.SetPosition(2, new Vector3(p.x + 0, p.y + 1, p.z + 1));
        line.SetPosition(3, new Vector3(p.x + 0, p.y + 1, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + 0, p.y + 0, p.z + 0));

        //FBHD
        line = this.transform.Find("line7").GetComponent<LineRenderer>();
        line.loop = false;
        line.positionCount = 5;
        line.SetPosition(0, new Vector3(p.x + 1, p.y + 0, p.z + 0));
        line.SetPosition(1, new Vector3(p.x + 1, p.y + 0, p.z + 1));
        line.SetPosition(2, new Vector3(p.x + 1, p.y + 1, p.z + 1));
        line.SetPosition(3, new Vector3(p.x + 1, p.y + 1, p.z + 0));
        line.SetPosition(4, new Vector3(p.x + 1, p.y + 0, p.z + 0));
    }
    private Vector3 CurrentHighlight()
    {
        return this.transform.Find("line4").GetComponent<LineRenderer>().GetPosition(4);        
    }
    private void DisplayCurrentConfig()
    {
        Vector3 p = CurrentHighlight();
        
        int x = (int)p.x;
        int y = (int)p.y;
        int z = (int)p.z;

        Points.A = MarchingCube.grd[x, y + 1, z];
        Points.B = MarchingCube.grd[x + 1, y + 1, z];
        Points.C = MarchingCube.grd[x, y, z];
        Points.D = MarchingCube.grd[x + 1, y, z];
        Points.E = MarchingCube.grd[x, y + 1, z + 1];
        Points.F = MarchingCube.grd[x + 1, y + 1, z + 1];
        Points.G = MarchingCube.grd[x, y, z + 1];
        Points.H = MarchingCube.grd[x + 1, y, z + 1];

        int config = MarchingCube.GetCubeConfig();
        txt.text = string.Format("{0} {1}", Bits.BinaryForm(config), config);
    }
    public void btnGridOn()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z].Visible = true;
                }
            }
        }
    }
    public void btnGridOff()
    {
        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z].Visible = false;
                }
            }
        }
    }
    public void btnClear()
    {
        MarchingCube.Clear();

        for (int z = 0; z < GridSize.z; z++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                for (int x = 0; x < GridSize.x; x++)
                {
                    MarchingCube.grd[x, y, z].On = false;
                    MarchingCube.grd[x, y, z].Visible = true;
                }
            }
        }

        MarchingCube.MarchCubes();
    }
}
