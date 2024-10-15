using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{

    [SerializeField] private Text hightScoreText, currentSocreateText;
    // Start is called before the first frame update
    // set the high score and current score
    void Start()
    {
        hightScoreText.text = PlayerPrefs.GetFloat("hightScore").ToString();
        currentSocreateText.text = PlayerPrefs.GetFloat("currentScore").ToString();
    }
}
