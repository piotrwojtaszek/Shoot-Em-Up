using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    private void Update()
    {
        if (player != null)
            transform.position = player.transform.position + new Vector3(0f,0.5f);
    }
}
