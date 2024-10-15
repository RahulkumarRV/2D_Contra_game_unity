using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float direction;
    // Start is called before the first frame update

    public GameObject destroyEffect;
    private Rigidbody2D rb;

    // set the reference to the component
    void Start()
    {
        Invoke("DistroyProjectile", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.velocity = transform.right * speed;
    }

    // destroy the bullet and play the destroy effect
    void DistroyProjectile() {
        if(destroyEffect == null) return;
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // destory the object if collided with any object other than ground
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag != "Ground"){
            Destroy(gameObject);
        }
        
    }
    // if it's enemey bullet the destroy if collided with the player
    // if it's player bullet the destroy if collided with the enemy
    void OnTriggerEnter2D(Collider2D other){

        if((transform.gameObject.tag == "EnemyBullet" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "Power")
            || (transform.gameObject.tag == "Bullet" && other.gameObject.tag != "Player" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Bullet")
        ){
            Destroy(gameObject);
        }
    }

    

}
