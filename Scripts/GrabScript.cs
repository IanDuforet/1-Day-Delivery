using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour
{
    private bool m_IsGrabbing = false;
    private GameObject m_Package;
    private float m_DelayTimer;
    private float m_MaxDelay = 0.2f;

    [SerializeField]
    private GameObject m_Particle;
    [SerializeField]
    private Quaternion m_Rotation;
    [SerializeField]
    private Vector3 m_OffsetPosition;
    private bool m_IsExisting = false;
    private GameObject m_TempParticle;

    [SerializeField]
    private AudioSource m_Sound = null;
    // Update is called once per frame
    void Update()
    {
        if(m_Package)
        {
            m_DelayTimer -= Time.deltaTime;
            if (m_DelayTimer <= 0)
            {
                this.transform.parent.parent.GetComponent<NewMovement>().SetUp();
            }
            if (m_IsGrabbing)
            {
                m_Package.transform.position = this.transform.position;
            }
        }

        if(m_IsGrabbing)
        {
            if (!m_IsExisting)
            {
                if (m_Particle != null)
                {
                    m_TempParticle = Instantiate(m_Particle, gameObject.transform.position, m_Rotation);
                    m_IsExisting = true;
                }
            }
            else
            {
                m_TempParticle.transform.parent = gameObject.transform;
                m_TempParticle.transform.position = gameObject.transform.position + m_OffsetPosition;
            }

        }
        else
        {
            if(m_TempParticle != null)
            {
                Destroy(m_TempParticle);
                m_IsExisting = false;
            }
        }

    }

    public void Interact()
    {
        if (m_Package != null && !m_IsGrabbing)
        {
            m_IsGrabbing = true;
        }
        else if (m_Package != null && m_IsGrabbing)
        {
            m_IsGrabbing = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Package" || other.tag == "Garbage")
        {
            m_Sound.Play();
            m_IsGrabbing = true;
            m_Package = other.gameObject;
            m_DelayTimer = m_MaxDelay;
        }

    }

    public bool IsGrabbing()
    {
        return m_IsGrabbing;
    }
    private void OnTriggerExit(Collider other)
    {
        if (m_Package && !m_IsGrabbing)
        {
            m_Package = null;
        }
    }

}
