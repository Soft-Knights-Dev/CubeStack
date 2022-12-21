using GameLogic;
using TMPro;
using UnityEngine;

public class RetryMenuController : MonoBehaviour
{
    [SerializeField] private GameObject retryCanvas;
    [SerializeField] private TMP_Text score;
    
    private void Awake()
    {
        retryCanvas.SetActive(false);
    }

    private void OnEnable()
    {
        GameSignals.GameEnd += OnGameFinished;
    }
    
    
    private void OnDisable()
    {
        GameSignals.GameEnd -= OnGameFinished;
    }

    private void OnGameFinished()
    {
        score.text = GameManager.Instance.Score.ToString();
        retryCanvas.SetActive(true);
    }
}
