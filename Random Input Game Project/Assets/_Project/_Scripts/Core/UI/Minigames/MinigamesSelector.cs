using UnityEngine;
using Game.Managers;
using TMPro;

namespace Game.UI
{
    public class MinigamesSelector : MonoBehaviour
    {
        [SerializeField] private GameObject minigameButtonPrefab;
        [SerializeField] private GameObject lockedMinigamePrefab;
        [SerializeField] private Transform minigamesParent;

        private void OnEnable()
        {
            CreateMinigameButtons();
        }

        public void CreateMinigameButtons()
        {
            DestroyMinigameButtons();

            foreach (Minigame m in MinigamesManager.Instance.minigames)
            {
                if (m.purchased)
                {
                    GameObject g = Instantiate(minigameButtonPrefab, minigamesParent);
                    MinigameUI mui = g.GetComponent<MinigameUI>();
                    mui.tmpText.text = m.name;
                    mui.mButton.sceneToLoad = m.minigame;
                }
                else
                {
                    Instantiate(lockedMinigamePrefab, minigamesParent);
                }
            }
        }

        public void DestroyMinigameButtons()
        {
            foreach (Transform t in minigamesParent)
                Destroy(t.gameObject);
        }
    }
}
