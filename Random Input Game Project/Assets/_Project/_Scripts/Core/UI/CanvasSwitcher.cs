using Game.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.UI
{
    /// <summary>
    /// CanvasSwitcher must be added to each button that will redirect to another panel (ex. MainMenu -> GameMenu through button Play).
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class CanvasSwitcher : MonoBehaviour
    {
        public CanvasType desiredCanvasType;
        public bool loadNewScene = false;

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
            switch (desiredCanvasType)
            {
                case CanvasType.GameMenu:
                    TimeManager.Instance.Resume();
                    _canvasManager.SwitchCanvas(desiredCanvasType);
                    GameManager.Instance.loopIndefinitely = false;
                    if (loadNewScene)
                        SceneGameManager.Instance.LoadRandomGameScene();
                    break;
                case CanvasType.MainMenu:
                    TimeManager.Instance.Pause();
                    _canvasManager.SwitchCanvas(desiredCanvasType);
                    if (loadNewScene)
                        SceneGameManager.Instance.LoadScene(Scenes.Starting);
                    break;
                default:
                    TimeManager.Instance.Pause();
                    _canvasManager.SwitchCanvas(desiredCanvasType);
                    break;
            }
        }
    }
}
