using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemyMovement : MonoBehaviour
{
    public LayerMask playerLayerMask;
    Rigidbody rb;
    public float speed;
    public float viewRange = 10f;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] target = Physics.OverlapSphere(transform.position, viewRange, playerLayerMask);
        if(target.Length>0)
        {
            Vector3 targetTransform = new Vector3(target[0].transform.position.x,transform.position.y,target[0].transform.position.z);
            transform.LookAt(targetTransform);

            rb.velocity = transform.forward * speed;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
            
    }
}
