using Game.Managers;
using UnityEngine;
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

        private Button _menuButton;

        private void Start()
        {
            _menuButton = GetComponent<Button>();
            _menuButton.onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            switch (desiredCanvasType)
            {
                case CanvasType.GameMenu:
                    TimeManager.Instance.Resume();
                    CanvasManager.Instance.SwitchCanvas(desiredCanvasType, SceneGameManager.Instance.transitionTime * 1.1f);
                    GameManager.Instance.SelectLoopingMinigames();
                    if (loadNewScene)
                        SceneGameManager.Instance.LoadRandomGameScene();
                    break;
                case CanvasType.MainMenu:
                    TimeManager.Instance.Pause();
                    CanvasManager.Instance.SwitchCanvas(desiredCanvasType, SceneGameManager.Instance.transitionTime * 1.1f);
                    if (loadNewScene)
                        SceneGameManager.Instance.LoadScene(Scenes.Starting);
                    break;
                default:
                    TimeManager.Instance.Pause();
                    CanvasManager.Instance.SwitchCanvas(desiredCanvasType);
                    break;
            }
        }
    }
}
