using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
#if UNITY_STANDALONE
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
#endif
        }
    }
}
