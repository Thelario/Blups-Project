using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class ResumeButton : MonoBehaviour, IPointerEnterHandler
    {
        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            TimeManager.Instance.Resume();
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameMenu);
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
#if UNITY_STANDALONE || UNITY_WEBGL
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
#endif
        }
    }
}
