using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private float lookRange = 10f;
    [SerializeField]
    protected int health;
    [SerializeField]
    private GameObject player;

    private NavMeshAgent agent;
    Transform target;


    public virtual void Init()
    {
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Awake()
    {
        Init();
    }

    public virtual void Update()
    {
        Movement();

    }

    public virtual void Movement()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRange)
        {
            agent.SetDestination(target.position);
        }
    }

}
