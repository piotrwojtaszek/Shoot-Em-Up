using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private float m_mouseSensivity = 100f;
    [SerializeField]
    private Transform m_playerBody;
    float m_xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
        m_playerBody.Rotate(Vector3.up * mouseX);
    }
}
