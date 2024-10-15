using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_controller : MonoBehaviour
{

    private Collider2D _collider;
    private bool _playerOnPlatform;
    private Animator _playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerOnPlatform && Input.GetKey(KeyCode.DownArrow) && Input.GetButtonDown("Jump")){
            _collider.enabled = false;
            if(_playerAnimator != null) _playerAnimator.Play("jump");
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider(){
        yield return new WaitForSeconds(0.5f);
        _collider.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value){
        var player = other.gameObject.GetComponent<PlayerMovment>();
        if(player != null){
            _playerOnPlatform = value;
            _playerAnimator = other.gameObject.GetComponent<Animator>();
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other){
        SetPlayerOnPlatform(other, false);
    }
}
