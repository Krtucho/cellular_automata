using System;
using System.Diagnostics;
// using System.Diagnostics;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Example01 : MonoBehaviour
{
    public Material material = null;
    public float zoom = 1.0f;
    public Text txt = null;
    public Grid grid = null;
    public Camera cam = null;
    public bool ListenPointEvent = false;
    private Mesh mesh = null;
    private Vector3 size = Vector3.zero;    //set through event when grid is ready
    private Vector3 orgCamPos;
    private Quaternion orgCamRot;

    private bool[,,] gridToLoad = null;

    private bool started = true;
    private bool ends;
    int toChange = 1;
    int current = 0;

    public ControladorDatosJuego controladorDatosJuego;
    public int steps = 0;
    private int actualStep = 0;

    private void Start()
    {
        GameObject go = this.gameObject;
        mesh = MarchingCube.GetMesh(ref go, ref material);
        MarchingCube.Clear();

        Transform t = cam.transform;
        orgCamPos = new Vector3(t.position.x, t.position.y, t.position.z);
        orgCamRot = new Quaternion(t.rotation.x, t.rotation.y, t.rotation.z, t.rotation.w);
    }
    private void Update()
    {
        // if(started){
        //     current += toChange;
        //     UnityEngine.Debug.Log(current);
        //     if(current % 5 == 0)
        //         UnityEngine.Debug.Log("working...");

        //     if(ends)
        //         started = false;

        //     UnityEngine.Debug.Log("Wating...");
        // }
    }
    private void OnEnable()
    {
        Grid.OnGridCreated += OnGridCreated;
        GridPoint.OnGridPointChange += OnGridPointChanged;
    }
    private void OnDisable()
    {
        Grid.OnGridCreated -= OnGridCreated;
        GridPoint.OnGridPointChange += OnGridPointChanged;
    }
    private void NoiseClear()
    {
        for (float z = 0; z < size.z; z++)
        {
            for (float y = 0; y < size.y; y++)
            {
                for (float x = 0; x < size.x; x++)
                {
                    GridPoint pt = MarchingCube.grd[(int)x, (int)y, (int)z];
                    pt.On = false;
                }
            }
        }
    }
    private void Noise2D()
    {
        NoiseClear();
        for (float z = 0; z < size.z; z++)
        {
            for (float x = 0; x < size.x; x++)
            {
                float height = Mathf.PerlinNoise(zoom * (x / size.x), zoom * (z / size.z)) * (size.y * 0.5f);
                for (int y = 0; y < height; y++)
                {
                    GridPoint pt = MarchingCube.grd[(int)x, y, (int)z];
                    pt.On = true;
                }
            }
        }
    }
    private void Noise3D()
    {
        NoiseClear();
        for (float z = 0; z < size.z; z++)
        {
            for (float y = 0; y < size.y; y++)
            {
                for (float x = 0; x < size.x; x++)
                {
                    float nx = (x / size.x) * zoom;
                    float ny = (y / size.y) * zoom;
                    float nz = (z / size.z) * zoom;
                    float noise = MarchingCube.PerlinNoise3D(nx, ny, nz);
                    if (noise > 0.5f)
                    {
                        GridPoint pt = MarchingCube.grd[(int)x, (int)y, (int)z];
                        pt.On = true;
                    }
                }
            }
        }
    }
    private void ConvertFromBoolToGridPoint(bool[,,,] booleanGrid)
    {
        GridPoint[,,] grd = new GridPoint[(int)grid.GridSize.x, (int)grid.GridSize.y, (int)grid.GridSize.z];
        // Convert
    }
    private void PrintMatrix(bool[,,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int k = 0; k < matrix.GetLength(2); k++)
                {
                    UnityEngine.Debug.Log(String.Format("({0} {1} {2}): {3}", i, j, k, matrix[i, j, k]));
                }
            }
        }
    }
    private void LoadGridAndMarchInstant(string filePath="")
    {

        // Update with grid matrix
        // Load if it's necessary
        // grdToLoad = LoadGridFromFile();
        // bool[,,] gridToLoad = new bool[(int)grid.GridSize.x, (int)grid.GridSize.y, (int)grid.GridSize.z];
        bool[,,] gridToLoad = controladorDatosJuego.CargarDatos(filePath);//new bool[(int)grid.GridSize.x, (int)grid.GridSize.y, (int)grid.GridSize.z];//DataHandler.CellLoader.LoadCellsFromJson("Assets/Cells/Cells.json");
        if (gridToLoad == null)
            return;

        // PrintMatrix(gridToLoad);

        // for (int i = 0; i < grid.GridSize.x; i++)
        // {
        //     gridToLoad[i, 0, 0] = true;
        // }

        for (int z = 0; z < grid.GridSize.z; z++)
        {
            for (int y = 0; y < grid.GridSize.y; y++)
            {
                for (int x = 0; x < grid.GridSize.x; x++)
                {

                    try
                    {
                        if (gridToLoad[x, y, z])
                            MarchingCube.grd[x, y, z].On = gridToLoad[x, y, z];
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
        }

        //Update
        btnInstant();
    }
    private void MarchInstant()
    {
        MarchingCube.Clear();
        MarchingCube.MarchCubes();
        MarchingCube.SetMesh(ref mesh);
    }
    private void PlaySimulationRender(){
        // MarchingCube.Clear();
        grid.btnClear();
        StartCoroutine(SimulationRender());
    }
    private IEnumerator SimulationRender(){
        actualStep = 0;
        while(actualStep < steps){
            btnLoad("/home/Krtucho/Unity/UnityProjects/Marching Cubes Full/Assets/Simulation/Steps/step_" + actualStep.ToString() +".json");
            yield return new WaitForSeconds(5.18f);
            UnityEngine.Debug.Log("Step: " + actualStep.ToString() + " From " + steps.ToString());
            actualStep++;
        }
    }
    private void MarchSlow()
    {
        MarchingCube.Clear();
        StartCoroutine(SlowStep());
    }
    private IEnumerator SlowStep()
    {
        for (int z = (int)(size.z - 2); z >= 0; z--)
        {
            for (int y = 0; y < (int)(size.y - 1); y++)
            {
                for (int x = 0; x < (int)(size.x - 1); x++)
                {
                    Vector3 p = new Vector3(x, y, z);
                    int config = MarchingCube.SetCubeToConfig(p);

                    grid.HighlightOutline(p);

                    MoveCamera(p);

                    txt.text = string.Format("{0} {1}", Bits.BinaryForm(config), config);

                    yield return new WaitForSeconds(0.07f);
                    if (config != 0 && config != 255)
                    {
                        yield return new WaitForSeconds(0.12f);
                        MarchingCube.SetMesh(ref mesh);
                        yield return new WaitForSeconds(0.18f);
                    }
                }
            }
        }

        cam.transform.position = orgCamPos;
        cam.transform.rotation = orgCamRot;
    }
    private void OnGridCreated(Vector3 sz)
    {
        size = new Vector3(sz.x, sz.y, sz.z);
    }
    private void OnGridPointChanged(bool on, Vector3 position)
    {
        if (ListenPointEvent == true)
        {
            btnInstant();
        }
    }
    private void MoveCamera(Vector3 p)
    {
        cam.transform.position = new Vector3(p.x + 0.5f, p.y + 6, p.z - 1.5f);
        cam.transform.LookAt(new Vector3(p.x + 0.5f, p.y + 0.5f, p.z + 0.5f));
    }
    public void btnLoad(string filePath="")
    {
        LoadGridAndMarchInstant(filePath);
    }
    public void btnPlay()
    {
        PlaySimulationRender();
    }
    public void btnInstant()
    {
        MarchInstant();
    }
    public void btnSlow()
    {
        MarchSlow();
    }
    public void btn2D()
    {
        Noise2D();
    }
    public void btn3D()
    {
        Noise3D();
    }
}
