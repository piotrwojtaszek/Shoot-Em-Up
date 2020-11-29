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
        Vector3 destinationPoint = playerCam.position + playerCam.forward * range;

        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            /*CubeEnemy cubeEnemy = hit.transform.GetComponent<CubeEnemy>();
             if (cubeEnemy != null)
             {
                 cubeEnemy.TakeDamage(damage);
             }*/

            destinationPoint = hit.point;
        }
        GameObject obj = Instantiate(bullet, firePoint.position + firePoint.forward * .25f, Quaternion.identity);
        obj.GetComponent<BulletController>().Shoot(destinationPoint, damage);
        foreach (ParticleSystem particle in muzzleFlash)
        {
            Instantiate(particle, firePoint);
        }
    }
}
