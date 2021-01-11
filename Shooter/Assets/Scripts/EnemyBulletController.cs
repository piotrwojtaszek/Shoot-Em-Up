using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{


    public float moveSpeed, lifeSpam;
    public Rigidbody theRB;
    public int damage = 1;

    public bool damageEnemy, damagePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;

        lifeSpam -= Time.deltaTime;

        if(lifeSpam <= 0)
        {

            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        if (other.gameObject.tag == "Player" && damagePlayer)

        {
            //PlayerHealthController.instance.DamagePlayer(damage);
            //Debug.Log("Hit Player at" + transform.position);
        }


        Destroy(gameObject);
    }
}
