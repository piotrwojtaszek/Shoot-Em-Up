using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyDrone : EnemyController, IBaseStats
{
    [SerializeField]
    public float Health { get; set; }

    public float MaxHealth => throw new System.NotImplementedException();

    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
            Die();
    }
}