using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody theRB;
    private bool chasing;
    private float distanceToChase = 10f, distanceToLose = 15f;
    private Vector3 target;

    public NavMeshAgent agent;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = PlayerMovementController.instance.transform.position;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, PlayerMovementController.instance.transform.position) < distanceToChase)
            {
                chasing = true;
            }


        }
        else
        {


            //transform.LookAt(PlayerMovementController.instance.transform.position);

            //theRB.velocity = transform.forward * moveSpeed;

            agent.destination = target;


            if (Vector3.Distance(transform.position, PlayerMovementController.instance.transform.position) > distanceToLose)
            {
                chasing = false;
            }

        }
    }
}
