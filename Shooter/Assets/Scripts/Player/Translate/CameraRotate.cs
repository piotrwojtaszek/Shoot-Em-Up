using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    Player_Movement player;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player_Movement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        if (player.isWalled)
        {
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            transform.localRotation = Quaternion.Euler(xRotation, currentRotation.y + mouseX, 0f);
            Debug.Log("JAZDA");
        }
        else
        {
            Debug.Log("NOPE");
            mouseX += transform.localRotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            playerBody.Rotate(Vector3.up * mouseX);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

    }
}
