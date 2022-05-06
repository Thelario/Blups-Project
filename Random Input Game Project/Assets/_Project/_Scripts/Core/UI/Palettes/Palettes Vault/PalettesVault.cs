using Game.Managers;
using UnityEngine;

namespace Game.UI
{
    public class PalettesVault : MonoBehaviour
    {
        [SerializeField] private GameObject unlockedPalettePrefab;
        [SerializeField] private GameObject lockedPalettePrefab;
        [SerializeField] private Transform palettesParent;

        private void OnEnable()
        {
            CreateItems();
        }

        private void OnDisable()
        {
            DestroyItems();
        }

        public void CreateItems()
        {
            foreach (ColorPalette cp in ColorPalettesManager.Instance.colorPalettes)
            {
                if (!cp.purchased)
                {
                    Instantiate(lockedPalettePrefab, palettesParent);
                    continue;
                }
                
                GameObject palette = Instantiate(unlockedPalettePrefab, palettesParent);
                PaletteItemVaultUI piui = palette.GetComponent<PaletteItemVaultUI>();
                piui.color.color = cp.color;
            }
        }

        public void DestroyItems()
        {
            foreach (Transform t in palettesParent)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
