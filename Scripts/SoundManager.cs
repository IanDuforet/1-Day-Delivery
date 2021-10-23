using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private AudioSource m_Sound = null;
    [SerializeField]
    private float m_Delay = 0.0f;
    [SerializeField]
    private bool m_IsLooping = false;
    public bool IsPlaying = false;
    private float m_ElapsedTime = 0.0f;
    void Start()
    {
        m_Sound.Stop();
        if (m_Delay == 0.0f)
        {
            if (m_IsLooping)
            {
                m_Sound.Play();
                IsPlaying = true;
            }
            else
            {
                m_Sound.Play();
                IsPlaying = false;
            }
        }
        else
        {
            m_Sound.PlayDelayed(m_Delay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_IsLooping)
        {
            if(!IsPlaying)
            {
                m_Sound.Play();
                IsPlaying = true;
            }
        }
        IsPlaying = m_Sound.isPlaying;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Package" && other.gameObject.tag != "Garbage")
            return;
        m_Sound.Play();
    }
}
