using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseStats
{
    int Health { get; set; }
    void TakeDamage(float amount);
    void Die();
}
