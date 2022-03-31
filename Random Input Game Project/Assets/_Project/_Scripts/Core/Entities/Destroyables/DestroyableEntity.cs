using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public abstract class DestroyableEntity : MonoBehaviour
    {
        protected virtual void Awake()
        {
            LevelManager.OnLevelPause += DestroyYourself;
            LevelManager.OnLevelEnd += DestroyYourself;
            LevelManager.OnLevelLost += DestroyYourself;
        }

        protected virtual void OnDestroy()
        {
            LevelManager.OnLevelPause -= DestroyYourself;
            LevelManager.OnLevelEnd -= DestroyYourself;
            LevelManager.OnLevelLost -= DestroyYourself;
        }

        public virtual void DestroyYourself(float time)
        {
            Destroy(gameObject, time);
        }

        public virtual void DestroyYourself()
        {
            DestroyYourself(0.0f);
        }
    }
}
