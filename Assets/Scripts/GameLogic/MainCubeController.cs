using Colors;
using Cubes;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameLogic
{
    public class MainCubeController : MonoBehaviour
    {
        [SerializeField] private float time;
        [SerializeField] private float alpha;
        
        private CubeInfo _currentCube;
        private TowerManager _tower;
        private ColorSequencer _seq;
        private bool _configured;
        private Tweener _tween;
        private float _startTime;
        private bool _zAxis;
        
        public CubeInfo CurrentCube => _currentCube;

        private void Awake()
        {
            _startTime = time;
        }

        public void Configure(TowerManager tower, ColorSequencer colorSeq)
        {
            _tower = tower;
            _seq = colorSeq;
            _configured = true;
        }

        public void UpdateCube()
        {
            Assert.IsTrue(_configured, "MainCubeController not configured!!");
            
            if(_currentCube != null)
                _currentCube.Cube.SetActive(false);
            
            var lastCube = _tower.LastCube;
            Debug.Log($"Last -> {lastCube}");
            var t = lastCube.CubeTransform;
            
            _currentCube = new CubeInfo(lastCube.SideA, lastCube.SideB, _seq.GetColor(), t.x, t.z);

            MoveCube();
        }

        private void MoveCube()
        {
            _zAxis = !_zAxis;
            
            var targetScale = _currentCube.Cube.transform.localScale;
            _currentCube.Cube.transform.localScale = Vector3.zero;

            var xTargetPos = new Vector3(-1, 0.1f, _currentCube.CubeTransform.z);
            var zTargetPos = new Vector3(_currentCube.CubeTransform.x, 0.1f, -1);
            var targetPos = _zAxis ? zTargetPos : xTargetPos;

            var xInitPos = new Vector3(1, 0.1f, _currentCube.CubeTransform.z);
            var zInitPos = new Vector3( _currentCube.CubeTransform.x, 0.1f, 1);
            
            _currentCube.SetPosition(_zAxis? zInitPos: xInitPos);
            
            if(_tween != null)
                _tween.Kill();
            
            var t = _currentCube.Cube.transform.DOScale(targetScale, 0.2f);
            t.onComplete += () =>
            { 
                Debug.Log(_currentCube);
                _tween = _currentCube.Cube.transform.DOMove(targetPos, time)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
            };
        }

        public void Stop()
        {
            _currentCube.Cube.SetActive(false);
            _currentCube = null;
            _tween.Kill();
            time = _startTime;
        }
        
        public void VelocityUp() => 
            time *= alpha;

        public void ResetVelocity() => time = _startTime;
    }
}