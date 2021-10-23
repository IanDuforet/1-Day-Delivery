using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour

{ 
    public void SwitchScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
        FindObjectOfType<PauseMenu>().GameIsPaused = false;
        FindObjectOfType<PauseMenu>().PauseMenuUI.SetActive(false);

        MovingShader[] movingShaders = FindObjectsOfType<MovingShader>();
        for (int i = 0; i < movingShaders.Length; i++)
        {
            movingShaders[i].Reset();
        }

        Debug.ClearDeveloperConsole();
    }
}
