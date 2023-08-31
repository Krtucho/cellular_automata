using UnityEngine;
using UnityEditor;  //for Editor (script must be in Editor folder)

//PURPOSE: inspector gridsize change, update scene

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Grid scr = (Grid)target;
        scr.GridOutline();
        scr.HighlightOutline(Vector3.zero);
    }
}
