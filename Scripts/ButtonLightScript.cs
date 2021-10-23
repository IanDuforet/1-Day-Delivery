using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLightScript : MonoBehaviour
{
    private float m_ColorIntensity = 1000;


    public void LightOn()
    {
        Color color = this.GetComponent<Renderer>().material.GetColor("_EmissiveColor");
        this.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color * m_ColorIntensity);
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void LightOff()
    {
        Color color = this.GetComponent<Renderer>().material.GetColor("_EmissiveColor");
        this.GetComponent<Renderer>().material.SetColor("_EmissiveColor", color / m_ColorIntensity);
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
