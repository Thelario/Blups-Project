using UnityEngine;
using UnityEngine.UI;
using Game.Managers;

namespace Game.UI
{
    public class MinigameButton : MonoBehaviour
    {
        [SerializeField] private Scenes sceneToLoad;

        private CanvasManager _canvasManager;
        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
            _canvasManager = CanvasManager.Instance;
        }

        private void OnButtonClicked()
        {
            GameManager.Instance.loopIndefinitely = true;
            TimeManager.Instance.Resume();
            SceneGameManager.Instance.LoadScene(sceneToLoad);
            _canvasManager.SwitchCanvas(CanvasType.GameMenu);
        }
    }
}
