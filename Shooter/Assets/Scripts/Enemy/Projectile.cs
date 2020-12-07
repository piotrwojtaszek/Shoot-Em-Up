using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;

    private bool hited = false;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public bool Attack(Transform target)
    {
        Debug.Log("Player " + target);
        rb.velocity = target.forward * 50;
        if (hited)
            return true;
        else
            return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DUPA");
    }
}
