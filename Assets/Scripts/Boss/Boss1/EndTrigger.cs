using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
public class EndTrigger : MonoBehaviour
{
    private GameObject Boss1; // reference to the boss1 object

    // check if the player touches the End Trigger then navigate to the next level scene
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            SceneManager.LoadScene("level2EntryScene");
        }
    }
}
