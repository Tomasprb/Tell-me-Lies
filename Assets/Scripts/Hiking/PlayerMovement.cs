using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed;
    private float MovementDirection;
    public float JumpForce;
    public float checkRadius;
    private Rigidbody2D rb;
    private bool FacingRight=true;
    private bool Jumping=false;
    private bool isGrounded=true;
    public Transform cielingCheck;
    public Transform groundCheck; 
    public LayerMask GroundObject;
    private Animator anim;

    private void Awake()
    {
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponent<Animator>();
    }
    void Update()
    {
        MovementDirection=Input.GetAxis("Horizontal");
        anim.SetBool("isIdle",true);
        
        if(MovementDirection!=0)
        {
            anim.SetBool("isIdle",false);
        }
        if(Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            Jumping=true;
        }
        if(MovementDirection>0&&!FacingRight)
        {
            FlipCharacter();
        }
        else if(MovementDirection<0&&FacingRight)
        {
            FlipCharacter();
        }
        if(isGrounded)
        {
            anim.SetBool("isJumping",false);
        }
        else if(!isGrounded)
        {
            anim.SetBool("isJumping",true);
        }

        rb.velocity=new Vector2(MovementDirection*MoveSpeed, rb.velocity.y);
       
        if(Jumping)
        {
            rb.AddForce(new Vector2(0f,JumpForce));
        }
        Jumping=false;
    }
    private void FixedUpdate()
    {
        isGrounded=Physics2D.OverlapCircle(groundCheck.position, checkRadius, GroundObject);
    }
    

    private void FlipCharacter()
    {
        FacingRight=!FacingRight;
        transform.Rotate(0f,180f,0f);
    }
}
