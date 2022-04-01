using Game.UI;
using UnityEngine;

namespace Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [HideInInspector] public Direction obstaclesCurrentDirection;

        public float obstaclesVelocity = 350f;

        public bool loopIndefinitely = false;

        public void SetRandomDirection()
        {
            obstaclesCurrentDirection = (Direction)Random.Range(0, 4);
            DangerAnimationManager.Instance.SetActiveExclamation(obstaclesCurrentDirection);
        }
    }
}
