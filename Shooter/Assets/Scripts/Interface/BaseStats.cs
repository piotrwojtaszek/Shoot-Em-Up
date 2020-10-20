using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public float m_maxHealth;
    public float m_currentHealth;

    public virtual void Awake()
    {
        m_currentHealth = m_maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        //Code this
        if(m_currentHealth-amount<=0f)
        {
            Die();
        }
        else
        {
            m_currentHealth -= amount;
        }
    }

    public virtual void Die()
    {
        //Code this
        Debug.Log("Die");
        Destroy(gameObject);
    }

}
