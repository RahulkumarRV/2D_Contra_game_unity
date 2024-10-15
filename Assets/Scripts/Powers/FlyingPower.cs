using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// controlle the flying power animation
public class FlyingPower : MonoBehaviour
{
    [SerializeField] float speed;
    private bool isActive = false;
    private Rigidbody2D rb;
    private GameObject player;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        // if(player != null && player.transform.position.x - transform.position.x > 25){
        //     Destroy(gameObject);
        // }
        if(!isActive){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    public void setIsActive(bool status){
        isActive = status;
    }

    public bool getIsActive(){
        return isActive;
    }

    public void setGravity(float gravityScale){
        rb.gravityScale = gravityScale;
    }
}
