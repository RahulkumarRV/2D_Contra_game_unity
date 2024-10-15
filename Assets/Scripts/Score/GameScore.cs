using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public int sum = 0;
    public int highScore ;

    
    private void Update()
    {
        {
            //to add enemy bullet points
            if (player != null && player.position.x > 0)
            {
                scoreText.text = SavingScore.Score.currentSocre.ToString();
            }
   
            
        }
    }
}
