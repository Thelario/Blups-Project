using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Toggle))]
    public class ToggleBehaviour : MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color notSelectedColor;

        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
            ColorBlock cb = toggle.colors;

            if (isOn)
            {
                cb.normalColor = selectedColor;
                cb.highlightedColor = selectedColor;
                cb.selectedColor = selectedColor;
                cb.pressedColor = selectedColor;
            }
            else
            {
                cb.normalColor = notSelectedColor;
                cb.highlightedColor = notSelectedColor;
                cb.selectedColor = notSelectedColor;
                cb.pressedColor = notSelectedColor;
            }

            toggle.colors = cb;
        }

        public void ChangeToggleValue(bool isOn)
        {
            toggle.isOn = isOn;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
#if UNITY_STANDALONE
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
#endif
        }
    }
}
