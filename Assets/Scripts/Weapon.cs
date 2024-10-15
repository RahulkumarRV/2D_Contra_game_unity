using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wapen : MonoBehaviour
{
    public float offset;
    private bool fireUp = false;
    private bool fireHorizontal = false;
    private bool fireUpRight = false;
    private bool fireDownRight = false;
    [SerializeField] private int lifes = 3;
    [SerializeField] private AudioSource deadAudioSource;
    public GameObject projectile;
    public Transform shotPoint;
    private Animator anim;
    private GameObject health;
    private float timeBtwShots; // take care the time between fires
    public float startTimeBtwShots; // time between each fire should be
    private int gunPower = 1, previousGunPower = 1;
    private float bulletSpeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = GameObject.Find("Health");
    }

    // controlle the player shooting machanisume and powers
    void Update()
    {
        // fire the bullets on the key F pressed
        if(timeBtwShots <= 0){
            Quaternion desiredRotation = checkFire();
            if(isFireing()){
                timeBtwShots = startTimeBtwShots;
                if(gunPower == 1){
                    Instantiate(projectile, shotPoint.position, desiredRotation).GetComponent<Projectile>().speed = bulletSpeed;
                }
                else if(gunPower == 2){
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, 4f)).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, -4f)).GetComponent<Projectile>().speed = bulletSpeed;
                }
                else if(gunPower == 3){
                    print(gunPower);
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, 8f)).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, 4f)).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, -4f)).GetComponent<Projectile>().speed = bulletSpeed;
                    Instantiate(projectile, shotPoint.position, desiredRotation * Quaternion.Euler(0f, 0f, -8f)).GetComponent<Projectile>().speed = bulletSpeed;
                }
                else if(gunPower == 4){
                    StartCoroutine(updateIfPowerIs4(10.0f));
                }
                else if(gunPower == 5){
                    PlayerPrefs.SetFloat("currentScore", PlayerPrefs.GetFloat("currentScore") + 50);
                }
            }  
        }else {
            timeBtwShots -= Time.deltaTime;
        }

        anim.SetBool("fireup", fireUp);
        anim.SetBool("fireupright", fireUpRight);
        anim.SetBool("firedownright", fireDownRight);
        anim.SetBool("firehorizontal", fireHorizontal);
    }
    // check the direction to the fire
    private Quaternion checkFire(){
        var rotationAngle = shotPoint.rotation;
        
        if(Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.F)){
            updateState("UP");
            // rotationAngle = Quaternion.Euler(new Vector3(0, 0, 90));
        }else if(Input.GetKey(KeyCode.F) ){
            updateState("HORIZONTAL");
            rotationAngle =  transform.rotation;
        }else if(Input.GetKey(KeyCode.R) ){
            updateState("UPRIGHT");
            // rotationAngle = Quaternion.Euler(new Vector3(0, 36, 36));
        }else if(Input.GetKey(KeyCode.G)){
            updateState("DOWNRIGHT");
            // rotationAngle = Quaternion.Euler(new Vector3(0, -45, -36));
        }else updateState("ALL_FALSE");
        
        return rotationAngle;
    }
    // update the player movement state
    private void updateState(string state){
        fireHorizontal = state == "HORIZONTAL";
        fireUp = state == "UP";
        fireDownRight = state == "DOWNRIGHT";
        fireUpRight = state == "UPRIGHT";
    }
    // check if the playe is shooting
    private bool isFireing() {
        return fireDownRight || fireHorizontal || fireUp || fireUpRight;
    }

    // get the gunpower 
    public int getGunPower(){
        return gunPower;
    }
    // set the gunpower
    public void setGunPower(int _gunPower){
        if(previousGunPower != 4) previousGunPower = gunPower;
        gunPower = _gunPower;
    }
    // if toches the enemey of it's bullet the destory
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy"){
            if(gunPower != 4){
                deadThePlayer();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy"){
            if(gunPower != 4){
                deadThePlayer();
            }
        }
    }

    IEnumerator updateIfPowerIs4(float time){
        yield return new WaitForSeconds(time);
        setGunPower(previousGunPower);
    }
    // playe the dead animation
    public void deadThePlayer(){
        if(anim == null) return;
        lifes--;
        if(deadAudioSource != null){
            deadAudioSource.Play();
        }
        if(lifes <= 0){
            // call game over funtion here 
            anim.SetBool("dead", true);
            SceneManager.LoadScene("GameOverScene");
            
        }else{
            if(health != null) {
                var hScript = health.GetComponent<Health>();
                hScript.Damage(10f);
            }

            StartCoroutine(playerComeFromAir(1.0f));
        }
    }

    IEnumerator playerComeFromAir(float time){
        transform.position = new Vector3(transform.position.x + 1, transform.position.y + 30f, transform.position.z);
        yield return new WaitForSeconds(time);
    }
}
