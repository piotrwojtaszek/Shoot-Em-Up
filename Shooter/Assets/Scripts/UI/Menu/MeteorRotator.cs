using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRotator : MonoBehaviour
{
    public Transform axis;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(axis.position, transform.up, speed*Time.deltaTime);
    }
}
