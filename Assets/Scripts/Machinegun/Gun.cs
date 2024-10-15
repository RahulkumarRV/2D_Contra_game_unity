using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check and update the gun to shoot the player
public class Gun : MonoBehaviour
{
    [SerializeField] float offset;
    [SerializeField] GameObject projectile, destroyEffect;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject Player;
    private float timeBtwShots;
    [SerializeField] float startTimeBtnShots;
    [SerializeField] float playerDistance;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if(Player == null) return;

        float playerDistance = Mathf.Abs(Player.transform.position.x - transform.position.x);
        
        // transform.gameObject.SetActive(playerDistance <= 22);
        if(Player.transform.position.x >= transform.position.x - 14)
            updateTheGun();
        
        if(Player.transform.position.x - transform.position.x > 25){
            print("gun inactive : " + (Player.transform.position.x - transform.position.x));
            transform.gameObject.SetActive(false);
        }

        

    }

    void updateTheGun(){

        if(timeBtwShots <= 0){

        Vector3 difference = Player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            // if(Input.GetKey(KeyCode.M)){
                var bullet = Instantiate(projectile, shotPoint.position, transform.rotation);
                bullet.GetComponent<Projectile>().speed = 5f;
                timeBtwShots = startTimeBtnShots;
            // }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            GameObject effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            effect.transform.localScale = new Vector3(5, 5, 0);
            Destroy(gameObject);
        }
    }
}
