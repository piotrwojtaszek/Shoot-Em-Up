using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FPS : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ((int)(1f / Time.unscaledDeltaTime)).ToString();
    }
}
