using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ClawMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 10;
    [SerializeField]
    private float m_HeadSpeed = 10;
    [SerializeField]
    private float m_PistonMin = -1.5f;
    [SerializeField]
    private float m_PistonMax = 15f;

    [SerializeField]
    private LayerMask m_LayerMask;

    private Rigidbody m_Rb;
    private Vector2 m_Movement;
    private bool m_PistonDown;
    private Transform m_PistonTransform;
    private Transform m_TriggerTransform;

    private GrabScript m_GrabScript;
    private PushScript m_PushScript;
    private Battery m_Battery;

    private float m_MaxHoverTime = 2.0f;
    private float m_HoverTimer= 0;
    private bool m_IsHovering = false;

    private bool m_IsGrabbing;

    // Start is called before the first frame update
    void Start()
    {
        m_Rb = this.GetComponent<Rigidbody>();
        m_PistonTransform = this.transform.Find("Piston");
        m_TriggerTransform = m_PistonTransform.transform.Find("Trigger");

        m_GrabScript = this.GetComponentInChildren<GrabScript>();
        m_PushScript = this.GetComponentInChildren<PushScript>();
        m_Battery = GameObject.Find("Generator").GetComponent<Battery>();

    }

    private void MoveHeadUp()
    {
        m_PistonTransform.transform.Translate(0, (m_HeadSpeed * Time.deltaTime), 0, Space.Self);
        Vector3 copy = m_PistonTransform.position;
        copy.y = Mathf.Clamp(copy.y, m_PistonMin, m_PistonMax);
        m_PistonTransform.position = copy;
    }

    private void MoveHeadDown()
    {
        if (m_IsHovering)
        {
            m_HoverTimer -= Time.deltaTime;
            if (m_HoverTimer <= 0)
            {
                m_IsHovering = false;
                m_HoverTimer = m_MaxHoverTime;
            }
        }
        else
        {
            m_PistonTransform.transform.Translate(0, (-m_HeadSpeed * Time.deltaTime), 0, Space.Self);
            Vector3 copy = m_PistonTransform.position;
            copy.y = Mathf.Clamp(copy.y, m_PistonMin, m_PistonMax);
            m_PistonTransform.position = copy;
        }
        if (m_PistonTransform.position.y <= m_PistonMin)
        {
            m_IsHovering = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsGrabbing)
            m_LayerMask = m_LayerMask & ~(1 << 11);
        else
            m_LayerMask = m_LayerMask | (1 << 11);
        RaycastHit hit;
        Physics.Raycast(m_TriggerTransform.position, -m_TriggerTransform.up, out hit, Mathf.Infinity, m_LayerMask);


        if (m_GrabScript)
        {
            m_PistonMin = hit.point.y + 6f;
            if (!m_Battery.IsDead)
            {
                if(m_PistonDown)
                {
                    MoveHeadDown();
                }
                else
                {
                    MoveHeadUp();
                }
            }
        }
        else if(m_PushScript)
        {
            m_PistonMin = hit.point.y + 6.5f;


            if (m_PistonDown)
            {
                MoveHeadDown();
            }
            else
            {
                MoveHeadUp();
            }
        }
    }

    private void FixedUpdate()
    {
        if (m_GrabScript)
        {
            if (!m_Battery.IsDead)
            {
                m_Rb.velocity = new Vector3(m_Movement.x * m_Speed, 0, m_Movement.y * m_Speed);
            }
            else
            {
                m_Rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        else if (m_PushScript)
        {
            m_Rb.velocity = new Vector3(m_Movement.x * m_Speed, 0, m_Movement.y * m_Speed);
        }
    }
    private void OnMove(InputValue value)
    {
        m_Movement = value.Get<Vector2>();
    }

    private void OnMovePistonDown()
    {
        m_PistonDown = true;
    }

    private void OnMovePistonUp()
    {
        m_PistonDown = false;
        m_IsHovering = false;
        m_HoverTimer = m_MaxHoverTime;
    }

    private void OnInteract()
    {
        if (m_GrabScript && !m_Battery.IsDead)
        {
            m_GrabScript.Interact();
            m_IsGrabbing = m_GrabScript.IsGrabbing();
        }
    }

}
