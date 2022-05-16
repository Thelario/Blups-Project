using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void RandomDirectionChange();
        public static event RandomDirectionChange OnRandomDirectionChange;

        public delegate void PlayerDeath();
        public static event PlayerDeath OnPlayerDeath;
        public static event PlayerDeath OnPlayerRevive;

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
        }

        public void SelectLoopingMinigames()
        {
            loopIndefinitely = false;
        }

        public void PlayerDies()
        {
            OnPlayerDeath?.Invoke();
        }

        public void PlayerRevive()
        {
            OnPlayerRevive?.Invoke();
        }
    }
}
