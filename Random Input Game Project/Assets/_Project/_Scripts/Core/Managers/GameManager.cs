using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void RandomDirectionChange();
        public static event RandomDirectionChange OnRandomDirectionChange;

        [HideInInspector] public Direction obstaclesCurrentDirection;

        public bool loopIndefinitely = false;

#if UNITY_ANDROID
        protected override void Awake()
        {
            base.Awake();
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
#endif

        public void SetRandomDirection()
        {
            obstaclesCurrentDirection = (Direction)Random.Range(0, 4);
            OnRandomDirectionChange?.Invoke();
        }

        public void SelectIndividualMinigame()
        {
            loopIndefinitely = true;
            LevelManager.Instance.SelectIndividualMinigame();
        }

        public void SelectLoopingMinigames()
        {
            loopIndefinitely = false;
            LevelManager.Instance.SelectLoopingMinigames();
        }
    }
}
