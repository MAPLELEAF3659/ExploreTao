using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    Vector3 newPos;
    public float speed = 1.5f;
    public float rotateSpeed = 5.0f;
    public float minX = -5, maxX = 5, minY = -2, maxY = 10, minZ = 10, maxZ = 20;
    public bool isMoving;
    public GameObject bird;

    void Start()
    {
        PositionChange();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, newPos) < 1f)
            PositionChange();
    }

    void PositionChange()
    {
        newPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        bird.transform.LookAt(newPos);
    }
}
