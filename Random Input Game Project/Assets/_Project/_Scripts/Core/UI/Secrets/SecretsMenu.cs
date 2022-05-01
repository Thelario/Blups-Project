using UnityEngine;
using Game.Managers;

namespace Game.UI
{
    public class SecretsMenu : MonoBehaviour
    {
        [SerializeField] private GameObject foundSecretPrefab;
        [SerializeField] private GameObject notFoundSecretPrefab;
        [SerializeField] private Transform secretsParent;
        [SerializeField] private SecretsPanel secretsPanel;

        private void OnEnable()
        {
            CreateItems();
        }

        private void OnDisable()
        {
            DestroyItems();
        }

        public void CreateItems()
        {
            foreach (Secret s in SecretsManager.Instance.secrets)
            {
                if (!s.found)
                {
                    Instantiate(notFoundSecretPrefab, secretsParent);
                    continue;
                }

                GameObject secret = Instantiate(foundSecretPrefab, secretsParent);
                SecretButtonUI sbui = secret.GetComponent<SecretButtonUI>();
                sbui.text.text = s.name;
                sbui.ruleText = s.content;
                sbui.secretsMenu = this;
            }
        }

        public void DestroyItems()
        {
            foreach (Transform t in secretsParent)
            {
                Destroy(t.gameObject);
            }
        }

        public void OpenRule(string ruleText)
        {
            secretsPanel.OpenPanel(ruleText);
        }
    }
}
