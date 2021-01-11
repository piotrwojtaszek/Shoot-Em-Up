using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController2 : MonoBehaviour
{

    private bool isChasing;
    public float distanceToChase = 30f, distanceToLose = 35f, distanceToStop = 5f, distanceToFire = 5f;
    private Vector3 targetPoint, startPoint;

    public NavMeshAgent agent;
    public float keepChasnigTime = 5f;
    private float chaseConuter;

    
    public GameObject bullet;
    public Transform firePoint;

    public float fireRate;
    private float fireCount;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        targetPoint = PlayerMovement.instance.transform.position;
        targetPoint.y = transform.position.y;
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < distanceToChase)
            {
                isChasing = true;

                fireCount = 1f;
  
            }

            if (chaseConuter > 0)
            {
                chaseConuter -= Time.deltaTime;

                if (chaseConuter <= 0)
                {
                    agent.destination = startPoint;
                }
            }

        }
        else
        {

            //transform.LookAt(targetPoint);

            //RB.velocity = transform.forward * moveSpeed;
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {

                agent.destination = targetPoint;
            }
            else 
            {

                agent.destination = transform.position;

            }
            if(Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                isChasing = false;

                chaseConuter = keepChasnigTime;
            }


            fireCount -= Time.deltaTime;
            if(fireCount <= 0)
            {

                fireCount = fireRate;
                if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) < distanceToFire)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);

                }
                //Instantiate(bullet, firePoint.position, firePoint.rotation);

            }

        }
    }

}
