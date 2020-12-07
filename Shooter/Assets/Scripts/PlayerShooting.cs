using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer m_line;
    public Transform m_firePoint;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;


        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f))
        {
            IBaseStats interfaceHit = hit.transform.GetComponent<IBaseStats>();
            if (interfaceHit != null)
            {
                interfaceHit.TakeDamage(1);

            }
        }
        else
        {
            StartCoroutine(Line(hit, false));
        }

    }

    IEnumerator Line(RaycastHit hitInfo, bool hitted)
    {

        if (hitted)
        {
            m_line.SetPosition(0, m_firePoint.position);
            m_line.SetPosition(1, hitInfo.point);
        }
        else
        {
            m_line.SetPosition(0, m_firePoint.position);
            m_line.SetPosition(1, Camera.main.transform.forward * 100f);
        }
        m_line.enabled = true;
        yield return new WaitForSeconds(0.1f);
        m_line.enabled = false;
    }
}
