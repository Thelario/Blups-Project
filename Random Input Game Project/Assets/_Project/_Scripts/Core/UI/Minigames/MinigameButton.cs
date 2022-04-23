using UnityEngine;
using UnityEngine.UI;
using Game.Managers;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class MinigameButton : MonoBehaviour, IPointerEnterHandler
    {
        public Scenes sceneToLoad;

        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
            GameManager.Instance.SelectIndividualMinigame();
            TimeManager.Instance.Resume();
            SceneGameManager.Instance.LoadScene(sceneToLoad);
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameMenu, SceneGameManager.Instance.transitionTime * 1.1f);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
#if UNITY_STANDALONE
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
#endif
        }
    }
}
