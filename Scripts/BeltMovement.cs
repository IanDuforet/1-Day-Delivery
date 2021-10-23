using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltMovement : MonoBehaviour
{

    public float Speed = 5.0f;
    public float StartSpeed = 5.0f;

    [SerializeField]
    private float m_TimeSpeedUp = 5.0f;
    [SerializeField]
    private float m_SpeedUp = 5.0f;
    private float m_ElapsedTimeSpeedUp = 0.0f;

    public bool IsPaused = false;

    [SerializeField]
    private GameObject m_Particle;
    [SerializeField]
    private float m_ParticleLifeTime = 1.0f;

    [SerializeField]
    private AudioSource m_Sound = null;


    private GameManager m_Manager;
    private void Start()
    {
        m_Sound.Stop();
        StartSpeed = Speed;
        m_ElapsedTimeSpeedUp = 0.0f;
        m_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void Update()
    {
        if (m_Manager.GetIfStarted())
        {
            m_ElapsedTimeSpeedUp += Time.deltaTime;
            if (m_ElapsedTimeSpeedUp > m_TimeSpeedUp)
            {
                m_ElapsedTimeSpeedUp = 0.0f;
                Speed += m_SpeedUp;
            }
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (gameObject.layer == LayerMask.NameToLayer("Belts"))
        {
            //check if the gameObject should be moving
            if (other.gameObject.tag != "Package" && other.gameObject.tag != "Garbage")
                return;

            //setting the velocity using a certain speed and with the forward
            Vector3 velocity = (Speed * gameObject.transform.forward) * Time.deltaTime;

            other.gameObject.GetComponent<Rigidbody>().velocity = velocity;
            
            

        }
    }

    private void OnCollisionEnter(Collision other)
    {

        if (gameObject.layer == LayerMask.NameToLayer("Belts"))
        {
            //check if the gameObject should be moving
            if (other.gameObject.tag != "Package" && other.gameObject.tag != "Garbage")
                return;
            
            if (other.gameObject.GetComponent<PackageScript>().IsFalling)
            {
                Vector3 place = other.gameObject.transform.position;
                place.y -= 0.3f;
                Quaternion rotation = Quaternion.Euler(0, gameObject.transform.rotation.eulerAngles.y + 90, 0);

                m_Particle.transform.rotation = rotation;
                GameObject tempParticle = Instantiate(m_Particle, place, rotation);
                m_Sound.Play();
                if (m_ParticleLifeTime != 0.0f)
                    Destroy(tempParticle, m_ParticleLifeTime);
                other.gameObject.GetComponent<PackageScript>().IsFalling = false;
            }

        }
    }
    public float GetSpeed()
    {
        return (Speed / StartSpeed - m_SpeedUp);
    }

}
