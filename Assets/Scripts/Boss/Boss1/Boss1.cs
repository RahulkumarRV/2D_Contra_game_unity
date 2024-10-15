using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // i am assuming that the guns are name as follows gun1 gun2 gun3
    // there should be always first child be the shootPoint
    private Transform[] guns; // list of gun points on the boss machine
    private Animator anim; // animation controller for the boss 
    private float timeBtwStartgun; // time taken between two gun shoot's
    private bool isMoving = false; 
    // time wait before shoot the projectile (Bullet)
    [SerializeField] private float startTimeBtwStartgun1 = 1.5f, startTimeBtwStartgun2 = 1.8f;
    [SerializeField] private GameObject Projectile; // the bullet object which shoot by the gun of the boss
    [SerializeField] private GameObject Player; // the reference to the Player object 
    [SerializeField] private AudioSource gunDamagedEffect, bossEndAudioEffect; // audio effect reffence to play on death of the boss 
    
    // Start is called before the first frame update
    void Start()
    {
        int childCount = transform.childCount; // check the number of child guns boss has
        guns = new Transform[childCount]; 
        anim = GetComponent<Animator>();
        Player = GameObject.Find("Player");
        // store the reference to the guns childs
        for(int i=0; i<childCount; i++){
            guns[i] = transform.GetChild(i);
        }
    }

    // if player is present in the level and distance between boss and player
    // is lesser than 20 activate the guns of the boss
    void Update()
    {
        if(Player == null) return;
        if((transform.position.x - Player.GetComponent<PlayerMovment>().transform.position.x) < 20){
            
            shoot(guns[1], startTimeBtwStartgun1);
            shoot(guns[2], startTimeBtwStartgun2);
        }
    }

    // update the animation accroding to a specific gun damaged 
    public void updateAnimationOnGunDamage(string gunName){
        if(gunName == "gun1"){
            if(gunDamagedEffect != null) gunDamagedEffect.Play();
            anim.SetTrigger("gun1damaged");
        }else if(gunName == "gun2"){
            if(gunDamagedEffect != null) gunDamagedEffect.Play();
            anim.SetTrigger("gun2damaged");
        }else if(gunName == "gun3"){
            if(bossEndAudioEffect != null) bossEndAudioEffect.Play();
            anim.SetTrigger("gun3damaged");
        }else{
            
        }
    }

    /*
    if projectile, gun, and gun point is present then fire the bullet from the given gun
    parameter : gun (reference to the gun object), startTimeBtwStartgun (time taken to fire bullet)
    */
    void shoot(Transform gun, float startTimeBtwStartgun){
        if(Projectile == null || gun == null || guns[0] == null) return;

        if(timeBtwStartgun <= 0){
            BoxCollider2D collider = gun.GetComponent<BoxCollider2D>();
            GameObject bullet = Instantiate(Projectile, guns[0].position, gun.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null){
                rb.gravityScale = 1f;
                bullet.transform.localScale *= 1.5f;
                bullet.GetComponent<Projectile>().speed = 8f;
            }

            timeBtwStartgun = startTimeBtwStartgun;
        }
        else{
            timeBtwStartgun -= Time.deltaTime;
        }
    }

    /* update the isMoving state */
    public void setIsMoving(bool status){
        isMoving = status;
    }

    // play the end of this level animation where player move to out of the level and navigate to next level
    void endThisLevel(){
        GameObject player = GameObject.FindWithTag("Player");
        Transform endPathPoint = transform.Find("endPathPoint");
        if(player == null || endPathPoint == null) return;
        isMoving = true;
        var playerScript = player.GetComponent<PlayerMovment>();
        player.transform.position = endPathPoint.transform.position;
        StartCoroutine(MoveRight(player));
    }

    IEnumerator MoveRight(GameObject player)
    {
        while (true)
        {
            // Move the object to the right
            player.transform.Translate(Vector3.right * 1 * Time.deltaTime);
            yield return null;
        }
    }
}
