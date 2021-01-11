using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChanger : MonoBehaviour
{
    Renderer rend;
    public Gradient gradient;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        int random = Random.Range(100, 700);
        Vector4 scale = new Vector4(transform.localScale.x * 3f, transform.localScale.y * 3f, transform.localScale.z * 3f, 4f);
        rend.material.SetVector("_Scale", scale);
        rend.material.SetInt("_Seed", random);
        Color randColor = gradient.Evaluate(Random.Range(0f, 1f));
        rend.material.SetColor("_Color", randColor);
    }
}
