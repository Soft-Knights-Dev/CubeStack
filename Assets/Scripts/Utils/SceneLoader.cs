using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public void LoadScene(string sceneName) =>
        SceneManager.LoadScene(sceneName);

    public void LoadMainScene()
    {
        LoadSceneWaiting("Game", 1f);
    }
        
    public void LoadSceneWaiting(string sceneName, float time) =>
        StartCoroutine(LoadWaitingCoroutine(sceneName, time));

    private IEnumerator LoadWaitingCoroutine(string sceneName, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
    
    
    
}
