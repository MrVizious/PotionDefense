using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DesignPatterns;
using Sirenix.OdinInspector;

public class SceneManagerSingleton : Singleton<SceneManagerSingleton>
{
    [Button]
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    [Button]
    public void GoToTestLevel()
    {
        SceneManager.LoadScene("Test Level");
    }

    [Button]
    public void GoToLoseMenu()
    {
        SceneManager.LoadScene("Lose Menu");
    }
}
