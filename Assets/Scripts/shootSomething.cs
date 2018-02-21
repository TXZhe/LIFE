using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootSomething : MonoBehaviour {

    public GameObject projectile;
    public Vector2 velocity;
    bool canShoot = true;
    public Vector2 offset = new Vector2(-1f, 0.65f);
    public float cooldown = 1f;

    public Camera_follow camerafollow;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Z) && canShoot && !GetComponent<Animator>().GetBool("GameOver"))
        {
            GameObject bullet = (GameObject) Instantiate(projectile, (Vector2)transform.position + new Vector2(offset.x * transform.localScale.x, offset.y), Quaternion.identity);
            GetComponent<Animator>().SetTrigger("Shoot");
        }
	}

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

}
