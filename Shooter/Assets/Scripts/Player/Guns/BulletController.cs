using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float m_damage = 1;
    public GameObject bulletHole;
    private void Start()
    {
        Invoke("DestoryMe", 10f);
    }
    public void Shoot(Vector3 direction, float damage, Vector3 velocity)
    {

        // the second argument, upwards, defaults to Vector3.up
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
        Instantiate(bulletHole, collision.contacts[0].point, Quaternion.identity);
        Destroy(gameObject);

    }
    void DestoryMe()
    {
        Destroy(gameObject);
    }
}
