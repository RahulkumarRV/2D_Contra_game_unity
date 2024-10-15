using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] fire;
    [SerializeField] private bool show = true;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            if(fire != null || fire.Length > 0){
                for(int i=0; i<fire.Length; i++){
                    if(show) fire[i].SetActive(true);
                    else Destroy(fire[i]);
                }
            }
        }
    }
}
