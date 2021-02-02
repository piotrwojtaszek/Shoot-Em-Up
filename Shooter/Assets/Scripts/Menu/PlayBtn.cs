using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBtn : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Play(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteAll();
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }
}
