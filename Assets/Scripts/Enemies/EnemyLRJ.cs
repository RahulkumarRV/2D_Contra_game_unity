using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLRJ : MonoBehaviour
{
    [SerializeField] private float speed, jumpPower, startTimeBtwShots;
    [SerializeField] private GameObject projectile;
    [SerializeField] private bool leftDirection = true; // if false then move to right
    [SerializeField] private LayerMask groundLayer; // to find the collision with ground layer, i contain the gound layer as value
    [SerializeField] private LayerMask playerLayer; // to find the collision with player layer, i contain the gound layer as value
    [SerializeField] private GameObject ground, destroyEffect;
    [SerializeField] private AudioSource deadAudioSource;
    private Transform shotPoint; // assumed first child is the shot point
    private bool isShooting = false, isOnGround;
    private float timeBtwShots = 0;
    private Rigidbody2D rb;
    private int direction = 1;
    private BoxCollider2D boxCollider; // reference to the box collider componenet
    private Animator anim;
    // private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        shotPoint = transform.GetChild(0);
        rb = GetComponent<Rigidbody2D>();
        boxCollider =  GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        // Player = GameObject.Find("Player");

        if(!leftDirection){
            direction = -1;
            transform.Rotate(0, 180, 0);
        }
        
    }

    // Update is called once per frame
    void Update(){
        if(anim == null) return;
        if(ground != null){
            var col = ground.GetComponent<EdgeCollider2D>();
            if(col != null && !col.enabled){
                rb.gravityScale = 0;
            }else{
                rb.gravityScale = 1;
            }
        }
        if(isGrounded() && !isOnGround){
            anim.SetTrigger("run");
            isOnGround = true;
        }

        
        // fire the bullets if the player is near to me
        if(isPlayerNewMe() && isGrounded()){
            
            anim.SetTrigger("shoot");
            if(timeBtwShots <=0){
                timeBtwShots = startTimeBtwShots;
                fireProjectile();
            }else{
                timeBtwShots -= Time.deltaTime;
            }
            isShooting = true;
        }else{
            isShooting = false;
            anim.SetTrigger("run");
        }

        // Move the object horizontally
        if(!isShooting){
            rb.velocity = new Vector2( direction * speed, rb.velocity.y);
        }

        
    }   

    // fire the projectile from the shotpint postion and at angel mentain by the shotpoint
    void fireProjectile() {
        if(projectile == null || shotPoint == null){
            print("Problem in EnemyFireForword_S Object shoting projectile");
            return;
        }
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }



    void shoot(){

        if(isPlayerNewMe() && isGrounded()){
            if(timeBtwShots <= 0){
                isShooting = true;
                anim.SetTrigger("shoot");
                fireProjectile();
                timeBtwShots = startTimeBtwShots;
            }else{
                timeBtwShots -= Time.deltaTime;
            }
        }else{
            anim.SetTrigger("run");
            isShooting = false;
        }
    }

    // check if the object is on the ground
    private bool isGrounded(){
        if(groundLayer == 0) return false;

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
        return hit.collider != null;
    }

    private bool isPlayerNewMe(){
        if(playerLayer == 0) return false;

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.right * direction, 10f, playerLayer);

        return hit.collider != null;
    }

    // add an EnemyJumpTrigger object so i can jump ofter collision triggers
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "EnemyJumpTrigger"){
            jump();
        }

        if(other.gameObject.tag == "Bullet"){
            if(deadAudioSource != null) deadAudioSource.Play();
            StartCoroutine(playDestroyEffect());
        }
    }

    IEnumerator playDestroyEffect(){
        jump();
        // var dataManagment = new DataManagment();
        // if(dataManagment != null){
        //     dataManagment.playerIncrementScore(10f);
        // }
        yield return new WaitForSeconds(0.5f);
        if(destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SavingScore.Score.playerIncrementScore(10.0f);
        // Increase the player Score
    }

    public void jump(){
        anim.SetTrigger("jump");
        rb.AddForce(Vector2.up * jumpPower);
        isOnGround = false;
    }

    public void flipDirection(){
        leftDirection = !leftDirection;

        if(!leftDirection){
            direction = -1;
            transform.Rotate(0, 180, 0);
        }else{
            direction = 1;
            transform.Rotate(0, -180, 0);
        }
    }
}
