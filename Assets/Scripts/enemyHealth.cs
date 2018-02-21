using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour {

    public float enemyMaxHealth;
    public bool dead = false;

    float currentHealth;

    public Animator anim;
    public Camera_follow camerafollow;
    // Use this for initialization
    void Start()
    {
        currentHealth = enemyMaxHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            makeDead();
        }
    }

    void makeDead()
    {
        dead = true;
        transform.gameObject.tag = "Untagged";

        
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 800f));
        anim.SetTrigger("DeadT");
        camerafollow.ShakeCamera(0.3f, 0.6f);
        //Destroy(gameObject);
    }
}

