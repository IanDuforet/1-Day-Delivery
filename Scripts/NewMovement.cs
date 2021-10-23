using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class NewMovement : MonoBehaviour
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

    enum State {Up, Down, Pause};
    private State m_State = State.Up;

    private Transform m_PistonTransform;
    private Transform m_TriggerTransform;

    private GrabScript m_GrabScript;
    private PushScript m_PushScript;

    private float m_DelayTimer;
    private float m_MaxDelay = 0.2f;

    private bool m_IsGrabbing;

    private PauseMenu m_PauseMenu;

    [SerializeField]
    private AudioSource m_MoveSound = null;
    [SerializeField]
    private AudioSource m_UpAndDownSound = null;
    private bool m_IsPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Rb = this.GetComponent<Rigidbody>();
        m_PistonTransform = this.transform.Find("Piston");
        m_TriggerTransform = m_PistonTransform.transform.Find("Trigger");

        m_GrabScript = this.GetComponentInChildren<GrabScript>();
        m_PushScript = this.GetComponentInChildren<PushScript>();

        m_PauseMenu = FindObjectOfType<PauseMenu>();
        m_DelayTimer = m_MaxDelay;
    }

    public void SetUp()
    {
        m_State = State.Up;
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
       
        m_PistonTransform.transform.Translate(0, (-m_HeadSpeed * Time.deltaTime), 0, Space.Self);
        Vector3 copy = m_PistonTransform.position;
        copy.y = Mathf.Clamp(copy.y, m_PistonMin, m_PistonMax);
        m_PistonTransform.position = copy;

        if (m_PistonTransform.position.y <= m_PistonMin)
        {
            m_State = State.Pause;
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
            m_PistonMin = hit.point.y + 0.5f;
           
                switch (m_State)
                {
                    case State.Up:
                        MoveHeadUp();
                        break;
                    case State.Down:
                        MoveHeadDown();
                        break;
                    case State.Pause:
                    m_DelayTimer -= Time.deltaTime;
                    if (m_DelayTimer <= 0)
                    {
                        m_State = State.Up;
                    }
                    break;
                }
        }
        else if(m_PushScript)
        {
            m_PistonMin = hit.point.y + 0.5f;


           switch(m_State)
            {
                case State.Up:
                    MoveHeadUp();
                    break;
                case State.Down:
                    MoveHeadDown();
                    break;
            }
        }
        Debug.Log(m_State);
    }

    private void FixedUpdate()
    {
            m_Rb.velocity = new Vector3(m_Movement.x * m_Speed, 0, m_Movement.y * m_Speed);
    }
    private void OnMove(InputValue value)
    {
        m_Movement = value.Get<Vector2>();
        if(!m_MoveSound.isPlaying)
            m_MoveSound.Play();
    }

    private void OnInteract()
    {
        m_UpAndDownSound.Play();
       if(m_PushScript)
        {
        if(m_State == State.Down || m_State == State.Pause)
             m_State = State.Up;
        else if (m_State == State.Up)
             m_State = State.Down;
        }
        if (m_GrabScript)
        {
            if (m_State == State.Pause || m_State == State.Down)
                m_State = State.Up;
            else if (m_State == State.Up && m_GrabScript.IsGrabbing())
                m_GrabScript.Interact();
            else if (m_State == State.Up && !m_GrabScript.IsGrabbing())
            {
                m_State = State.Down;
                m_DelayTimer = m_MaxDelay;
            }
        }
    }

    private void OnPause()
    {
        Debug.Log("pause");
        if(m_PauseMenu)
            m_PauseMenu.GameIsPaused = !m_PauseMenu.GameIsPaused;
    }

}
