using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void loadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void quitLevel()
    {
        Application.Quit();
    }

    public void loadNext(string name, int counter)
    {
        SceneManager.LoadScene(name);
    }


}
