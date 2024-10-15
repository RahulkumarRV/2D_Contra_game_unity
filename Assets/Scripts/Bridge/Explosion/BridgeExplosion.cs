using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeExplosion : MonoBehaviour
{
    Transform[] children;

    // set the reference to component's of the object
    void Start()
    {
        children = new Transform[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
        }

        StartCoroutine(startExplosion(0.2f));
    }
    // if bridge is triggered then play the explosion animationof the bridge and destroy the bridge
    IEnumerator startExplosion(float timeDelay){

        for(int i=0; i<children.Length; i++){
            children[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(timeDelay);
        }
        Destroy(gameObject);
    }

    
}
