using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    //na razie walic bieganie po scianie, przylepianie jest wystarczajaco dobre
    private Rigidbody rb;

    private float x = 0.0f;
    private float y = 0.0f;
    private bool jumping = false;
    private bool sprinting = false;
    public float m_sensitivity = 10.0f;
    public Transform playerCam;
    public Transform orientation;

    private float desiredX;
    private float rotationX;

    public float moveSpeed = 10.0f;
    public float maxSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public float wallDragForce = 0.0f;
    private bool isInAir = false;
    private bool isWallRunning = true;
    public LayerMask groundMask, wallMask;
    public float wallLeapForce = 10f;
    public float wallStickForward = 10f;
    public float counterMovement;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //CounterMovement();
        SetMovement();
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        MyInputs();

    }

    private void MyInputs()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        jumping = Input.GetButton("Jump");
        sprinting = Input.GetKey(KeyCode.LeftShift);

        if (!GroundCheck() && !isWallRunning)
        {
            isInAir = true;
            x = 0;
        }


    }

    /// <summary>
    /// Camera rotation
    /// </summary>
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.fixedDeltaTime;

        Vector3 currentRotation = playerCam.transform.localRotation.eulerAngles;
        desiredX = currentRotation.y + mouseX;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

        playerCam.localRotation = Quaternion.Euler(rotationX, desiredX, 0f);
        orientation.localRotation = Quaternion.Euler(0.0f, desiredX, 0.0f);
    }

    private void SetMovement()
    {
        WallRun();
        Vector2 mag = LimitMaxVelocity();
        float xMag = mag.x, yMag = mag.y;

        if (x > 0 && xMag > maxSpeed) x = 0;
        if (x < 0 && xMag < -maxSpeed) x = 0;
        if (y > 0 && yMag > maxSpeed) y = 0;
        if (y < 0 && yMag < -maxSpeed) y = 0;

        Vector3 wallRunDirection = ForwardWallRun(WallCheck());
        if (isWallRunning)
        {
            rb.AddForce(wallRunDirection * y * moveSpeed * Time.fixedDeltaTime);
        }
        else
            rb.AddForce(orientation.forward * y * moveSpeed * Time.fixedDeltaTime);

        rb.AddForce(orientation.right * x * moveSpeed * Time.fixedDeltaTime);

        CounterMovement(mag);
    }

    //sprawdzic czy na pawno dobrzer dziala
    private Vector2 LimitMaxVelocity()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private void Jump()
    {
        if (jumping && GroundCheck())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(orientation.position, Vector3.down, out hit, 1.1f, groundMask))
        {
            //Debug.Log("Ground");
            return true;
        }
        return false;
    }


    private void WallRun()
    {
        int wallCheck = WallCheck();


        if (wallCheck != 0)
        {
            isWallRunning = true;
            x = 0;
            Vector3 wallRunDirection = ForwardWallRun(WallCheck());
            rb.AddForce(wallRunDirection * wallStickForward, ForceMode.Impulse);
            rb.useGravity = false;
            rb.AddForce(Vector3.down * wallDragForce, ForceMode.Force);
            if (jumping)
            {
                rb.useGravity = true;
                rb.AddForce(orientation.right * wallCheck * wallLeapForce, ForceMode.Impulse);
                rb.AddForce(Vector2.up * jumpForce / 2f, ForceMode.Impulse);
            }
        }
        else
        {
            isWallRunning = false;
            rb.useGravity = true;
        }
    }

    //jelsi sciaan to nadaj graczowi physical material 0 friction
    private int WallCheck()
    {
        int left = 0;
        int right = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j += 2)
            {

                RaycastHit hit;
                Vector3 org = new Vector3(orientation.position.x, orientation.position.y + i, orientation.position.z);
                //Debug.DrawRay(org, orientation.transform.right * j, Color.red);
                if (Physics.Raycast(org, orientation.transform.right * j, out hit, 1f, wallMask) && !GroundCheck())
                {
                    if (j < 0)
                        left++;
                    else
                        right++;
                }
            }
        }
        if (left > 1)
            return 1;
        else if (right > 1)
            return -1;

        return 0;
    }

    Vector3 ForwardWallRun(int side)
    {
        RaycastHit hit;
        if (isWallRunning)
        {
            if (Physics.Raycast(orientation.transform.position, orientation.transform.right * side * -1f, out hit, 1f, wallMask))
            {
                Vector3 normal = hit.normal;
                Vector3 velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                Vector3 direction = velocity - normal * Vector3.Dot(velocity, normal);
                Debug.DrawRay(orientation.transform.position, direction * 10f, Color.red);
                Debug.Log("ORIENTATION");
            }
        }

        return Vector3.zero;
    }

    void CounterMovement(Vector3 mag)
    {
        float threshold = 0.01f;
        if (GroundCheck())
        {
            if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
            {
                rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
            }
            if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
            {
                rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
            }
        }
    }
}
