using UnityEngine;

namespace Game.Managers
{
    public class DangerAnimationManager : Singleton<DangerAnimationManager>
    {
        [SerializeField] private Transform[] dangerParticles;
        [SerializeField] private Transform[] directionParticles;

        protected override void Awake()
        {
            base.Awake();

            GameManager.OnRandomDirectionChange += RandomDirectionChange;
        }

        private void OnDestroy()
        {
            GameManager.OnRandomDirectionChange -= RandomDirectionChange;
        }

        public void SetActiveAllDangerParticles(bool active)
        {
            return;
        
            if (dangerParticles.Length == 0)
                return;

            foreach (Transform ex in dangerParticles)
                ex.gameObject.SetActive(active);
        }

        public void SetActiveAllDirectionParticles(bool active)
        {
            if (directionParticles.Length == 0)
                return;

            foreach (Transform p in directionParticles)
                p.gameObject.SetActive(false);
        }

        public void SetActiveExclamation(Direction dir)
        {
            if (dangerParticles.Length == 0 || directionParticles.Length == 0)
                return;

            SetActiveAllDangerParticles(false);
            SetActiveAllDirectionParticles(false);

            int d = (int)dir;
            dangerParticles[d].gameObject.SetActive(true);
            directionParticles[d].gameObject.SetActive(true);
        }

        private void RandomDirectionChange()
        {
            SetActiveExclamation(GameManager.Instance.obstaclesCurrentDirection);
        }
    }
}
