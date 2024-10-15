using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireForword_S : MonoBehaviour
{
    [SerializeField] float speed;
    private Rigidbody rb;
    [SerializeField] private GameObject projectile, destroyEffect;
    private Transform shotPoint; // assuming shoting point is the child object of my object and it should be at 0th index
    [SerializeField] private int lifes = 1;
    private float timeBtwShots;
    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private AudioSource deadAudioSource;
    [SerializeField] private bool isLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shotPoint = transform.GetChild(0);
        if(isLeft){
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwShots <= 0){
            fireProjectile();
            timeBtwShots = startTimeBtwShots;
        }else{
            timeBtwShots -= Time.deltaTime;
        }
    }
    // shoot the bullet to shoot point right directiona and it's angle
    void fireProjectile() {
        if(projectile == null || shotPoint == null){
            print("Problem in EnemyFireForword_S Object shoting projectile");
            return;
        }

        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
    // if toches the bullet of the player then play the destroy effect
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            lifes--;
            if(lifes <= 0){
                if(deadAudioSource != null) deadAudioSource.Play();
                SavingScore.Score.playerIncrementScore(10.0f);
                StartCoroutine(playDestroyEffect());
            }
        }
    }
    // create the destroy effect
    IEnumerator playDestroyEffect(){
        var rb = GetComponent<Rigidbody2D>();
        if(rb != null) rb.AddForce(Vector2.up * 300);
        yield return new WaitForSeconds(0.5f);
        if(destroyEffect != null) Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
