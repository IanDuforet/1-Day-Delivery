using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingBelts : Interactable
{
    [SerializeField]
    private float m_PauseTime = 5.0f;
    private float m_ElapsedTime = 0.0f;
    private bool m_IsPaused = false;
    private void Update()
    {
        if (m_IsPaused)
        {
            m_ElapsedTime += Time.deltaTime;
            if(m_ElapsedTime >= m_PauseTime)
            {
                gameObject.GetComponent<BeltMovement>().IsPaused = false;
                m_IsPaused = false;
                m_ElapsedTime = 0.0f;
            }
        }
        
    }

    public override void OnInteract() 
    {
        Debug.Log("paused1");

        gameObject.GetComponent<BeltMovement>().IsPaused = true;
        m_IsPaused = true;

    }
}
