using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy_Basic : Enemy, IBaseStats
{
    [SerializeField]
    public int Health { get; set; }

    public override void Init()
    {

    }

    public void Damage()
    {


    }

    public override void Movement()
    {

    }

    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}