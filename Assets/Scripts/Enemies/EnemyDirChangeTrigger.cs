using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirChangeTrigger : MonoBehaviour
{
    // if touche the enemy flip the direction
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            other.gameObject.GetComponent<EnemyLRJ>().flipDirection();
        }
    }
}
