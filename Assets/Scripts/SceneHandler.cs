using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void GoToGame()
    {

        SceneManager.LoadScene(Random.Range(3, SceneManager.sceneCountInBuildSettings));
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene(2);
    }
}
