using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingScore : MonoBehaviour
{
    // Start is called before the first frame update
    public static SavingScore Score;

    private float _currentScore;
    private float _highScore;

    public float currentSocre
    {
        get { return _currentScore; }
    }

    public float highSocre
    {
        get { return _highScore; }
    }

    public void incrementScore( float val )
    {
        if (val > 0)
        {
            val = val - _currentScore; 
            _currentScore += val;
        }
    }

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

    private void Awake() 
    {
        if (Score == null)
        {
            Score = this;
            DontDestroyOnLoad(Score);
        }
        else if (Score != null)
        {
            Destroy(Score);
        }
    }

    private void Start()
    {
        _highScore = PlayerPrefs.GetFloat("hightScore");
        // _currentScore = PlayerPrefs.GetFloat("currentscore");
        PlayerPrefs.DeleteKey("currentScore");
    }

}
