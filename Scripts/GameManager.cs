using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   

    [SerializeField]
    private Text m_Timer;
    [SerializeField]
    private float m_Time = 60;
    private SceneSwitcher m_SceneSwitcher;
    private SpawnManager m_Spawner;
    private int m_Lives = 3;
    [SerializeField]
    private LivesScript m_LivesUI;
    [SerializeField]
    private AudioSource m_ErrorSound = null;
    [SerializeField]
    private AudioSource m_StartSound = null;
    [SerializeField]
    private AudioSource m_GameOverSound = null;
    bool m_GameStarted = false;

    private PlayerInputManager m_Manager;
    private Animation m_CamShake;
    

    private void Start()
    {
        m_SceneSwitcher = GameObject.Find("SceneSwitcher").GetComponent<SceneSwitcher>();
        m_Spawner = GameObject.Find("SpawnSystem").GetComponent<SpawnManager>();
        Debug.Log("Move: Left Stick\nGo Down/Grab with grabber: X or A");
        PlayerStats.Reset();
        m_Manager = GameObject.Find("PlayerInputSystemManager").GetComponent<PlayerInputManager>();
        m_CamShake = Camera.main.GetComponent<Animation>();

        BeltMovement[] belts = FindObjectsOfType<BeltMovement>();
        for (int i = 0; i < belts.Length; i++)
        {
            belts[i].Speed = 75;
        }
        MovingShader[] movingShaders = FindObjectsOfType<MovingShader>();
        for (int i = 0; i < movingShaders.Length; i++)
        {
            movingShaders[i].Reset();
        }

        string minutes = Mathf.Floor(m_Time / 60).ToString("00");
        string seconds = (m_Time % 60).ToString("00");

        m_Timer.text = (string.Format("{0}:{1}", minutes, seconds));
        m_StartSound.Play();
    }

    public bool GetIfStarted()
    {
        return m_GameStarted;
    }

    public void StartGame()
    {
        m_GameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Manager.playerCount == 1)
            m_GameStarted = true;
            

        if(m_GameStarted)
            m_Time -= Time.deltaTime;

        string minutes = Mathf.Floor(m_Time / 60).ToString("00");
        string seconds = (m_Time % 60).ToString("00");

        m_Timer.text = (string.Format("{0}:{1}", minutes, seconds));

        
        if(m_Time <= 0 || m_Lives <= 0)
        {
            PlayerStats.Missed = (int)m_Spawner.GetTotalPackages() - ((int)PlayerStats.Delivered + (int)PlayerStats.Mistakes + (int)PlayerStats.Recycled);
            m_GameOverSound.Play();
            m_SceneSwitcher.SwitchScene("ScoreScene");
        }
    }

    public void DeliveredGood()
    {
        PlayerStats.Delivered++;
    }

    public void DeliveredBlue()
    {
        PlayerStats.DeliveredBlue++;
    }
    public void DeliveredYellow()
    {
        PlayerStats.DeliveredYellow++;
    }

    public void DeliveredBad()
    {
        m_ErrorSound.Play();
        PlayerStats.Mistakes++;
        m_Lives--;
        m_LivesUI.Strike();
        m_CamShake.Play("CameraShake");
    }

    public void DeliveredGarbage()
    {
        m_ErrorSound.Play();
        PlayerStats.Mistakes++;
        m_Lives--;
        m_LivesUI.Strike();
        m_CamShake.Play("CameraShake");

    }

    public void DeliveredRecycled()
    {
        PlayerStats.Recycled++;
    }



}
