using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (player != null)
            transform.position = player.transform.position;
    }
}
