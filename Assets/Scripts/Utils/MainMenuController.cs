using Input;
using UnityEngine;


public class MainMenuController : MonoBehaviour
{
    private GameInput _input;
    
    void Awake()
    {
        _input = new GameInput();
        _input.SetInputActivation(true);
    }
}
