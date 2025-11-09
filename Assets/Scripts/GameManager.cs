using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelLoadWait = 0.5f;
    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadSceneAfterWait(levelName));
        
    }

    public void QuitGame()
    {
        print("Quit Game");
        Application.Quit();
    }

    IEnumerator LoadSceneAfterWait(string levelName)
    {
        yield return new WaitForSeconds(levelLoadWait);
        SceneManager.LoadScene(levelName);
    }
}
