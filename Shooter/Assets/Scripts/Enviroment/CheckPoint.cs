using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject prefab;
    public ParticleSystem particle;
    public float rotateSpeed = 1f;
    private Vector3 rotateAxies;
    Transform player;
    private void Start()
    {
        RandomRotateAxies();

    }
    private void Update()
    {
        transform.Rotate(rotateAxies * Time.deltaTime * rotateSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.points++;
            UICanvas.instance.CreatePanel(prefab, UICanvas.instance.m_popup);
            player = other.transform;
            LeanTween.scale(gameObject, Vector3.zero, .5f).setEase(curve).setOnComplete(DestroyMe);
        }
    }

    private void DestroyMe()
    {
        Instantiate(particle, player);
        Destroy(this.gameObject);
    }
    void RandomRotateAxies()
    {
        rotateAxies = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    }
}
