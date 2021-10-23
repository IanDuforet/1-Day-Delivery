using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LivesScript : MonoBehaviour
{
    [SerializeField]
    private Image[] m_Crosses;
    [SerializeField]
    private Sprite m_NewCross;
    private int m_Index = 0;

    public void Strike()
    {
        Debug.Log(m_Index);
        if(m_Index < m_Crosses.Length)
        {
            m_Crosses[m_Index].sprite = m_NewCross;
            m_Index++;
        }
    }
}
