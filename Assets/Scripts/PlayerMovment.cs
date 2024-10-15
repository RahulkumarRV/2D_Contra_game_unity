
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float speed = 1; // the speed of the player movement
    [SerializeField] private float jumpPower; //  power of the jump 
    [SerializeField] private LayerMask groundLayer; // to find the collision with ground layer, i contain the gound layer as value

    private bool facingRight = true; // check if the player is facing right direction
    private bool isliedown = false; // check if the player is laying down on the ground

    private Rigidbody2D rb; // reference to the rigid body componenet of the player 
    private BoxCollider2D boxCollider; // reference to the box collider componenet of the player
    private Animator anim;  // reference to the animator componenet of the player

    // call on the first time the player script load
    /*
        set speed, jump power of the player also you can set in unity editor
    */
    private void Awake(){
        speed = 5f;
        jumpPower = 850f;
        rb = GetComponent<Rigidbody2D>();
        boxCollider =  GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update(){
        float moveInput = Input.GetAxis("Horizontal");
        //  change the face direction
        if(moveInput > 0.0f && !facingRight || moveInput < 0.0f && facingRight){
            flip();
        }
        // jump the player
        if(Input.GetButtonDown("Jump") && isGrounded() && !isliedown){
            jump();
        }
       // Check if down arrow key is pressed
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && isGrounded())
        {
            lieDown();
        }else{
            lieUp();
        }
        // set the movement of the player only in case player is not laying down on the ground
        if(!isliedown){
            leftRightMovement(moveInput);
        }

        anim.SetBool("runing", moveInput != 0 && isGrounded()); // player on can run if the player is on the ground and horizontally not idle
        anim.SetBool("grounded", isGrounded()); 
    }

    // move left or right
    public void leftRightMovement(float position){
        rb.velocity = new Vector2(position * speed, rb.velocity.y);
    }

    // flip the player to face to the direction of movement
    private void flip(){
        facingRight = !facingRight;
        // transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        transform.Rotate(0, 180, 0);
    }

    // jump the player 
    private void jump(){
        rb.AddForce(Vector2.up * jumpPower);
        anim.SetTrigger("jump");
    }

    // check the player is on ground layer object
    private bool isGrounded(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.2f, groundLayer);
        return hit.collider != null;
    }

    // lie down the player on the ground
    private void lieDown(){
        anim.SetTrigger("liedown");
        anim.SetBool("isliedown", true);
        isliedown = true;
    }

    // check player is on the ground or not
    private void lieUp(){
        anim.SetBool("isliedown", false);
        isliedown = false;
    }

    
}