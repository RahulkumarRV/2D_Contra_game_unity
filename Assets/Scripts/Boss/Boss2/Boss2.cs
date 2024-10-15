using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    private Animator anim; // animatation controller 
    private bool runing;    // check object is running
    private Rigidbody2D rb; // reference to rigid body of this object
    [SerializeField] private float speed, timeBtnShoot;
    [SerializeField] private int direction = -1; // check the direction of this object
    private GameObject Player; // reference to the player in level
    [SerializeField] private GameObject bullet, destroyEffect, ground, triggerToEndLevel; 
    private float shootTime; // time between shoot two bullets
    private Transform shootPoint; // reference to the the shooting point
    [SerializeField] private int lifes = 5; // lifes of this object
    [SerializeField] private AudioSource shootAudioEffect, deadAudioEffect;

    // Start is called before the first frame update and store the reference to components
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        shootPoint = transform.GetChild(0);
        if(anim != null) anim.SetTrigger("runing");
        runing = true;
    }

    void Update()
    {
        float differece = 0;
        // Controller(1.0f);
        // play the animation according to player position
        if(Player != null){
            differece = Player.transform.position.x - transform.position.x;
            direction = differece < 0 ? -1 : 1;
            if(Mathf.Abs(differece) < 3){
                anim.SetTrigger("idle");
                runing = false;
            }else{
                anim.SetTrigger("runing");
                runing = true;
            }
        }
        // if player move down the my ground the i need to stay on this ground
        if(ground != null){
            var col = ground.GetComponent<EdgeCollider2D>();
            if(col != null && !col.enabled){
                rb.gravityScale = 0;
            }else{
                rb.gravityScale = 1;
            }
        }
        // if player move left and right from me then i have to change my direction toword the player
        if(differece < 0){
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }else{
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        // if player is far away form me the run towords the player other wise stay and shoot bullets
        if(runing){
            rb.velocity = new Vector2( direction * speed, rb.velocity.y);
            
            // transform.localScale = new Vector2(transform.localScale.x * direction, transform.localScale.y);
        }
        else{
            if(shootTime <= 0){
                if(shootPoint != null && Player != null){
                    var direction = Player.transform.position - shootPoint.position;
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, direction);
                    // Instantiate(bullet, shootPoint.position , shootPoint.rotation * Quaternion.Euler(0f, 0f, 4f));
                    Instantiate(bullet, shootPoint.position , shootPoint.rotation);
                    if(shootAudioEffect != null) shootAudioEffect.Play();
                }
                shootTime = timeBtnShoot;
            }else{
                shootTime -= Time.deltaTime;
            }

        }
    }

    // if player collides with me then damage the player
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name == "Player"){
            var playerScript = other.gameObject.GetComponent<Wapen>();
            playerScript.deadThePlayer();
        }
    }

    // access the direction object 
    public int getDirection(){
        return direction;
    }

    // set the direction object
    public void setDirection(int _direction){
        direction = _direction;
    }

    void Controller(float time){
        var timer = time;
        if(timer <= 0){
            if(Player != null){
                float differece = Player.transform.position.x - transform.position.x;
                direction = differece < 0 ? -1 : 1;
                if(Mathf.Abs(differece) < 5){
                    print("boss 2 shooting");
                }
            }
            timer = time;
        }else{
            timer -= Time.deltaTime;
        }
    }

    // if player bullet hit boss the it's life decrease by 1 each time
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            lifes--;
            if(lifes <= 0){
                if(deadAudioEffect != null) deadAudioEffect.Play();
                StartCoroutine(playDestroyEffect());
            }
        }
    }

    // play the destroy effect 
    IEnumerator playDestroyEffect(){
        if(rb != null) rb.AddForce(Vector2.up * 300);
        yield return new WaitForSeconds(0.5f);
        if(destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        var gate = GameObject.Find("bossGate");
        if(gate != null && triggerToEndLevel != null && Player != null){
            gate.GetComponent<Animator>().SetTrigger("finalstate");
            Player.transform.position = triggerToEndLevel.transform.position;
        }
    }

}
