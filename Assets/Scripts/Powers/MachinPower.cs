using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controlle the machine power and update the player power
public class MachinPower : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpPower;
    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private char PowerType;
    [SerializeField] AudioSource releaseEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){
            if(rb != null){
                if(destroyEffect != null) {
                    Instantiate(destroyEffect, transform.position, Quaternion.identity).transform.localScale *= 2f;;
                    
                }
                rb.AddForce(Vector2.up * jumpPower);
                rb.gravityScale = 2f;
                if(releaseEffect != null) releaseEffect.Play();
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            // add action here what should be happend after the player collect the power
            var Player = GameObject.Find("Player");
            if(Player != null){
                var playerScript = Player.GetComponent<Wapen>();
                switch (PowerType)
                {
                    case 'M':
                        playerScript.setGunPower(2);
                        break;
                    case 'S':
                        playerScript.setGunPower(4);
                        break;
                    case 'R':
                        break;
                    case 'B':
                        playerScript.setGunPower(3);
                        break;
                    case 'F':
                        playerScript.setGunPower(5);
                        break;
                    default:
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    
}
