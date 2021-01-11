using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{                                 

    // Use this for initialization
    void Start()
    {
        // As needed
    }

    // Update is called once per frame
    void Update()
    {
        // As needed
    }

    private void OnTriggerEnter(Collider col)
    {
        col.transform.position = new Vector3 (Random.Range(-50.0f,50.0f),col.transform.position.y, Random.Range(-50.0f, 50.0f));
    }
}

