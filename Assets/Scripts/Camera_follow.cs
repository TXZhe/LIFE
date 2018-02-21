using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour {

    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float yMin;

    private Transform target;

    public float Xoffset;
    public float Yoffset;

    public float shakeTimer;
    public float shakeAmount;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
		
	}
	
	// Update is called once per frame
	/*void LateUpdate () {
        transform.position = new Vector3(Mathf.Clamp(target.position.x + Xoffset, xMin, xMax), Mathf.Clamp(target.position.y + Yoffset, yMin, yMax), transform.position.z);
	}*/

    void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(target.position.x + Xoffset, xMin, xMax), Mathf.Clamp(target.position.y + Yoffset, yMin, yMax), transform.position.z);
        if (shakeTimer >= 0)
        {
            Vector2 ShakePos = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + ShakePos.x, transform.position.y + ShakePos.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
    }

    public void ShakeCamera(float shakePwr, float shakeDur)
    {
        shakeAmount = shakePwr;
        shakeTimer = shakeDur;
    }
}
