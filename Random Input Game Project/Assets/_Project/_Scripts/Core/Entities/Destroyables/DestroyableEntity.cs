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

            GameManager.OnPlayerDeath += AnimateDestroy;
        }

        protected virtual void OnDestroy()
        {
            LevelManager.OnLevelPause -= DestroyYourself;
            LevelManager.OnLevelEnd -= DestroyYourself;
            LevelManager.OnLevelLost -= DestroyYourself;
            
            GameManager.OnPlayerDeath -= AnimateDestroy;
        }

        protected void DestroyYourself(float time)
        {
            Destroy(gameObject, time);
        }

        protected virtual void DestroyYourself()
        {
            DestroyYourself(0.0f);
        }

        private void AnimateDestroy()
        {
            LeanTween.scale(gameObject, Vector3.zero, .5f);
            DestroyYourself(0.5f);
        }
    }
}
