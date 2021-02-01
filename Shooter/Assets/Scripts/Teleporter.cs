using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    Transform destination;
 

    void OnTriggerEnter(Collider col)
    {
        col.transform.position = destination.position;
    }
}
