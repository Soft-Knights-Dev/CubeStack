using UnityEngine;

namespace Colors
{
    public class ColorSequencer: MonoBehaviour
    {
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private int step;
        
        private int _currentValue;
        public int CurrentValue => _currentValue;

        
        private void Awake() =>
            _currentValue = -step;

        public UnityEngine.Color GetColor() =>
            colorGradient.Evaluate((_currentValue += step) % 100 / 100f);

        public Color EvaluateAt(float position) => 
            colorGradient.Evaluate(position);

        public void Reset() =>
            _currentValue = -step;
    }
}