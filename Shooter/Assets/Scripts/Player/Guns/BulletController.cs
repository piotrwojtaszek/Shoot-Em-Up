using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float m_damage = 1;
    public ParticleSystem bulletHole;
    private void Start()
    {
        Invoke("DestoryMe", 1.25f);
    }
    public void Shoot(Vector3 hitPoint, float damage)
    {

        // the second argument, upwards, defaults to Vector3.up
        Vector3 direction = hitPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
        GetComponent<Rigidbody>().AddForce(transform.forward * 20f);

        m_damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<CubeEnemy>())
        {
            collision.gameObject.GetComponent<CubeEnemy>().TakeDamage(m_damage);
        }

        Instantiate(bulletHole, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

        Destroy(gameObject);

    }
    void DestoryMe()
    {
        Destroy(gameObject);
    }
}
