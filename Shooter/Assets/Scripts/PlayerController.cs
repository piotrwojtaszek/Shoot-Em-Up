using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : IBaseStats
{
    public float Health { get; set; }

    public float MaxHealth => throw new System.NotImplementedException();

    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField]
    private GameObject player;

    Transform target;
    NavMeshAgent agent;


    // Start is called before the first frame update
    void Awake()
    {
        target = player.transform;
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float amount)
    {
        throw new System.NotImplementedException();
    }
}
