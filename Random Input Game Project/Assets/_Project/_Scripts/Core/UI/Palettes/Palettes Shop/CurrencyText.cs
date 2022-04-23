using UnityEngine;
using TMPro;
using Game.Managers;

namespace Game.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class CurrencyText : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            CurrencyManager.OnUpdateCurrency += UpdateText;
        }

        private void OnDestroy()
        {
            CurrencyManager.OnUpdateCurrency -= UpdateText;
        }

        private void OnEnable()
        {
            UpdateText(CurrencyManager.Instance.CurrencyAmount);
        }

        private void UpdateText(int currency)
        {
            _text.text = currency.ToString();
        }
    }
}
