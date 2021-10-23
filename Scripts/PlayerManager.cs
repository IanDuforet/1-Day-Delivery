using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_CraneType1;
    [SerializeField]
    private GameObject m_CraneType2;
    private PlayerInputManager m_Manager;

    [SerializeField]
    private Text m_Player1JoinText;
    [SerializeField]
    private Text m_Player2JoinText;

    [SerializeField]
    private Transform m_SpawnPosPlayer1;
    [SerializeField]
    private Transform m_SpawnPosPlayer2;

    // private Battery m_Battery;
    private bool m_FirstJoin = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Manager = this.GetComponent<PlayerInputManager>();
        //m_Battery = GameObject.Find("Generator").GetComponent<Battery>();
    }

    void OnPlayerJoined()
    {
        Debug.Log(m_Manager.playerCount.ToString());
        if (m_Manager.playerCount == 1)
        {
            m_Manager.playerPrefab = m_CraneType1;
            m_Player1JoinText.enabled = false;
            GameObject.Find("NewGrabber(Clone)").transform.position = m_SpawnPosPlayer1.position;
            m_Player2JoinText.CrossFadeAlpha(0, 5, false);
        }
        else if (m_Manager.playerCount == 2)
        {
            m_Manager.playerPrefab = m_CraneType2;
            m_Player2JoinText.enabled = false;
            GameObject.Find("NewPusher(Clone)").transform.position = m_SpawnPosPlayer2.position;

        }

        if (!m_FirstJoin)
        {
            m_FirstJoin = true;
            //m_Battery.SetHasJoined(m_FirstJoin);
        }

        
    }
}
