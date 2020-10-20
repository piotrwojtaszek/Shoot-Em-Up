using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 1f;
    [SerializeField]
    private float m_jumpForce = 100f;
    private Rigidbody m_rb;

    private float x, z;
    private bool jumping;

    // Start is called before the first frame update
    void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Inputs();
    }
    // Update is called once per frame
    void FixedUpdate()
    {


        Move();
    }

    private void Inputs()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jumping = Input.GetButtonDown("Jump");


    }

    private void Move()
    {
        // m_rb.AddForce(Vector3.down * 2f);
        Vector3 velocity = transform.right * x * m_speed + transform.forward * z * m_speed;

        m_rb.velocity = new Vector3(velocity.x, m_rb.velocity.y, velocity.z);
        //m_rb.AddForce(transform.right * x * Time.deltaTime * m_speed);

        if (jumping)
            m_rb.AddForce(transform.up * m_jumpForce);
    }
}
