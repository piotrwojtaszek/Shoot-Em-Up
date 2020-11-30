using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolGun : Gun
{
    public override void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Vector3 destinationPoint = playerCam.position + playerCam.forward * range;

            if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
            {

                destinationPoint = hit.point;
            }
            GameObject obj = Instantiate(bullet, firePoint.position + firePoint.forward * .25f, Quaternion.identity);
            obj.GetComponent<BulletController>().Shoot(destinationPoint, damage, speed, range);
            foreach (ParticleSystem particle in muzzleFlash)
            {
                Instantiate(particle, firePoint);
            }
        } 
    }
}
