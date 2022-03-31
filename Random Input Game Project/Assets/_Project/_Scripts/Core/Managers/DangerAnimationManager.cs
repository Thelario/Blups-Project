using UnityEngine;

namespace Game.Managers
{
    public class DangerAnimationManager : Singleton<DangerAnimationManager>
    {
        [SerializeField] private Transform[] dangerParticles;
        [SerializeField] private Transform[] directionParticles;

        public void SetActiveAllDangerParticles(bool active)
        {
            if (dangerParticles == null)
                return;

            foreach (Transform ex in dangerParticles)
                ex.gameObject.SetActive(active);
        }

        public void SetActiveAllDirectionParticles(bool active)
        {
            if (directionParticles == null)
                return;

            foreach (Transform p in directionParticles)
                p.gameObject.SetActive(false);
        }

        public void SetActiveExclamation(Direction dir)
        {
            if (dangerParticles == null || directionParticles == null)
                return;

            SetActiveAllDangerParticles(false);
            SetActiveAllDirectionParticles(false);

            int d = (int)dir;
            dangerParticles[d].gameObject.SetActive(true);
            directionParticles[d].gameObject.SetActive(true);
        }
    }
}
