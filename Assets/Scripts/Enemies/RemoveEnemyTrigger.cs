using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEnemyTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // remove the enemy
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"){
            Destroy(other.gameObject);
        }
    }
}
