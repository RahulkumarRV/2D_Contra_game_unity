using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireDigonalCanFlip : MonoBehaviour
{
    private Transform shotPoint; // assumed to be the first children is the shotpoint object
    [SerializeField] private GameObject projectile, destroyEffect, ground;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots, speed;
    private GameObject Player;
    private Animator anim;
    [SerializeField] private bool isHiddenEnemy = false; // check that this script is using for any hiddent playe enemy
    private float animationTransitionDuration = 1f; // set then time to switch between animations of hidden behaviour
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource deadAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        shotPoint = transform.GetChild(0);
        Player = GameObject.FindWithTag("Player");
        animationTransitionDuration = startTimeBtwShots / 3;
        rb = GetComponent<Rigidbody2D>();
        if(isHiddenEnemy){
            anim = GetComponent<Animator>();   
        }
    }

    // Update is called once per frame
    void Update()
    {
        // update gravity if player go down the platform
        if(ground != null){
            var col = ground.GetComponent<EdgeCollider2D>();
            if(col != null && !col.enabled){
                rb.gravityScale = 0;
            }else{
                rb.gravityScale = 1;
            }
        }
        // shot the bullet
        if(timeBtwShots <= 0){
            flip();
            if (isHiddenEnemy) StartCoroutine(startHiddenEnemyBehavior(animationTransitionDuration));
            else fireProjectile();
            timeBtwShots = startTimeBtwShots;
        }
        else timeBtwShots -= Time.deltaTime;
        
    }
    // change the direction of the object
    void flip(){

        if(Player == null){
            print("Player not found in the EnemyFireDigonalCanFlip");
            return;
        }
        float relativePosition = Player.transform.position.x - transform.position.x;
        if(relativePosition < 0){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }else{
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        
    }
    // shoot the object at shoot point angle
    void fireProjectile(){
        if(projectile == null || shotPoint == null){
            print("Problem in EnemyFireDigonalCanFlip Object shoting projectile");
            return;
        }

        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
    // play the hidden player animation 
    IEnumerator startHiddenEnemyBehavior(float timeDelay){
        anim.SetTrigger("comeout");
        yield return new WaitForSeconds(timeDelay);
        anim.SetTrigger("shoot");
        yield return new WaitForSeconds(timeDelay);
        anim.SetTrigger("hidden");
        yield return new WaitForSeconds(timeDelay);
    }
    // if collided with the bullet then destory
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            if(deadAudioSource != null) deadAudioSource.Play();
            SavingScore.Score.playerIncrementScore(10.0f);
            StartCoroutine(playDestroyEffect());
        }
    }
    // play the destroy effect
    IEnumerator playDestroyEffect(){
        var rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.AddForce(Vector2.up * 300);
        yield return new WaitForSeconds(0.5f);
        if(destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
