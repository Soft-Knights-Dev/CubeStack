using UnityEditor;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void Awake()
    {
        #if UNITY_WEBGL
        gameObject.SetActive(false);
        #endif
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif
        
        Application.Quit();
    }
}
