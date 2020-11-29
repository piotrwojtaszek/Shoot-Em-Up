using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem[] muzzleFlash;
    public Transform firePoint;
    public GameObject bullet;
    public Transform playerCam;
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

        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            /*CubeEnemy cubeEnemy = hit.transform.GetComponent<CubeEnemy>();
             if (cubeEnemy != null)
             {
                 cubeEnemy.TakeDamage(damage);
             }*/

            Vector3 hitPoint = hit.point - firePoint.position;
            GameObject obj = Instantiate(bullet, firePoint.position, Quaternion.identity);
            obj.GetComponent<BulletController>().Shoot(hitPoint, 1f, GetComponent<Rigidbody>().velocity);
        }
        foreach (ParticleSystem particle in muzzleFlash)
        {
            Instantiate(particle, firePoint);
        }
    }
}
