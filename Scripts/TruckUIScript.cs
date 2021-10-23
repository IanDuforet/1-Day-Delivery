using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TruckUIScript : MonoBehaviour
{
    public enum PackageColor
    {
        Red,
        Green,
        Blue,
        Yellow,
        Garbage
    }

    private Text m_Text;
    private string m_String = "0/0";
    [SerializeField]
    private PackageColor m_TruckColor;
    private SpawnManager m_Spawner;
    private int m_ToCollect;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = this.GetComponentInChildren<Text>();
        m_Spawner = GameObject.Find("SpawnSystem").GetComponent<SpawnManager>();
        switch(m_TruckColor)
        {
            case PackageColor.Blue:
                m_ToCollect = m_Spawner.GetTotalBluePackages();
                break;
            case PackageColor.Yellow:
                m_ToCollect = m_Spawner.GetTotalYellowPackages();
                break;
        }

        m_String = "0/" + m_ToCollect;
        m_Text.text = m_String;
    }

    public void UpdateText()
    {
        switch (m_TruckColor)
        {
            case PackageColor.Blue:
                m_String = PlayerStats.DeliveredBlue.ToString() + "/" + m_ToCollect.ToString();
                break;
            case PackageColor.Yellow:
                m_String = PlayerStats.DeliveredYellow.ToString() + "/" + m_ToCollect.ToString();
                break;
        }
        m_Text.text = m_String;
    }
}
