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
    public float maxSpeed = 20f;
    private bool grounded;
    [Tooltip("Jaką maskę uznajemy za ziemie")]
    public LayerMask whatIsGround;
    private float x, y;
    private bool jumping;
    private bool sprinting;
    private bool walled = false;
    public LayerMask whatIsWall;
    public float skinWidth = .1f;
    public float leapSpeed = 100f;
    private bool readyToJump = true;
    private float jumpCooldown = .1f;
    private float wallCooldown = .1f;
    private bool readyToWalled = true;
    private int wallId = -1;
    public int maxJumps = 1;
    private int jumpCount = 1;
    //dodac stamine ktora sie zmiejsza wraz z uzywaniem sprintu

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
        Look();
        MyInput();
        Movement();
        CheckGround();
        WallCheck();
        WallGrab();
    }

    private void Movement()
    {
        if (jumping && readyToJump)
        {
            Jump();
        }
        if (SetMaxVelocity())
        {
            return;
        }
        if (walled)
            rb.velocity = Vector3.zero;

        rb.AddForce(orientation.transform.forward * y * moveSpeed * Time.deltaTime);
        rb.AddForce(orientation.transform.right * x * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (grounded && readyToJump || readyToJump && jumpCount > 0)
        {
            readyToJump = false;
            float multiplier = jumpSpeed + 2f * jumpCount / maxJumps;
            rb.AddForce(Vector3.up * multiplier);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        jumpCount--;
        readyToJump = true;
    }
    private void ResetWalled()
    {
        readyToWalled = true;
    }
    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        jumping = Input.GetButtonDown("Jump");
        sprinting = Input.GetKey(KeyCode.LeftShift);
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
            jumpCount = maxJumps;
            ResetWallId();
        }
        else
        {
            grounded = false;
        }
    }

    private bool SetMaxVelocity()
    {
        float speed = Vector3.Magnitude(rb.velocity);  // test current object speed
        float maxCurrentSpeed = sprinting ? maxSpeed * 2f : maxSpeed;

        if (speed > maxCurrentSpeed)
        {
            float brakeSpeed = speed - maxSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rb.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rb.AddForce(-brakeVelocity);  // apply opposing brake force
            return true;
        }
        else
            return false;
    }

    private void WallCheck()
    {
        if (grounded || !readyToWalled)
        {
            walled = false;
            return;
        }

        Vector3 center = transform.TransformPoint(col.center);
        Vector3 size = transform.TransformVector(col.radius, col.height, col.radius);
        float radius = size.x;
        float height = size.y;
        Vector3 bottom = new Vector3(center.x, center.y - height / 2 + radius, center.z);
        Vector3 top = new Vector3(center.x, center.y + height / 2 - radius, center.z);
        Collider[] cols = Physics.OverlapCapsule(top, bottom, radius + skinWidth, whatIsWall);
        if (cols.Length > 0)
        {
            if (wallId != cols[0].GetHashCode())
            {
                walled = true;
                wallId = cols[0].GetHashCode();
                return;
            }
        }
        else
        {
            walled = false;
        }
    }

    private void WallGrab()
    {
        if (walled)
        {
            rb.velocity = Vector3.zero;
            WallLeap();
        }
    }

    private void WallLeap()
    {
        if (jumping && readyToWalled)
        {
            readyToWalled = false;
            rb.AddForce(playerCam.forward * leapSpeed);
            rb.AddForce(Vector3.up * leapSpeed * 0.25f);
            Invoke(nameof(ResetWalled), wallCooldown);
        }
    }

    private void ResetWallId()
    {
        wallId = -1;
    }
}