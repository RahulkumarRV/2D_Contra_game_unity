using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controlle the movment of the bullet of both player and enemies 
public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float direction;
    // Start is called before the first frame update
    [SerializeField] AudioSource shootAudioSource;
    public GameObject destroyEffect;
    private Rigidbody2D rb;

    void Start()
    {
        Invoke("DistroyProjectile", lifeTime);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        if(shootAudioSource != null) shootAudioSource.Play();
    }

    void DistroyProjectile() {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag != "Ground"){
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other){

        if((transform.gameObject.tag == "EnemyBullet" && other.gameObject.tag != "Enemy" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "Bullet" && other.gameObject.tag != "Power")
            || (transform.gameObject.tag == "Bullet" && other.gameObject.tag != "Player" && other.gameObject.tag != "Ground" && other.gameObject.tag != "Trigger" && other.gameObject.tag != "EnemyBullet" && other.gameObject.tag != "Bullet")
        ){
            Destroy(gameObject);
        }
    }

    

}
