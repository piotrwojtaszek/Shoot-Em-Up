using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int speed;


    public virtual void Init()
    {

    }
    private void Awake()
    {
        Init();
    }

    public virtual void Update()
    {


    }

    public virtual void Movement()
    {

        //float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        //Vector3 direction = player.transform.localPosition - transform.localPosition;



    }

}
