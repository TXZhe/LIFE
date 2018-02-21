using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 10f;
    public float maxDistance = 20f;

    public float weaponDamage = 1f;

    public float explosionTimeUp;

    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-player.localScale.x * speed, GetComponent<Rigidbody2D>().velocity.y);
    }
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x >= maxDistance)
        {
            DestroyProjectile();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player"))
        {
            DestroyProjectile();
            if (other.CompareTag("Enemy"))
            {
                enemyHealth hurtEnemy = other.gameObject.GetComponent<enemyHealth>();
                hurtEnemy.addDamage(weaponDamage);
            }
        }
    }

    void DestroyProjectile()
    {

        GetComponent<Animator>().SetBool("Baozha",true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
        Destroy(gameObject, explosionTimeUp);
    }
}
