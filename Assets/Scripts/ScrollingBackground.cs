using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public float backgroundSize;
    public float backgroundZ;
    public float paralaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;



    // Use this for initialization
    void Start () {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void ScrollLeft()
    {
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, layers[leftIndex].position.y, backgroundZ);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        layers[leftIndex].position = new Vector3(layers[leftIndex].position.x + backgroundSize, layers[leftIndex].position.y, backgroundZ);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex =  0;
    }

    // Update is called once per frame
    void Update () {
        float deltax = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltax * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;

        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            ScrollLeft();
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            ScrollRight();
    }
}
