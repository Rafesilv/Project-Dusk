using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public string level;

    public void Restart()
    {
        SceneManager.LoadScene(level);
    }

    public void BackMainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
