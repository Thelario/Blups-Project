using UnityEngine;
using UnityEngine.UI;
using Game.Managers;

namespace Game.UI
{
    public class MinigameButton : MonoBehaviour
    {
        [SerializeField] private Scenes sceneToLoad;

        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            GameManager.Instance.SelectIndividualMinigame();
            TimeManager.Instance.Resume();
            SceneGameManager.Instance.LoadScene(sceneToLoad);
            CanvasManager.Instance.SwitchCanvas(CanvasType.GameMenu, SceneGameManager.Instance.transitionTime * 1.1f);
        }
    }
}
