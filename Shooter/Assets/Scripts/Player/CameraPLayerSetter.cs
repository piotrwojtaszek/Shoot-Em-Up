using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPLayerSetter : MonoBehaviour
{
    [SerializeField]
    Transform follow;
    [SerializeField]
    Transform lookAt;
    private void Awake()
    {
        
    }

    public void Respawn(Transform player)
    {
        follow = player;
        lookAt = player;
    }
}
