using System.Collections.Generic;

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
