using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float leapForce = 10f;
    Vector3 velocity;
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isWalled = false;
    public Transform groundCheck;
    public Transform[] wallCheck;
    public LayerMask groundMask;
    public LayerMask wallMask;
    bool leftWallCheck = true;
    bool rightWallCheck = true;
    int wallSide = 0;
    float wallCheckRange = 0.8f;
    Vector3 wallNormal;
    // Update is called once per frame
    void Update()
    {
        isGrounded = GroundCheck();
        if (!isGrounded)
            isWalled = WallCheck();
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        velocity = transform.right * x * speed + transform.forward * z * speed + transform.up * velocity.y;


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isWalled)
            velocity.y += gravity * Time.deltaTime;
        else if (isWalled && !isGrounded)
        {
            velocity.x = wallNormal.z * 2f;
            if (velocity.y >= 2f)
                velocity.y += gravity / 2f * Time.deltaTime;
            else if (velocity.y <= -1.5f)
                velocity.y += gravity / 8f * Time.deltaTime;
            else
                velocity.y += gravity / 2f * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isWalled && !isGrounded)
        {
            StartCoroutine(WallCheckOff());
            velocity = wallNormal * leapForce;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Debug.Log("Leap");
        }

        controller.Move(velocity * Time.deltaTime);
    }

    bool GroundCheck()
    {
        for (float i = 0f; i < 1.1f; i += 0.5f)
        {
            for (float j = 0f; j < 1.1f; j += 0.5f)
            {
                Debug.DrawRay(groundCheck.root.position + new Vector3(-0.5f + i, groundCheck.localPosition.y, -0.5f + j), Vector3.down * 0.1f, Color.red);
            }
        }
        int counter = 0;
        for (float i = 0f; i < 1.1f; i += 0.5f)
        {
            for (float j = 0f; j < 1.1f; j += 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(groundCheck.root.position + new Vector3(-0.5f + i, groundCheck.localPosition.y, -0.5f + j), Vector3.down, 0.1f, groundMask))
                {
                    counter++;

                }
            }
        }
        if (counter > 2)
            return true;
        return false;

    }

    bool WallCheck()
    {
        for (float z = -0.5f; z < 1f; z += 0.5f)
        {
            for (float i = -0.5f; i < 1; i++)
            {
                for (int j = -1; j < 2; j += 2)
                {
                    if (leftWallCheck && j == -1 || rightWallCheck && j == 1)
                    {
                        RaycastHit hit;
                        Debug.DrawRay(transform.position + transform.forward * z + transform.up * i, transform.right * j + transform.forward * z, Color.red);
                        if (Physics.Raycast(transform.position + transform.forward * z + transform.up * i, transform.right * j+transform.forward*z/2f, out hit, .8f, wallMask) && !GroundCheck())
                        {
                            wallSide = j;
                            wallNormal = hit.normal;
                            return true;
                        }
                    }

                }
            }
        }
        wallSide = 0;
        return false;
    }

    IEnumerator WallCheckOff()
    {
        if (wallSide == -1)
            leftWallCheck = false;
        else if (wallSide == 1)
            rightWallCheck = false;
        yield return new WaitForSeconds(.5f);

        leftWallCheck = true;
        rightWallCheck = true;
    }
}
