using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumpTrigger : MonoBehaviour
{
    // if toches the enemy the make enemy to jump
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyLRJ>().jump();

        }
    }
}
