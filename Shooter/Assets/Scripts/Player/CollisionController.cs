using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude < 2f && collision.relativeVelocity.magnitude > 1f)
        {
            Debug.LogWarning("Male BUM");
            PlayerController.instance.TakeDamage(10f);
        }
        else if (collision.relativeVelocity.magnitude >= 2f && collision.relativeVelocity.magnitude <= 4f)
        {
            PlayerController.instance.TakeDamage(50f);
            Debug.LogWarning("Srednie BUM");
        }

        else if (collision.relativeVelocity.magnitude > 4f)
        {
            PlayerController.instance.TakeDamage(100f);
            Debug.LogWarning("Duze BUM");
        }
    }
}
