using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private int countBeforeDestroy = 1; // lifes of the object
    [SerializeField] private GameObject destroyEffect; // destroy the object animation
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if object collided by the bullet then play destroy effect
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Bullet"){
            checkandDestroy();
        }

    }
  // if object touches by the bullet then play destroy effect
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            checkandDestroy();
        }

    }

    // check and run destroy effect
    void checkandDestroy(){
        countBeforeDestroy -= 1;
        if(countBeforeDestroy == 0){
            transform.parent.GetComponent<Boss1>().updateAnimationOnGunDamage(gameObject.name);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
