using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class SnowLevelEnd : MonoBehaviour
{
    [SerializeField] private AudioSource endLevelAudioSource;
    // check if the player reach to the end after destroy the boss the move to next level
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" && transform.parent.childCount <= 1){
            if(endLevelAudioSource != null) endLevelAudioSource.Play();
            SceneManager.LoadScene("level3Transition");
        }
    }
}
