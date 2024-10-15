using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class endLevel2Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // if player reach the end of this level after destroy the boss then move to next level
    void OnTriggerExit2D(Collider2D other){
        // call function to end this level
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene("GameOverScene");
        }
    }


}
