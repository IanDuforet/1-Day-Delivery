using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageScript : MonoBehaviour
{
    public enum PackageColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Garbage
    }
    [SerializeField]
    private Material[] m_Materials = new Material[5];
    private Renderer m_Renderer;

    public PackageColor Color;

    public bool IsFalling = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Renderer = this.GetComponent<Renderer>();
        //m_Renderer.material = m_Materials[(int)Color];
    }

    private void Update()
    {
        if (this.GetComponent<Rigidbody>().velocity.y < -0.75f)
            IsFalling = true;
    }

    public PackageColor GetPackageType()
    {
        return Color;
    }
}
