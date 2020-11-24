using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private float lookRange = 10f;
    [SerializeField]
    protected int health;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject projectilePrefab;

    private NavMeshAgent agent;
    [SerializeField]private GameObject projectileSpawn;
    Transform target;

    #region Setup
    #endregion


    public virtual void Init()
    {
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("Enemy Init");
    }
    private void Awake()
    {
        Init();
    }

    public virtual void Update()
    {
        Movement();
        if (Input.GetKeyDown("f"))
            Attack();
    }

    public virtual void Movement()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRange)
        {
            agent.SetDestination(target.position);
            Debug.Log("Enemy moves");
        }
    }

    public virtual void Attack()
    {
        Debug.Log("Enemy Attacks");
        Projectile projectile = Instantiate(projectilePrefab.GetComponent<Projectile>(),projectileSpawn.transform.position, transform.rotation);
        if(projectile.Attack(transform))
        {
            IBaseStats interfaceHit = player.transform.GetComponent<IBaseStats>();
            if (interfaceHit != null)
            {
                interfaceHit.TakeDamage(damage);
            }
        }
    }


}
