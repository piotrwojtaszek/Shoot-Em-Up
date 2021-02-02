using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class FinishCanvas : MonoBehaviour
{
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI newBest;
    public TextMeshProUGUI toBeatText;
    public TextMeshProUGUI toBeatTime;
    public float currentTime;

    private void Start()
    {
        float bestTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex.ToString());
        currentTimeText.text = currentTime.ToString("F2");
        if (currentTime < bestTime || bestTime == 0f)
        {
            newBest.gameObject.SetActive(true);
            toBeatText.gameObject.SetActive(false);
            toBeatTime.gameObject.SetActive(false);
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().buildIndex.ToString(), currentTime);
        }
        else if (currentTime > bestTime && bestTime != 0f)
        {
            newBest.gameObject.SetActive(false);
            toBeatText.gameObject.SetActive(true);
            toBeatTime.gameObject.SetActive(true);
            toBeatTime.text = bestTime.ToString("F2");

        }
        else
        {
            toBeatText.gameObject.SetActive(false);
            toBeatTime.gameObject.SetActive(false);
            newBest.gameObject.SetActive(false);
        }
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
