using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnImplementation : MonoBehaviour
{
    public void ResumeBtn(GameObject canvass)
    {
        canvass.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MenuBtn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
