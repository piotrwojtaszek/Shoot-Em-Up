using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float speed = 1f;
    public int ammoOnReload = 10;
    public int currentAmmo = 10;
    public int ammoAll = 100;
    public ParticleSystem[] muzzleFlash;
    public Transform firePoint;
    public GameObject bullet;
    public Transform playerCam;

    private void Update()
    {

        UIAmmo.instance.TextUpdate(currentAmmo, ammoAll);

        if (currentAmmo > 0)
        {
            Shoot();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public virtual void Shoot()
    {
        //override this method
    }
    void Reload()
    {
        int temp = ammoOnReload - currentAmmo;

        if (ammoAll-temp > 0)
        {
            ammoAll -= temp;
            currentAmmo = ammoOnReload;
        }


    }
    public void SubstactAmmo()
    {
        currentAmmo -= 1;
    }
}
