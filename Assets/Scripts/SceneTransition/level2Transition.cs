using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level2Transition : MonoBehaviour
{
    [SerializeField]
    private float delayTimeToLoad = 5f;

    private float time;

    // Update is called once per frame
    private void Update()
    {
        time = time + Time.deltaTime;
        if (time > delayTimeToLoad)
        {
            SceneManager.LoadScene("level3");
        }
    }
}
