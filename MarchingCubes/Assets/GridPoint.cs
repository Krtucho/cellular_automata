using System.Xml.Schema;
using UnityEngine;

// NOTE: this script is used for MarchingCube
// PURPOSE: enables each point to be easily identified as on / off

public class GridPoint : MonoBehaviour
{
    #region --- events ---
    public delegate void GridPointChange(bool on, Vector3 position);    //event handler signature
    public static event GridPointChange OnGridPointChange;              //event name
    #endregion

    public Color onColor = Color.red;
    public Color offColor = Color.grey;
    private float onSize = 0.1f;
    private float offSize = 0.025f;
    private MeshRenderer mr = null;
    private bool _on = false;

    public bool On
    {
        get
        {
            return _on;
        }
        set
        {
            _on = value; 
            GetRenderer().material.color = (value == true) ? onColor : offColor;

            if (value == true)
            {
                this.transform.localScale = new Vector3(onSize, onSize, onSize);
            }
            else
            {
                this.transform.localScale = new Vector3(offSize, offSize, offSize);
            }
        }
    }
    public bool Visible
    {
        get
        {
            return GetRenderer().enabled;
        }
        set
        {
            GetRenderer().enabled = value;
        }
    }
    public Vector3 Position
    {
        get
        {
            return this.transform.localPosition;
        }
        set
        {
            this.transform.localPosition = new Vector3(value.x, value.y, value.z);
        }
    }

    private void Start()
    {
        if (this.GetComponent<Rigidbody>() != null)
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }

        if (this.GetComponent<Collider>() != null)
        {
            this.GetComponent<Collider>().isTrigger = true;
        }
        
        On = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        On = true;

        if (OnGridPointChange != null)
        {
            OnGridPointChange(On, Position); //fire off event (for any code listening)
        }
    }
    private MeshRenderer GetRenderer()
    {
        if (mr == null)
        {
            mr = this.GetComponent<MeshRenderer>();
        }
        return mr;
    }
}
