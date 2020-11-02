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
    bool isGrounded = false;
    bool isWalled = false;
    public Transform groundCheck;
    public LayerMask groundMask;
    public LayerMask wallMask;
    int wallSide = 0;
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


        velocity = new Vector3((transform.right.x * x * speed), velocity.y, (transform.forward.z * z * speed));



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (!isWalled)
            velocity.y += gravity * Time.deltaTime;
        else if (isWalled)
        {
            velocity.y = 0;
        }

        if (wallSide == -1)
        {
            Debug.Log("Left");
        }
        else if (wallSide == 1)
        {
            Debug.Log("Right");
        }

        if (Input.GetButtonDown("Jump") && isWalled)
        {
            Vector3 leapJump = transform.right * x * leapForce;

        }
        //poruszanie sie
        //controller.Move(move * speed * Time.deltaTime);
        //grawitacja
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
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j += 2)
            {

                RaycastHit hit;
                Vector3 org = new Vector3(transform.position.x, transform.position.y + i, transform.position.z);
                Debug.DrawRay(org, transform.right * j, Color.red);
                if (Physics.Raycast(org, transform.transform.right * j, out hit, 1f, wallMask) && !GroundCheck())
                {
                    wallSide = j;
                    return true;
                }
            }
        }
        wallSide = 0;
        return false;
    }
}
