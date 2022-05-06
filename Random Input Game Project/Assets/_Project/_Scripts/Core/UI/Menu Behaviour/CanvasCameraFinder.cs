using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class CanvasCameraFinder : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void OnLevelWasLoaded(int level)
        {
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                _canvas.worldCamera = Camera.main;
            }
        }
    }
}
