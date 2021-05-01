using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAni : MonoBehaviour
{
    public GameObject particle;
    public void GenAni()
    {
        Instantiate(particle,transform);
    }
}
