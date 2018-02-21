using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_script : MonoBehaviour {

    public Transform[] spots;
    public float speed;
    GameObject player;
    Vector3 playerpos;

    public Animator anim;
    Rigidbody2D rigid;

    Vector2 newpos;
    Vector2 oldpos;
    float velocityX;
    float velocityY;

    public Camera_follow camerafollow;

    public bool grounded = true;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public GameControl gamecontrol;

    public enemyHealth enemyhealth;

    bool facingRight = true;
    bool dead = false;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Boss");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        
    }
	
	// Update is called once per frame
	void FixedUpdate() {
        //anim.SetFloat("Speed", rigid.velocity.magnitude);
        newpos = transform.position;
        var media = (newpos - oldpos);
        velocityX = media.x / Time.deltaTime;
        velocityY = media.y / Time.deltaTime;
        oldpos = newpos;

        anim.SetFloat("Speed", velocityX);
        anim.SetFloat("vSpeed", velocityY);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Grounded", grounded);

        dead = enemyhealth.dead;
        anim.SetBool("Dead", dead);
    }


    IEnumerator Boss()
    {
        //first
        while (!gamecontrol.gameOver && !dead)
        {
            //first - go to 1
            //rigid.isKinematic = false;
            Flip(transform.position.x,spots[0].position.x);
            while (transform.position.x != spots[0].position.x && !dead)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(spots[0].position.x, transform.position.y), speed);
                yield return null;
            }
            if (dead)
                break;
            //transform.localScale = new Vector2(-3, 3);
            yield return new WaitForSeconds(1f);

            //second - jump up to 2
            rigid.isKinematic = true;
            Flip(transform.position.x, spots[1].position.x);
            while (transform.position.x != spots[1].position.x && !dead)
            {
                transform.position = Vector2.MoveTowards(transform.position, spots[1].position, speed * 4);
                yield return null;
            }
            if (dead)
                break;
            yield return new WaitForSeconds(1f);

            //third - jump to player
            playerpos = player.transform.position;
            Debug.Log("ok");
            rigid.isKinematic = false;
            Flip(transform.position.x, playerpos.x);
            while (transform.position.x != playerpos.x && !dead)
            {
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerpos.x, transform.position.y), speed * 4);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(playerpos.x, playerpos.y+(3.5f)), speed * 6);
                yield return null;
            }
            if (dead)
                break;
            camerafollow.ShakeCamera(0.3f, 0.6f);

            yield return new WaitForSeconds(1f);
        }
        rigid.isKinematic = false;
    }

    void Flip(float a, float b)
    {
        if((a > b && facingRight)|| (a < b && !facingRight))
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
