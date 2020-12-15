using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBullet : BulletController
{
    public float lifeTime = 1f;
    private void Start()
    {
        Invoke("DestroyMe", lifeTime);
    }

    public float extraDistance = 1f;
    public override void Shoot(Vector3 hitPoint, float damage, float speed, float distance)
    {
        Vector3 direction = hitPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        GetComponent<Rigidbody>().AddForce(Vector3.up * speed / 4f);
        m_damage = damage;
    }

    private void DestroyMe()
    {
        foreach (ParticleSystem particle in bulletHole)
        {
            Instantiate(particle,transform.position,Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider[] cols =  Physics.OverlapSphere(transform.position, 3f);
        foreach(Collider col in cols)
        {
            if (col.gameObject.GetComponent<CubeEnemy>())
            {
                col.gameObject.GetComponent<CubeEnemy>().TakeDamage(m_damage);
            }
        }

        foreach (ParticleSystem particle in bulletHole)
        {
            Instantiate(particle, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }
        Destroy(gameObject);

    }
}
