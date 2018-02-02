using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_controler : MonoBehaviour {

    public float maxSpeed = 3.5f;
    bool facingRight = false;
    public Animator anim;

    public bool gameOver = false;

    public bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float jumpForce = 700f;
    float doubleJumpFacter = 0.7f;

    public float fallMultiplier = 2.3f;

    bool doubleJump = false;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);

        if (grounded)
        {
            doubleJump = false;
        }

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetBool("GameOver", gameOver);


        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
	}

    void Update()
    {
        if((grounded|| !doubleJump) && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Grounded", false);
            if(!doubleJump && !grounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce * doubleJumpFacter));
                doubleJump = true;
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
            }
        }

        if(GetComponent<Rigidbody2D>().velocity.y<0)
        {
            GetComponent<Rigidbody2D>().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
