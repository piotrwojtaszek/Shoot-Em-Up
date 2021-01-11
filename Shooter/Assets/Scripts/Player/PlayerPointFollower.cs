using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerPointFollower : MonoBehaviour
{
    Transform m_player;
    public Vector3 m_offset;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = m_player.position + m_offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_player)
            transform.position = m_player.position + m_offset;
    }
}
