using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlackController : MonoBehaviour
{
    private Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }
    // if object hit by the bullet then destory this object
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            SavingScore.Score.playerIncrementScore(10.0f);
            Destroy(gameObject);
        }
    }

}
