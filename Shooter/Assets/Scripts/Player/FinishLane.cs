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
    [SerializeField]
    FinishCanvas canvasFinish;
    private void Start()
    {
        StartCoroutine(CheckEnemys());
    }
    void Update()
    {
        levelTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        canvas.SetActive(true);
        canvasFinish.currentTime = levelTime;
        Time.timeScale = 0f;

    }
    IEnumerator CheckEnemys()
    {
        while(true)
        {
            GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
            if(enemys.Length==0)
            {
                break;
            }
            yield return new WaitForSeconds(1f);
        }


        GetComponent<SphereCollider>().enabled = true;
    }
}
