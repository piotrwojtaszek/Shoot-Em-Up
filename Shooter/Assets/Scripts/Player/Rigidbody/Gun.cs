using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float speed = 1f;
    public ParticleSystem[] muzzleFlash;
    public Transform firePoint;
    public GameObject bullet;
    public Transform playerCam;

    private void Update()
    {
        Shoot();
    }

    public virtual void Shoot()
    {
        //override this method
    }
}
