using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageScript : MonoBehaviour
{
    private GameManager m_Manager;
    [SerializeField]
    private AudioSource m_Sound = null;

    [SerializeField]
    private Animation m_Brrr;

    // Start is called before the first frame update
    void Start()
    {
        m_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

  


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Package")
        {
            GrabScript garbScript = FindObjectOfType<GrabScript>();
            if (!garbScript.IsGrabbing())
            {
                PackageScript script = other.GetComponent<PackageScript>();
                if ((int)script.GetPackageType() == (int)PackageScript.PackageColor.Garbage)
                {
                    m_Manager.DeliveredRecycled();
                }
                else
                {
                    m_Manager.DeliveredBad();
                }
                m_Sound.Play();
                Destroy(other.gameObject);
                m_Brrr.Play("ShredderShake");
            }
        }
    }

}
