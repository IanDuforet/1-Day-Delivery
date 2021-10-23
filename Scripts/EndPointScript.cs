using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour
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
    private PackageColor m_EndPointColor;
    [SerializeField]
    private Material[] m_Materials = new Material[4];

    private GameManager m_Manager;

    [SerializeField]
    private TruckUIScript m_TruckPanel;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInParent<Renderer>().material = m_Materials[(int)m_EndPointColor];
        m_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Package")
        {
            PackageScript script = other.GetComponent<PackageScript>();
            if((int)script.GetPackageType() == (int)PackageColor.Garbage)
            {
                m_Manager.DeliveredGarbage();
            }
            else if ((int)script.GetPackageType() == (int)m_EndPointColor)
            {
                switch(m_EndPointColor)
                {
                    case PackageColor.Blue:
                        m_Manager.DeliveredBlue();
                        break;
                    case PackageColor.Yellow:
                        m_Manager.DeliveredYellow();
                        break;
                }
                m_TruckPanel.UpdateText();
                m_Manager.DeliveredGood();
            }
            else
            {
                m_Manager.DeliveredBad();
            }

            Destroy(other.gameObject);
        }
    }
}
