using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        public enum LevelState { PauseBetween, Playing, Pause }

        public delegate void LevelEvent();
        public static LevelEvent OnGameStart;
        public static LevelEvent OnLevelStart;
        public static LevelEvent OnLevelEnd;
        public static LevelEvent OnLevelLost;
        public static LevelEvent OnLevelPause;
        public static LevelEvent OnLevelUnPause;
        public static LevelEvent OnCoinObtained;

        [SerializeField] private Text timeBetweenText;
        [SerializeField] private Text currentLevelTimeText;
        [SerializeField] private float timeBetweenWaves; // 10 or 15 seconds, after which the game changes the level and input
        [SerializeField] private float timeBetweenPauses; // 3 or 5 seconds, that tells the player the new input and the new way to play

        private float _currentLevelTime;
        private float _timeBetweenWavesCounter;
        private float _timeBetweenPausesCounter;
        private LevelState _levelState;
        private LevelState _prevState;

        private void Start()
        {
            _currentLevelTime = 0f;
            _timeBetweenWavesCounter = timeBetweenWaves;
            _timeBetweenPausesCounter = timeBetweenPauses;
            _levelState = LevelState.PauseBetween;

            OnLevelEnd?.Invoke();
            OnGameStart?.Invoke();
        }

        private void OnLevelWasLoaded(int level)
        {
            _timeBetweenWavesCounter = timeBetweenWaves;
            _timeBetweenPausesCounter = timeBetweenPauses;
            _levelState = LevelState.PauseBetween;

            OnLevelEnd?.Invoke();
            OnGameStart?.Invoke();
        }

        private void Update()
        {
            CheckTime();
        }

        private void CheckTime()
        {
            UpdateLevelTime();

            if (_levelState == LevelState.Playing)
            {
                _timeBetweenWavesCounter -= Time.deltaTime;
                UpdateTimeBetweenText(_timeBetweenWavesCounter);

                if (_timeBetweenWavesCounter <= 0f)
                {
                    _levelState = LevelState.PauseBetween;
                    _timeBetweenWavesCounter = timeBetweenWaves;
                    OnLevelEnd?.Invoke();
                    SceneGameManager.Instance.LoadRandomGameScene();
                }
            }
            else if (_levelState == LevelState.PauseBetween)
            {
                _timeBetweenPausesCounter -= Time.deltaTime;
                UpdateTimeBetweenText(_timeBetweenPausesCounter);

                if (_timeBetweenPausesCounter <= 0f)
                {
                    _levelState = LevelState.Playing;
                    _timeBetweenPausesCounter = timeBetweenPauses;
                    OnLevelStart?.Invoke();
                }
            }
        }

        public void Pause()
        {
            _prevState = _levelState;
            _levelState = LevelState.Pause;
            OnLevelPause?.Invoke();
        }

        public void UnPause()
        {
            _levelState = _prevState;
            OnLevelUnPause?.Invoke();
        }

        private void UpdateLevelTime()
        {
            if (_levelState != LevelState.Playing)
                return;

            _currentLevelTime += Time.deltaTime;

            if (currentLevelTimeText != null)
                currentLevelTimeText.text = ((int)_currentLevelTime).ToString();
        }

        private void UpdateTimeBetweenText(float time)
        {
            if (currentLevelTimeText != null)
                timeBetweenText.text = ((int)time).ToString();
        }
    }
}
