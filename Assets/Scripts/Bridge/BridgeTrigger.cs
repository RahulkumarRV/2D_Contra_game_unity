using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrigger : MonoBehaviour
{

    private bool isActive = false;

    private Transform bridge;
    void Start() {
        bridge = transform.GetChild(0);
    }
    // check if the player tuches the bridge then start the explosion effect
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && bridge != null){
            isActive = true;
            var bridgeScript = bridge.GetComponent<Bridge>();
            StartCoroutine(bridgeScript.setExplosionPosition(1.7f));
            var bridgeChildScript = bridge.transform.GetChild(0).GetComponent<BridgeChild>();
            if(bridgeChildScript != null){
                StartCoroutine(bridgeChildScript.nextStep(2.0f));
            }
            
        }
    }

    // check if the bridge is active or not
    public bool getIsActive(){
        return isActive;
    }
}
