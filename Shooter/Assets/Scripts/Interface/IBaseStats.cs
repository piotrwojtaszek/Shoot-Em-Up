using System.Collections.Generic;
using UnityEngine;

public interface IBaseStats
{
    float MaxHealth { get; }
    float CurrentHealth { get; set; }
    void TakeDamage(float amount);
    void Die();
}
