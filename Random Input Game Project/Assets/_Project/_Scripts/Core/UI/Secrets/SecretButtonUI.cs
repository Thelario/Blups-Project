using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class SecretButtonUI : MonoBehaviour
    {
        public string ruleText;
        public Text text;
        public SecretsMenu secretsMenu;

        public void OnButtonClicked()
        {
            secretsMenu.OpenRule(ruleText);
        }
    }
}
