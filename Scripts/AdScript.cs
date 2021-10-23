using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdScript : MonoBehaviour
{
    private Image m_AdImage;
    [SerializeField]
    private Sprite[] m_Sprites;
    private float m_AdTime = 3f;
    private float m_Timer = 0;
    private int m_Index = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_AdImage = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(m_Index);
        m_Timer -= Time.deltaTime;
        if(m_Timer <= 0)
        {
            //m_AdImage.CrossFadeAlpha(0f, 0.5f, false);
            m_AdImage.sprite = m_Sprites[m_Index];
            //m_AdImage.CrossFadeAlpha(1f, 0.5f, false);
            m_Timer = m_AdTime;
            m_Index++;
            if (m_Index > m_Sprites.Length-1)
                m_Index = 0;
        }
    }
}
