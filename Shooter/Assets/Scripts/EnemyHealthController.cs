using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class EnemyHealthController : MonoBehaviour
{

    public int currHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DamageEnemy(int damageAmount)
    {

        currHealth -= damageAmount;

        if(currHealth <=0)
        {
            Destroy(gameObject);
        }

    }
}
