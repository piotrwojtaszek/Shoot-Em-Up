using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementController : MonoBehaviour
{
    //wyzwanie: systemy spowalniajace obrot zawiodly, opanuj statek
    //wyzwanie: mozesz uzywac tylko jednego typu silnikow manewrowych tzn. np. Q i E, musisz odpowiednio dopasowac sie do figury
    //gdy obracamy kamera przyciski sa wylaczaone
    //usunac stabilise bo jest angular drag wiec 
    //zanim nie opanujemy sondy nie bedziemy w stanie wyhamowac 
    //pousuwac rigidbody z meteorow, i dawac je dopiero gdy gracz jest w poblizu
    //dac mozliwosc stalego napedu
    private Rigidbody m_rb;
    public float m_verticalTorque = 10f;
    public float m_horizontalTorque = 10f;
    public float m_rotateTorque = 10f;
    public float m_mainEngine = 100f;
    public Joystick joystick;
    public Action activeEngines;
    public bool m_invertControlls = false;
    private float m_invertVariable = 1f;

    private float horizontal = 0f;
    private float vertical = 0f;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        if (m_invertControlls)
        {
            m_invertVariable = 1f;
        }
        else
        {
            m_invertVariable = -1f;
        }
        //m_rb.maxAngularVelocity = 20f;

        /*m_rb.angularDrag = 0f;
        m_rb.AddTorque(transform.forward * m_rotateTorque * 8f,ForceMode.Impulse);
        m_rb.AddTorque(transform.up * m_rotateTorque * 12f, ForceMode.Impulse);*/
    }

    // Update is called once per frame
    private void Update()
    {
        // nie wiem po co to ale wole nie tykać xd
        m_rb.angularVelocity = new Vector3(Mathf.Round(m_rb.angularVelocity.x * 1000) / 1000f, Mathf.Round(m_rb.angularVelocity.y * 1000) / 1000f, Mathf.Round(m_rb.angularVelocity.z * 1000) / 1000f);
        //Debug.Log("x:" + m_rb.angularVelocity.x + "     y:" + m_rb.angularVelocity.y + "    z:" + m_rb.angularVelocity.z + "     RotatationSpeed:" + m_rb.angularVelocity.magnitude + "  velocity:" + m_rb.velocity.magnitude);

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        //vertical movement
        if (vertical > 0.15f || vertical < -0.15f)
            m_rb.AddTorque(transform.right * m_verticalTorque * .25f * vertical * m_invertVariable);

        //vertical movement
        if (horizontal > 0.15f || horizontal < -0.15f)
            m_rb.AddTorque(transform.up * m_horizontalTorque * horizontal * -1f * m_invertVariable);

        float accelerationX = Input.acceleration.x;
        accelerationX = Mathf.Clamp(accelerationX, -.5f, .5f);
        if (accelerationX > .15f)
        {
            m_rb.AddTorque(transform.forward * accelerationX * m_rotateTorque * -1f);
        }
        if (accelerationX < -.15f)
        {
            m_rb.AddTorque(transform.forward * m_rotateTorque * accelerationX * -1f);
        }

        activeEngines?.Invoke();
    }

    private void Stablilise()
    {
        if (m_rb.angularVelocity.magnitude < .07f)
        {
            m_rb.angularVelocity = Vector3.zero;
        }
    }//dodac losowe awarie kamere z przdu

    private void Engine()//osiagniecie max zniszczy statek
    {
        if (m_rb.velocity.magnitude < 5.5f)
            m_rb.AddForce(transform.forward * m_mainEngine);
        else
            LimitVelocity();
    }

    private void LimitVelocity()
    {
        if (m_rb.velocity.magnitude >= 5.5f)
            m_rb.velocity = Vector3.ClampMagnitude(m_rb.velocity, 5.49f);
    }

    public void EngineDown()
    {
        activeEngines += Engine;
    }
    public void EngineUp()
    {
        activeEngines -= Engine;
    }

}
