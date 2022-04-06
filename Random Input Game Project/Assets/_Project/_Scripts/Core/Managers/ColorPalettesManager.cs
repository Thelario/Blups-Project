using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [System.Serializable]
    public class ColorPalette
    {
        public int price = 25;
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

        protected override void Awake()
        {
            base.Awake();

            LevelManager.OnLevelStart += SetNewPalette;
            LevelManager.OnGameStart += SetNewPalette;
        }

        private void OnDestroy()
        {
            LevelManager.OnLevelStart -= SetNewPalette;
            LevelManager.OnGameStart -= SetNewPalette;
        }

        private void SetNewPalette()
        {
            currentPalette = GetRandomPaletteFromSelectedPalettes();
        }

        public ColorPalette GetRandomPaletteFromSelectedPalettes()
        {
            // TODO: having to create another list everytime we try to get a random palette might not be
            //       the most efficient thing to do. I should probably optimize this code.

            List<ColorPalette> selectedPalettes = new List<ColorPalette>();

            foreach (ColorPalette cp in colorPalettes)
            {
                if (cp.selected)
                    selectedPalettes.Add(cp);
            }

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
