using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float start = 2.8f, endOffset;
    void Start()
    {

    }

    // cmove camera postion along the player 
    void Update()
    {
        if(player == null) return;
        if(player.position.x < endPoint.position.x + endOffset){
            if (start < player.position.x)
            {
                start = player.position.x;
            }
            transform.position = new Vector3(start, transform.position.y, transform.position.z);
        }

    }
}