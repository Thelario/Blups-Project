using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Managers
{
    [System.Serializable]
    public class Secret
    {
        public string name;
        public bool found;
        public string content;
    }

    public class SecretsManager : Singleton<SecretsManager>
    {
        [Header("Secrets Stuff")]
        [FormerlySerializedAs("secrets")] public List<Secret> availableSecrets;
        public List<Secret> notAvailableSecrets;
        public bool keepSpawningSecrets = true;

        [Header("Secret Picked Animation")] 
        [SerializeField] private GameObject animationCanvas;
        [SerializeField] private Animator secretPickedAnimator;
        [SerializeField] private string secretAnimationName;
        [SerializeField] private float secretAnimationTime;

        protected override void Awake()
        {
            base.Awake();
            
            LoadSecrets();
        }

        private void SaveSecrets()
        {
            foreach (Secret s in availableSecrets)
            {
                PlayerPrefs.SetInt(s.name, s.found ? 1 : 0);
            }
            
            PlayerPrefs.Save();
        }

        private void LoadSecrets()
        {
            foreach (Secret s in availableSecrets)
            {
                if (!PlayerPrefs.HasKey(s.name))
                    return;

                s.found = PlayerPrefs.GetInt(s.name) == 1;
            }
        }
        
        public void UnlockRandomSecret()
        {
            StartCoroutine(nameof(AnimateSecretPicked));
            
            if (!keepSpawningSecrets)
                return;
            
            // We first retrieve all the not found secrets
            List<Secret> notFoundSecrets = new List<Secret>();
            foreach (Secret s in availableSecrets)
            {
                if (s.found)
                    continue;
                
                notFoundSecrets.Add(s);
            }
            
            // Then we mark as found one random secret from those secrets that we have retrieved
            notFoundSecrets[Random.Range(0, notFoundSecrets.Count)].found = true;
            
            SaveSecrets();
            
            CheckKeepSpawningSecrets();
        }

        private void CheckKeepSpawningSecrets()
        {
            // We count the number of secrets that are still not found
            int secretsNotFound = 0;
            foreach (Secret s in availableSecrets)
            {
                if (s.found)
                    continue;

                secretsNotFound++;
            }

            // If all the secrets have been found, then do not spawn secrets anymore
            if (secretsNotFound == 0)
                keepSpawningSecrets = false;
        }

        private IEnumerator AnimateSecretPicked()
        {
            TimeManager.Instance.Pause();
            animationCanvas.SetActive(true);
            secretPickedAnimator.Play(secretAnimationName);
            yield return new WaitForSecondsRealtime(secretAnimationTime);
            animationCanvas.SetActive(false);
            TimeManager.Instance.Resume();
        }
    }
}
