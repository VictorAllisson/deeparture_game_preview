using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    Animator anim;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool jumping;


    private Rigidbody2D rb;

 
    private bool isGrounded; // variavel que veririfica se personagem esta no chao
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;

    void Start()
    {

        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", moveInput);
        rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);

    }



    //Update is called once per frame
    void Update()
    {
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
            anim.SetBool("in_air", false);
        }
        else
        {
            anim.SetBool("in_air", true);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps >0)
        {
            
            rb.velocity = Vector2.up * jumpForce;
            
            extraJumps--;
            
        }
        
    }
}

