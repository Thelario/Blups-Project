using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ExitGameButton : MonoBehaviour, IPointerEnterHandler
{
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnButtonClicked()
    {
        SoundManager.Instance.PlaySound(SoundType.ButtonClick);

        Exit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
#if UNITY_STANDALONE
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
#endif
    }
}