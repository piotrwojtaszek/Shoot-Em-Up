using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GranadeGun : Gun
{
    AudioSource audio;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public float minForce = 10f;
    public float maxForce = 20f;
    float force = 10f;
    public override void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
            StartCoroutine(AccumulateForce());


    }



    IEnumerator AccumulateForce()
    {
        while (Input.GetButton("Fire1"))
        {
            force += Time.deltaTime * speed;
            force = Mathf.Clamp(force, minForce, maxForce);
            yield return null;
        }
        audio.Play();
        currentAmmo -= 1;
        RaycastHit hit;
        Vector3 destinationPoint = playerCam.position + playerCam.forward * range;

        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, range))
        {
            destinationPoint = hit.point;
        }
        GameObject obj = Instantiate(bullet, firePoint.position + firePoint.forward * .25f, Quaternion.identity);
        obj.GetComponent<BulletController>().Shoot(destinationPoint, force, force, range);
        foreach (ParticleSystem particle in muzzleFlash)
        {
            Instantiate(particle, firePoint);
        }
        force = minForce;
        yield return null;
    }
}
