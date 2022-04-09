using Game.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class PaletteItemVaultUI : MonoBehaviour
    {
        public Image[] colors;
        [HideInInspector] public ColorPalette colorPalette;
        [SerializeField] private Toggle toggle;
        public ToggleBehaviour toggleBehaviour;

        public void Toggle()
        {
            colorPalette.selected = toggle.isOn;
        }
    }
}
