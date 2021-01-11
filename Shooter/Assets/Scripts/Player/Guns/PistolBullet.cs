using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBullet : BulletController
{
    public int damage = 1;
    protected Vector3 startPoint;
    protected float maxLiveDistance = 1f;
    private void Start()
    {
        startPoint = transform.position;
    }
    public override void Update()
    {
        if (Vector3.Distance(startPoint, transform.position) > maxLiveDistance)
        {
            Destroy(gameObject);
        }
    }
    public override void Shoot(Vector3 hitPoint, float damage, float speed, float distance)
    {
        Vector3 direction = hitPoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        maxLiveDistance = distance;
        m_damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealthController>())
        {
            collision.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        foreach (ParticleSystem particle in bulletHole)
        {
            Instantiate(particle, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }
        Destroy(gameObject);

    }
}
