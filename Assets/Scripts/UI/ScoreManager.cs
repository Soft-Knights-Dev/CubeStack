using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        public int Score { get; private set; }

        private void Awake()
        {
            Score = 0;
        }

        public void UpdateScore(int score)
        {
            Score += score;
            scoreText.text = Score.ToString();
        }
        
        public void Reset()
        {
            Score = 0;
            scoreText.text = Score.ToString();
        }
    }
}