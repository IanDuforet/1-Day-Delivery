using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScript : MonoBehaviour
{
    GameObject m_FoundButton;
    private float m_ColorIntensity = 10;
    private float m_DelayTimer;
    private float m_MaxDelay = 0.2f;
 
    private void Update()
    {
        if (m_FoundButton)
        {
            m_DelayTimer -= Time.deltaTime;
            if(m_DelayTimer <= 0)
            {
                this.transform.parent.parent.GetComponent<NewMovement>().SetUp();   
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button")
        {
            m_FoundButton = other.gameObject;
            if(m_FoundButton)
            { 
                Color color = m_FoundButton.GetComponent<Renderer>().material.GetColor("_EmissiveColor");
                m_FoundButton.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * m_ColorIntensity);
                m_FoundButton.GetComponent<ButtonScript>().Interact();
                m_DelayTimer = m_MaxDelay;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button")
        {
            if(m_FoundButton)
            {
                Color color = m_FoundButton.GetComponent<Renderer>().material.GetColor("_EmissiveColor");
                m_FoundButton.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color / m_ColorIntensity);
                m_FoundButton = null;
            }
        }
    }
}
