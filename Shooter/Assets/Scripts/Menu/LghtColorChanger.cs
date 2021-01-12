using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LghtColorChanger : MonoBehaviour
{
    Light swiatelko;
    [SerializeField]
    float hue=0;
    Color rgbColor;
    // Start is called before the first frame update
    void Start()
    {
        swiatelko = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        hue += Time.deltaTime/12f;
        hue %= 1;
        rgbColor = Color.HSVToRGB(hue,1,1);
        
        swiatelko.color = rgbColor;
    }
}
