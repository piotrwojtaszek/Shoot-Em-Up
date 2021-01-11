using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook m_freeLook;
    [SerializeField]
    private CinemachineVirtualCamera m_followCamera;
    private float xAngle;
    public float yAngle;
    public Vector2 m_Speed;
    public Joystick cameraJoystick;
    public RectTransform moveJoystick;
    private float horizontal;
    private float vertical;
    public bool m_moving = true;

    // Start is called before the first frame update
    private void Awake()
    {
        m_freeLook = GetComponent<CinemachineFreeLook>();
        xAngle = 0.0f;
        yAngle = m_freeLook.m_YAxis.Value;
        m_freeLook.Priority = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        horizontal = cameraJoystick.Horizontal;
        vertical = cameraJoystick.Vertical;

        if (m_moving && (horizontal != 0 || vertical != 0f))
        {
            xAngle = horizontal;
            yAngle += vertical / 3000f * m_Speed.y;
            m_freeLook.m_YAxis.Value = yAngle;
            m_freeLook.m_XAxis.Value = xAngle;
            yAngle = Mathf.Clamp(yAngle, 0f, 1f);
        }
    }

    public void CameraMode()
    {
        m_moving = !m_moving;
        if (m_moving)
        {
            m_freeLook.Priority = 100;
            cameraJoystick.gameObject.SetActive(m_moving);
            moveJoystick.sizeDelta = new Vector2(980, 980);
            m_followCamera.Priority = 1;
        }
        else
        {
            cameraJoystick.gameObject.SetActive(m_moving);
            moveJoystick.sizeDelta = new Vector2(1920, 980);
            m_freeLook.Priority = 1;
            m_followCamera.Priority = 100;
        }
    }
}
