using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreScript : MonoBehaviour
{
    [SerializeField]
    private Text m_DeliveredText;
    [SerializeField]
    private Text m_RecyledText;
    [SerializeField]
    private Text m_MissedText;
    // Start is called before the first frame update
    void Start()
    {
        m_DeliveredText.text = PlayerStats.Delivered.ToString();
        m_RecyledText.text = PlayerStats.Recycled.ToString();
        m_MissedText.text = PlayerStats.Missed.ToString();
    }

   
}
