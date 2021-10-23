using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackChangerScript : Interactable
{
    private Animator m_TrackChanger;
    [SerializeField]
    private ButtonLightScript m_ButtonLight;
    private bool m_State;
    [SerializeField]
    private AudioSource m_OpeningSound;
    [SerializeField]
    private AudioSource m_ClosingSound;
    void Start()
    {
        m_TrackChanger = this.GetComponent<Animator>();
    }

    public override void OnInteract()
    {
        m_State = m_TrackChanger.GetBool("IsOpen");
        m_TrackChanger.SetBool("IsOpen", !m_State);

        if (!m_State)
        {
            m_OpeningSound.Play();
            m_ButtonLight.LightOn();
        }
        else
        {
            m_ClosingSound.Play();
            m_ButtonLight.LightOff();
        }

    }


}
