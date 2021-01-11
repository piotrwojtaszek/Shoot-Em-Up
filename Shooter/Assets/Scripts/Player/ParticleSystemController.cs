using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour
{
    public GameObject m_particlePrefab;
    public MovementController m_movement;
    private bool m_corutine = false;
    public bool m_active = false;
    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (m_active && !m_corutine)
        {
            StartCoroutine(IniciateParticles());
        }
    }

    public IEnumerator IniciateParticles()
    {
        m_corutine = true;
        while (m_active)
        {
            Debug.Log("Partcles");
            Instantiate(m_particlePrefab,transform);


            yield return new WaitForSeconds(0.2f);
        }
        m_corutine = false;
        yield return null;
    }
}
