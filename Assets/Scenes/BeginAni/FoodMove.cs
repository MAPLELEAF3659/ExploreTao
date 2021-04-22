using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMove : MonoBehaviour
{
    GameObject pot;
    bool isMoving;
    public float smoothTime=1f;

    // Start is called before the first frame update
    void Start()
    {
        pot = GameObject.Find("pot(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(gameObject.transform.position, pot.transform.position) > 0.1f)
                gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, pot.transform.position
                    , smoothTime * Time .deltaTime );
            else
                isMoving = false;
        }
    }

    public void Move()
    {
        isMoving = true;
    }
}
