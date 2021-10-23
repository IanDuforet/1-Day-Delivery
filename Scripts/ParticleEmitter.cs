using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject m_Particle;
    [SerializeField]
    private float m_LifeTime = 1.0f;
    [SerializeField]
    private Vector3 m_OffsetPosition;
    [SerializeField]
    private Quaternion m_Rotation;
    void Start()
    {
        if(m_LifeTime == 0.0f)
            Spawn();
    }

    void Spawn()
    {
        if (m_Particle != null)
        {
            GameObject tempParticle = Instantiate(m_Particle, m_OffsetPosition, m_Rotation);
            if (m_LifeTime != 0.0f)
                Destroy(tempParticle, m_LifeTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("enter");
        if (collision.gameObject.tag != "Package" && collision.gameObject.tag != "Garbage")
            return;
        m_OffsetPosition = collision.gameObject.transform.position;
        m_OffsetPosition.y += 0.5f;
        Spawn();
    }
}
