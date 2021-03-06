﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class Player_controler : MonoBehaviour {

    public float maxSpeed = 3.5f;
    bool facingRight = false;
    public Animator anim;
	private bool isDead = false;

    public bool grounded = true;
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
        //anim.SetBool("GameOver", false);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("GameOver", isDead);
        if (grounded)
        {
            doubleJump = false;
        }

        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        float move = 0f;
        if (isDead == false)
        {
            if (SceneManager.GetActiveScene().name == "life")
            {
                move = -1;
            }
            else
            {
                move = Input.GetAxis("Horizontal");
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(move));


        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
	}

    void Update()
	{
		if (isDead == false) 
		{
			if ((grounded || !doubleJump) && Input.GetKeyDown (KeyCode.UpArrow)) {
				anim.SetBool ("Grounded", false);
				if (!doubleJump && !grounded) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce * doubleJumpFacter));
					doubleJump = true;
				} else {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
				}
			}

			if (GetComponent<Rigidbody2D> ().velocity.y < 0) {
				GetComponent<Rigidbody2D> ().velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			}
		}
	}

    void Flip(){
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.CompareTag("Death") || other.collider.CompareTag("Enemy"))
        {
			if(isDead == false)
            {
                anim.SetTrigger("GameOverT");
                //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(-transform.localScale.x * 1000f, 1000f));
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 800f));
            }
            isDead = true;
            GameControl.instance.PlayerDied ();
        }
    }

}
