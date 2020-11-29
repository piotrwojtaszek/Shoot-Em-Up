using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanRorator : MonoBehaviour
{
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
    }

    public void ChangeMaterial(Material material)
    {
        GetComponentInChildren<Renderer>().material = material;
    }
}
