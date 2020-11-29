using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunel : MonoBehaviour
{
    public int numberOfSegments = 1;
    public Material[] materials;
    public GameObject prefab;
    public float rotate;
    public float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        float scale = prefab.transform.localScale.z;
        for (int i = 0; i <= numberOfSegments; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.position += transform.forward + new Vector3(0f, 0f, scale * i);
            obj.transform.Rotate(Vector3.forward * rotate * i);
            obj.GetComponent<fanRorator>().ChangeMaterial(materials[i % 2]);
            obj.GetComponent<fanRorator>().speed = rotateSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
