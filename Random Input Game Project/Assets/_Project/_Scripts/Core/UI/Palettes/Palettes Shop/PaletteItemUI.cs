using Game.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class PaletteItemUI : MonoBehaviour, IPointerEnterHandler
    {
        public Image[] colors;
        public ColorPalette colorPalette;
        public PalettesShop palettesShop;

        public void OnButtonClicked()
        {
            SoundManager.Instance.PlaySound(SoundType.ButtonClick);
            palettesShop.StartPurchase(colorPalette);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SoundManager.Instance.PlaySound(SoundType.MouseOverButton);
        }
    }
}
