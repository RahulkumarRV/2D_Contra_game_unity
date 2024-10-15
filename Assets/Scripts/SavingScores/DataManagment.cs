using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagment : MonoBehaviour
{
    private float _currentScore;
    private float _highScore;
    public void playerIncrementScore( float val )
    {
        if (val > 0)
        {
            _currentScore += val;
            PlayerPrefs.SetFloat("currentScore", _currentScore);
        }
        if (_currentScore >= _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetFloat("hightScore", _highScore);
        }
    }
    private void Start()
    {
        _highScore = PlayerPrefs.GetFloat("hightScore");
        // _currentScore = PlayerPrefs.GetFloat("currentscore");
        PlayerPrefs.DeleteKey("currentScore");
    }

}
