using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public float positionX = -10.0f;
    public float positionY = 7.25f;
    public float positionZ = -10.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col)
    {
        col.transform.position = new Vector3(positionX, positionY, positionZ);
    }
}
