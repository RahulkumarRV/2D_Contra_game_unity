using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// manage the high score of the player game play
public class Level1SenceDataManagment : MonoBehaviour
{
    [SerializeField] Text highScore;
    // Start is called before the first frame update
    void Start()
    {
        if(highScore != null){
            highScore.text = PlayerPrefs.GetFloat("highScore").ToString();
        }
    }

}
