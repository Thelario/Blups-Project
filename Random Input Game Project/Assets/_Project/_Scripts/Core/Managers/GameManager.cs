using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void RandomDirectionChange();
        public static event RandomDirectionChange OnRandomDirectionChange;

        [HideInInspector] public Direction obstaclesCurrentDirection;

        public float obstaclesVelocity = 350f;

        public bool loopIndefinitely = false;

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
