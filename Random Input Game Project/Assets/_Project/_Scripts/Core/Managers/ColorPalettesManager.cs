using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [System.Serializable]
    public class ColorPalette
    {
        public int price = 25;
        public bool purchased;
        public bool selected = true;
        public Color color;
        public void Buy()
        {
            purchased = true;
        }
    }

    [System.Serializable]
    public struct Colors
    {
        public Color[] colors;
    }

    public class ColorPalettesManager : Singleton<ColorPalettesManager>
    {
        public List<ColorPalette> colorPalettes;

        public ColorPalette currentPalette;
        [SerializeField] private ColorPalette defaultPalette;

        protected override void Awake()
        {
            base.Awake();

            LevelManager.OnLevelStart += SetNewPalette;
            LevelManager.OnGameStart += SetNewPalette;

            SetupColors();
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= SetNewPalette;
            LevelManager.OnGameStart -= SetNewPalette;
        }

        private void SetupColors()
        {
            SetupPalettesPrices();

            LoadColors();
        }

        private void SetupPalettesPrices()
        {
            foreach (ColorPalette cp in colorPalettes)
                cp.price = 25;
        }

        private void LoadColors()
        {
            foreach (ColorPalette cp in colorPalettes)
            {
                if (!PlayerPrefs.HasKey(cp.color.ToString()))
                    return;
                
                cp.purchased = PlayerPrefs.GetInt(cp.color.ToString()) == 1;
            }
        }

        public void SaveColors()
        {
            foreach (ColorPalette cp in colorPalettes)
            {
                // We save 1 if the color has been purchased and 0 if not.
                // The key is the color converted into a string.
                PlayerPrefs.SetInt(cp.color.ToString(), cp.purchased ? 1 : 0);
            }
        }

        private void SetNewPalette()
        {
            currentPalette = GetRandomPaletteFromSelectedPalettes();
        }

        public ColorPalette GetRandomPaletteFromSelectedPalettes()
        {
            List<ColorPalette> selectedPalettes = new List<ColorPalette>();

            foreach (ColorPalette cp in colorPalettes)
            {
                if (cp.selected && cp.purchased)
                    selectedPalettes.Add(cp);
            }

            if (selectedPalettes.Count == 0)
                return defaultPalette;

            return selectedPalettes[Random.Range(0, selectedPalettes.Count)];
        }

        public Color GetRandomColor()
        {
            if (currentPalette == null)
                return defaultPalette.color;

            return GetRandomPaletteFromSelectedPalettes().color;
        }
    }
}
