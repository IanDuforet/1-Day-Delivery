using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battery : Interactable
{
    // Start is called before the first frame update
    [SerializeField]
    private float m_MaxLifeTime = 60.0f;
    [SerializeField]
    private float m_LifeTime = 60.0f;
    [SerializeField]
    private Image m_PowerImage;
    public bool IsDead = false;
    private bool m_HasJoined = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsDead && m_HasJoined)
        {
            m_LifeTime -= Time.deltaTime;
        }

        if(m_LifeTime < 0.0f)
        {
            IsDead = true;
        }

        float toFill = (float)m_LifeTime / (float)m_MaxLifeTime;
        m_PowerImage.fillAmount = toFill;
    }

    public override void OnInteract()
    {
        Debug.Log("push");
        IsDead = false;
        m_LifeTime = m_MaxLifeTime;
    }

    public void SetHasJoined(bool hasJoined)
    {
        m_HasJoined = hasJoined;
    }

}
