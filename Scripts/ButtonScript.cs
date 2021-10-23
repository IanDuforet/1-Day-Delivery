using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    private Interactable m_Interactable = null;
    [SerializeField]
    private AudioSource m_Sound = null;
    public void Interact()
    {
        Debug.Log("int");
        if (m_Interactable != null)
        {
            m_Sound.Play();
            m_Interactable.OnInteract();
        }
    }
}
