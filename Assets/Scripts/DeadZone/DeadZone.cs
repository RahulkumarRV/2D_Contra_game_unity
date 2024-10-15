using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // if player collided with the dead zone the damage the player
    void OnCollisionEnter2D(Collision2D other){
            print(other.gameObject.tag + " hit the dead zone");
        if(other.gameObject.tag == "Player"){
            var player = GameObject.Find("Player");
            if(player != null){
                player.GetComponent<Wapen>().deadThePlayer();
            }
        }
    }
}
