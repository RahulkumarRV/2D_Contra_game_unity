using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controlle the flying power animation and update the player power accroding 
public class FlyingPowerChild : MonoBehaviour
{
    private FlyingPower ParentScript;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private char PowerType;
    private GameObject player;
    [SerializeField] AudioSource releaseEffect;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent != null) ParentScript = transform.parent.GetComponent<FlyingPower>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        player = GameObject.Find("Player");
    }

    void Update(){
        if(player != null &&  transform.position.x - player.transform.position.x > 25){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){

            if(ParentScript != null && !ParentScript.getIsActive()){
                TriggerActive();
            }
            if(player != null){
            var playerScript = player.GetComponent<Wapen>();
                if(playerScript != null){
                switch (PowerType)
                    {
                        case 'M':
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
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Bullet"){

            TriggerActive();
            if(releaseEffect != null) releaseEffect.Play();
        }
    }

    void TriggerActive(){
        ParentScript.setIsActive(true);
        anim.SetBool("isactive", true);
        ParentScript.setGravity(1f);
        if(capsuleCollider != null){
            // capsuleCollider.isTrigger = true;
        }
    }
}
