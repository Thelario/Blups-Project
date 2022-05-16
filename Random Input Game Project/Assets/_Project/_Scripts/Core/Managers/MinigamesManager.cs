using System.Collections.Generic;
using UnityEngine;

namespace Game.Managers
{
    [System.Serializable]
    public class Minigame
    {
        public Scenes minigame;
        public bool purchased = false;
        public int price = 25;
        public string name;

        public void Buy()
        {
            purchased = true;
        }
    }

    public class MinigamesManager : Singleton<MinigamesManager>
    {
        public List<Minigame> minigames;

        protected override void Awake()
        {
            base.Awake();
            
            SaveMinigames();
            LoadMinigames();
        }

        private void SaveMinigames()
        {
            foreach (Minigame m in minigames)
            {
                PlayerPrefs.SetInt(m.name, m.purchased ? 1 : 0);
            }
            
            PlayerPrefs.Save();
        }

        private void LoadMinigames()
        {
            foreach (Minigame m in minigames)
            {
                if (!PlayerPrefs.HasKey(m.name))
                    return;
                
                m.purchased = PlayerPrefs.GetInt(m.name) == 1;
            }
        }
        
        public bool CheckMinigamePurchased(Scenes scene)
        {
            foreach (Minigame m in minigames)
            {
                if (scene != m.minigame)
                    continue;

                return m.purchased;
            }

            return false;
        }

        public bool CheckMinigamePurchased(int scene)
        {
            return CheckMinigamePurchased((Scenes)scene);
        }
    }
}
