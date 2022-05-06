using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [System.Serializable]
    public class ColorPalette
    {
        [HideInInspector] public int price = 25;
        public bool purchased = false;
        public bool selected = true;
        public Colors colors;

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

            SetupPalettesPrices();
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= SetNewPalette;
            LevelManager.OnGameStart -= SetNewPalette;
        }

        private void SetupPalettesPrices()
        {
            foreach (ColorPalette cp in colorPalettes)
                cp.price = 25;
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

        public List<Colors> GetSelectedColors()
        {
            List<Colors> cs = new List<Colors>();

            foreach (ColorPalette cp in colorPalettes)
            {
                if (cp.selected)
                    cs.Add(cp.colors);
            }

            return cs;
        }

        public Color GetRandomColor()
        {
            if (currentPalette.colors.colors.Length == 0)
                return defaultPalette.colors.colors[Random.Range(0, currentPalette.colors.colors.Length)];

            return currentPalette.colors.colors[Random.Range(0, currentPalette.colors.colors.Length)];
        }

        public Color GetRandomColor(Colors colors)
        {
            return colors.colors[Random.Range(0, colors.colors.Length)];
        }

        public Color GetRandomColor(ColorPalette palette)
        {
            return palette.colors.colors[Random.Range(0, palette.colors.colors.Length)];
        }
    }
}
