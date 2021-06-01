using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreGamaManager : MonoBehaviour
{
    public GameObject floor360;
    MeshRenderer storyMesh;
    Material floor1mat, floor2mat;
    public GameObject floor1, floor2;
    
    // Start is called before the first frame update
    void Start()
    {
        storyMesh = floor360.GetComponent<MeshRenderer>();
        storyMesh.material = floor1mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
