using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    // Start is called before the first frame update
    public float m_Speed = 1;
    public float m_Far = 100;
    private float m_NowFar = 0;
    private bool m_FacingRight = false;
    private Rigidbody2D m_Rigidbody2D;
    private Vector3 m_Defaultpos;
    Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    public GameObject gameObj;
    void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Defaultpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float nowSpeed;
        if(m_FacingRight)
        {
            nowSpeed = 1.0f;
        }
        else
        {
            nowSpeed = -1.0f;
        }
        Vector3 targetVelocity = new Vector2(nowSpeed * m_Speed, 0);
        m_NowFar += nowSpeed;
        if(m_NowFar >= m_Defaultpos.x + m_Far || m_NowFar <= m_Defaultpos.x - m_Far)
        {
            Flip();
        }
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"&& !collision.gameObject.GetComponent<PlayerMovement>().m_Hurt)
        {
            if(collision.gameObject.GetComponent<CharacterController2D>().IsJump())
            {
                Destroy(gameObject);
                Instantiate(gameObj, transform.position, transform.rotation);
            }else
            {
                collision.gameObject.GetComponent<PlayerMovement>().Hurt();
            }
            collision.gameObject.GetComponent<CharacterController2D>().Hurt();
        }
    }
}
