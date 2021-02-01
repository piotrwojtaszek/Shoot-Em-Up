using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FinishLane : MonoBehaviour
{
    [SerializeField]
    GameObject canvas;
    float levelTime = 0f;
    [SerializeField]
    TextMeshProUGUI czas;

    void Update()
    {
        levelTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        canvas.SetActive(true);
        czas.text = levelTime.ToString("F2");
        Time.timeScale = 0f;

    }

}
