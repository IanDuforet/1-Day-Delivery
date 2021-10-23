using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShader : MonoBehaviour
{
    public Material m_Material;

    //private Material m_CopyMaterial;

    [SerializeField]
    private Material m_BeltMaterial;

    [SerializeField]
    private GameObject m_Belt;

    private bool m_HasStarted = false;
    private float m_ElapsedTime = 0.0f;
    private void Start()
    {
        //m_CopyMaterial = m_Material;
        //m_Material.SetFloat("Speed", 0.8f);
        //m_Material = m_OriginalMat;
        this.GetComponent<MeshRenderer>().material = Material.Instantiate(m_BeltMaterial);
        m_Material = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material.SetFloat("Speed", 0.1f); /*= m_Material;*/
        m_HasStarted = true;
    }
    // Update is called once per frame
    void Update()
    {
        m_ElapsedTime += Time.deltaTime;
        Debug.Log(GetComponent<MeshRenderer>().material.GetFloat("Speed").ToString() + " " + m_ElapsedTime.ToString() );
        //Debug.Log(m_Material.name);
        if (GetComponent<MeshRenderer>().material.name == "M_movingBelt 1")
        {
            GetComponent<MeshRenderer>().material.SetFloat("Speed", -m_Belt.GetComponent<BeltMovement>().GetSpeed() + 0.1f);
        }
        else
            GetComponent<MeshRenderer>().material.SetFloat("Speed", -m_Belt.GetComponent<BeltMovement>().GetSpeed());
    }

    public void Reset()
    {
        if (m_HasStarted)
        {
            GetComponent<MeshRenderer>().material.SetFloat("Speed", -0.1f);
            GetComponent<MeshRenderer>().material = m_Material;
        }
    }
}
