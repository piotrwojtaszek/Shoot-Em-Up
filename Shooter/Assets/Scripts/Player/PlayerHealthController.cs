using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currHealth;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;

        HealthCanvas.instance.healthSlider.maxValue = maxHealth;
        HealthCanvas.instance.healthSlider.value = currHealth;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y<=-30)
        {
            PlayerDead();
        }
    }

    public void DamagePlayer(int DamageAmount)
    {

        currHealth -= DamageAmount;
        //UIController.instance.ShowDmg();


        if (currHealth <= 0)
        {
            gameObject.SetActive(false);

            currHealth = 0;
            PlayerDead();


        }



        HealthCanvas.instance.healthSlider.value = currHealth;


    }

    public void PlayerDead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}
