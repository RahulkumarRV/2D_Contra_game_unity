using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingScene : MonoBehaviour
{
    // Start is called before the first frame update

    public void StartingSceneMovement()
    {
        SceneManager.LoadScene("Level1EntryScene");
    }
}
