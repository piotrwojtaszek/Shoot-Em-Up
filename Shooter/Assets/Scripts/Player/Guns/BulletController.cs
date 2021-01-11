using UnityEngine;

public class BulletController : MonoBehaviour
{
    protected float m_damage = 1;
    public ParticleSystem[] bulletHole;

    public virtual void Update()
    {

    }
    public virtual void Shoot(Vector3 hitPoint, float damage, float speed, float distance)
    {

        
    }


}
