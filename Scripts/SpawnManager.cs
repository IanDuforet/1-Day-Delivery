using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_Packages;
    [SerializeField]
    private GameObject[] m_SpawnPoints;
    [SerializeField]
    private float m_SpawnTime = 5.0f;
    [SerializeField]
    private float m_StartTime = 7.0f;
    private float m_ElapsedTimeSpawn = 0.0f;
    private float m_ElapsedTimeFinal = 0.0f;
    private float m_ElapsedTimeStart = 0.0f;

    private GameManager m_Manager;

    private int m_PackageCounter = 0;
    private int m_AmountOfTotalPackages = 0;

    private float FinalTime = 0.0f;

    [SerializeField]
    private float m_RampUpTime = 0.5f;

    [SerializeField]
    private int m_AmountOfYellowPackages = 4;
    [SerializeField]
    private int m_AmountOfBluePackages = 3;
    [SerializeField]
    private int m_AmountOfGarbagePackages = 3;

    private int m_CurrentYellow;
    private int m_CurrentBlue;
    private int m_CurrentGarbage;

    private void Start()
    {
        m_CurrentYellow = m_AmountOfYellowPackages;
        m_CurrentGarbage = m_AmountOfGarbagePackages;
        m_CurrentBlue = m_AmountOfBluePackages;
        m_AmountOfTotalPackages = (m_AmountOfYellowPackages + m_AmountOfBluePackages + m_AmountOfGarbagePackages);
        m_Manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        if (m_Manager.GetIfStarted())
        {
            m_ElapsedTimeStart += Time.deltaTime;
            m_ElapsedTimeSpawn += Time.deltaTime;
            m_ElapsedTimeFinal += Time.deltaTime;
            if (m_ElapsedTimeStart > m_StartTime)
            {
                if (m_PackageCounter < m_AmountOfTotalPackages)
                {
                    if (m_ElapsedTimeSpawn >= m_SpawnTime)
                    {
                        Spawning();
                        m_ElapsedTimeSpawn = 0.0f;
                    }
                }

            }
        }
    }
    void Spawning()
    {
        int randomSpawnPoint = Random.Range(0, m_SpawnPoints.Length);
        if (m_PackageCounter % m_Packages.Length == 0 && m_CurrentYellow != 0)
        {
            Debug.Log("spawned");
            m_Packages[m_PackageCounter % m_Packages.Length].GetComponent<PackageScript>().Color = PackageScript.PackageColor.Yellow;
            --m_CurrentYellow;
        }
        else if (m_PackageCounter % m_Packages.Length == 1 && m_CurrentBlue != 0)
        {
            m_Packages[m_PackageCounter % m_Packages.Length].GetComponent<PackageScript>().Color = PackageScript.PackageColor.Blue;
            --m_CurrentBlue;
        }
        else if (m_PackageCounter % m_Packages.Length == 2 && m_CurrentGarbage != 0)
        {
            m_Packages[m_PackageCounter % m_Packages.Length].GetComponent<PackageScript>().Color = PackageScript.PackageColor.Garbage;
            --m_CurrentGarbage;
        }

        if (m_PackageCounter % 3 == 0)
            m_SpawnTime -= m_RampUpTime;
        Instantiate(m_Packages[m_PackageCounter % m_Packages.Length], m_SpawnPoints[randomSpawnPoint].transform);
        m_PackageCounter++;
    }

    public int GetTotalPackages()
    {
        return m_AmountOfTotalPackages;
    }

    public int GetTotalBluePackages()
    {
        return m_AmountOfBluePackages;
    }
    public int GetTotalYellowPackages()
    {
        return m_AmountOfYellowPackages;
    }
    public int GetTotalGarbagePackages()
    {
        return m_AmountOfGarbagePackages;
    }
}
