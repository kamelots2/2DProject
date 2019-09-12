using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    enum State
    {
        Normal,
        Jump,
        Croush,

    }
	public CharacterController2D controller;
    public Animator animator;
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool m_Jump = false;
	bool m_Crouch = false;
    private State m_State = State.Normal;
    bool m_Stop = false;
    public bool m_Hurt = false;
    float m_InvTime;

    // Update is called once per frame
    void Update () {
        if (m_Stop)
            return;
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
		if (Input.GetButtonDown("Jump")&&!animator.GetBool("IsCrouch"))
		{
            m_State = State.Jump;
            m_Jump = true;
            animator.SetBool("IsJump", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            m_State = State.Croush;
            m_Crouch = true;
            animator.SetBool("IsCrouch", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            m_Crouch = false;
        }
    }

    public void OnLanding()
    {
        m_State = State.Normal;
        animator.SetBool("IsJump", false);
    }

    public void OnCrouching(bool isCrouch)
    {
        m_State = State.Normal;
        animator.SetBool("IsCrouch", isCrouch);
    }

    void FixedUpdate ()
	{
        if (m_Stop)
            return;
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump);
		m_Jump = false;
        if(m_Hurt)
        {
            m_InvTime -= Time.fixedDeltaTime;
            if(m_InvTime <= 0)
            {
                m_Hurt = false;
                animator.SetBool("IsHurt", m_Hurt);
            }
        }
	}

    void Fight()
    {
        m_Stop = true;
    }

    public bool IsJump()
    {
        return m_State == State.Jump;
    }

    public void Hurt()
    {
        if(!m_Hurt)
        {
            m_Hurt = true;
            m_InvTime = 1.0f;
            animator.SetBool("IsHurt", m_Hurt);
            GameObject.Find("GameMgr").GetComponent<GameManager>().UpdateCherry(-1);
        }      
    }
}
