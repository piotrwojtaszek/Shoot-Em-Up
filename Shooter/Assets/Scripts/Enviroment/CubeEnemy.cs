using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour, IBaseStats
{
    public float maxHealth = 10;
    private float currentHealth = 0f;
    public float MaxHealth { get { return maxHealth; } }
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0f)
            Die();
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
