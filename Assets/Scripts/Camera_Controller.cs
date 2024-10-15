using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform player;


    private float start;


    void Start()
    {
        start = 11.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (start < player.position.x)
        {
            start = player.position.x;
        }
        transform.position = new Vector3(start, transform.position.y, transform.position.z);


    }
}
