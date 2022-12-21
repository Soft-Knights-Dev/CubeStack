using Colors;
using Cubes;
using Cutter;
using Input;
using Pooler;
using Sirenix.OdinInspector;
using Sound;
using UI;
using UnityEngine;
using Utils;

namespace GameLogic
{
    [RequireComponent(typeof(ColorSequencer))]
    [RequireComponent(typeof(MainCubeController))]
    [RequireComponent(typeof(ScoreManager))]
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private PerfectMatchController perfectMatch;
        
        private ColorSequencer _colorSequencer;
        private TowerManager _tower;
        private BoxCutter _cutter;
        private GameInput _input;
        private ScoreManager _score;
        private MainCubeController _mainCube;
        private bool _canPlay;
        
        [ShowInInspector] public int NumCubes => _tower?.NumCubes ?? 0;
        public int Score => _score.Score;
        public float CubeWidth => _mainCube.CurrentCube?.SideA ?? float.NaN;
        public int perfectStreak;
        protected  void Start()
        {
            _colorSequencer = GetComponent<ColorSequencer>();
            _mainCube = GetComponent<MainCubeController>();
            _score = GetComponent<ScoreManager>();
            
            _cutter = new BoxCutter();
            _input = new GameInput();
            _tower = new TowerManager();

            _mainCube.Configure(_tower, _colorSequencer);
            ResetGame();
            GameSignals.GameStart?.Invoke();
        }
        
        private void OnEnable()
        {
            GameSignals.PlayerTap += OnTap;
        }

        private void OnDisable()
        {
            GameSignals.PlayerTap -= OnTap;
        }

        public void RemoveCube(CubeInfo info) =>
            _tower.DeleteFromTower(info);

        private void OnTap()
        {
            if (!_canPlay) return;
            
            _tower.MoveTowerDown();
            
            var mainCube = _mainCube.CurrentCube;
            var towerCube = _tower.LastCube;
            var cuts = _cutter.CutCubes(towerCube, mainCube);
            var state = CheckGameStatus(cuts, towerCube);
            
            Debug.Log(state);

            switch (state)
            {
                case GameStatus.PerfectMatch:
                    PerfectMatch(mainCube, towerCube);
                    perfectStreak++;
                    SoundManager.Instance.PitchValue(0.005f);
                    break;
                
                case GameStatus.Cut:
                    CutBoxes(cuts, mainCube, towerCube);
                    SoundManager.Instance.ResetPitch();
                    break;
                
                case GameStatus.Finished:
                    _canPlay = false;
                    _mainCube.Stop();
                    SoundManager.Instance.ResetPitch();
                    GameSignals.GameEnd?.Invoke();
                    break;
            }
        }

        private GameStatus CheckGameStatus(CuttingDeltas d, CubeInfo towerCube)
        {
            var towerCubeSize = new Vector3(towerCube.SideA, 0.1f, towerCube.SideB);
            var goa = d.CenterDifference.MultiplyVector(d.CutDirection).sqrMagnitude;
            var gob = towerCubeSize.MultiplyVector(d.CutDirection).sqrMagnitude;
            
            var gameOver =  goa >= gob;
            var perfect = goa < gob * 0.0005f;
            
            if (perfect)
                return GameStatus.PerfectMatch;
            
            return gameOver ? GameStatus.Finished : GameStatus.Cut;
        }

        private void PerfectMatch(CubeInfo mainCube, CubeInfo towerCube)
        {
            var c = new CubeInfo(mainCube.SideA, mainCube.SideB, mainCube.Color, 
                towerCube.CubeTransform.x, towerCube.CubeTransform.z);
            
            SoundManager.Instance.PlayPerfect();
            _tower.AddToTower(c);
            c.ScaleUp(0.02f);
            _mainCube.VelocityUp();
            perfectMatch.DoEffect(c);
            _mainCube.UpdateCube();
            _score.UpdateScore(1);
        }

        private void CutBoxes(CuttingDeltas d, CubeInfo mainCube, CubeInfo towerCube)
        {
            var newCube = _cutter.PerformCut(d, towerCube, mainCube);
            
            SoundManager.Instance.PlayNormal();
            _tower.AddToTower(newCube);
            _mainCube.ResetVelocity();            
            _mainCube.UpdateCube();
            _score.UpdateScore(1);
        }

        public void ResetGame()
        {
            _tower.Reset();
            SoundManager.Instance.ResetPitch();
            CubePooler.Instance.Reset();
            _score.Reset();
            _colorSequencer.Reset();
            _tower.AddToTower(new CubeInfo(1, 1, _colorSequencer.GetColor(), 0, 0));
            _mainCube.UpdateCube();
            _canPlay = true;
            perfectStreak = 0;
            
        }
    }
}