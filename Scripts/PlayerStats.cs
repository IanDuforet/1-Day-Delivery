using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    //Amount of good packages delivered
    private static int m_Delivered = 0;
    //Amount of mistakes made
    private static int m_Mistakes = 0;
    //Amount of garbage you recycled
    private static int m_Recycled = 0;
    //Amount of packages not delivered
    private static int m_Missed = 0;


    private static int m_DeliveredBlue;
    private static int m_DeliveredYellow;

    public static int Delivered
    {
        get
        {
            return m_Delivered;
        }
        set
        {
            m_Delivered = value;
        }
    }

    public static int DeliveredBlue
    {
        get
        {
            return m_DeliveredBlue;
        }
        set
        {
            m_DeliveredBlue = value;
        }
    }

    public static int DeliveredYellow
    {
        get
        {
            return m_DeliveredYellow;
        }
        set
        {
            m_DeliveredYellow = value;
        }
    }

    public static int Mistakes
    {
        get
        {
            return m_Mistakes;
        }
        set
        {
            m_Mistakes = value;
        }
    }

    public static int Recycled
    {
        get
        {
            return m_Recycled;
        }
        set
        {
            m_Recycled = value;
        }
    }

    public static int Missed
    {
        get
        {
            return m_Missed;
        }
        set
        {
            m_Missed = value;
        }
    }

    public static void Reset()
    {
        m_Delivered = 0;
        m_DeliveredBlue = 0;
        m_DeliveredYellow = 0;
        m_Recycled = 0;
        m_Missed = 0;
        m_Mistakes = 0;
    }
}
