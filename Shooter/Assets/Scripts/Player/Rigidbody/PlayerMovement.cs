using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCam;
    public Transform orientation;
    public Transform groundCheck;
    private Rigidbody rb;
    private CapsuleCollider col;
    //Rotation and look
    private float xRotation;
    [Tooltip("Czulosc myszy")]
    public float sensitivity = 50f;
    private float desiredX;
    //Movement
    [Tooltip("Jak szybko ma przyspieszac")]
    public float moveSpeed = 4500;
    [Tooltip("Jak wysoko skacze")]
    public float jumpSpeed = 400f;
    public float maxSpeed = 20;
    public bool grounded;
    [Tooltip("Jaką maskę uznajemy za ziemie")]
    public LayerMask whatIsGround;
    private float x, y;
    private bool jumping;
    private bool walled = false;
    public LayerMask whatIsWall;
    public float skinWidth = .2f;
    public float leapSpeed = 100f;
    private bool oldWalled = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        CheckGround();
        WallCheck();
        WallGrab();
        MyInput();
        Look();
    }

    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        if (jumping && grounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            grounded = false;
        }
        if (SetMaxVelocity())
        {
            return;
        }
        if (grounded)
        {
            rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
            rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);
        }



    }

    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButtonDown("Jump");
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime;

        //Find current look rotation
        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        //Rotate, and also make sure we dont over- or under-rotate.
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Perform the rotations
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CheckGround()
    {
        Collider[] col = Physics.OverlapSphere(groundCheck.position, 0.2f, whatIsGround);
        if (col.Length > 0)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

    }

    private bool SetMaxVelocity()
    {
        float speed = Vector3.Magnitude(rb.velocity);  // test current object speed

        if (speed > maxSpeed)
        {
            float brakeSpeed = speed - maxSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity * 2f);  // apply opposing brake force
            return true;
        }
        else
            return false;
    }

    private void WallCheck()
    {
        if (grounded)
            return;
        Vector3 center = transform.TransformPoint(col.center);
        Vector3 size = transform.TransformVector(col.radius, col.height, col.radius);
        float radius = size.x;
        float height = size.y;
        Vector3 bottom = new Vector3(center.x, center.y - height / 2 + radius, center.z);
        Vector3 top = new Vector3(center.x, center.y + height / 2 - radius, center.z);
        Collider[] cols = Physics.OverlapCapsule(top, bottom, radius + skinWidth, whatIsWall);
        if (cols.Length > 0)
        {
            walled = true;
        }
        else
        {
            walled = false;
        }
    }

    private void WallGrab()
    {
        oldWalled = false;
        if (walled)
        {
            rb.useGravity = false;
            if (walled != oldWalled)
            {
                oldWalled = true;
                rb.velocity = Vector3.zero;
            }
        }

        WallLeap();
    }

    private void WallLeap()
    {
        if (y > 0f && walled && jumping)
        {
            rb.AddForce(playerCam.forward * leapSpeed, ForceMode.Impulse);
            rb.useGravity = true;
            skinWidth = 0.0f;
            Invoke("DelayWallCheck", 0.2f);
        }
    }

    private void DelayWallCheck()
    {
        skinWidth = 0.1f;
    }
}