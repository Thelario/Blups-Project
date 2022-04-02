using UnityEngine;

namespace Game.Managers
{
    public class ExclamationManager : MonoBehaviour
    {
        [SerializeField] private GameObject exclamationPrerfab;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void CreateExclamation(Vector3 spawnPoint, Direction direction)
        {
            Destroy(Instantiate(exclamationPrerfab, GetRealPosition(spawnPoint, direction), Quaternion.identity, _transform), 1f);
        }

        private Vector3 GetRealPosition(Vector3 spawnPoint, Direction direction)
        {
            Vector3 position = spawnPoint;

            switch (direction)
            {
                case Direction.Left:
                    position += new Vector3(1.75f, 0f, 0f);
                    break;
                case Direction.Right:
                    position += new Vector3(-1.75f, 0f, 0f);
                    break;
                case Direction.Up:
                    position += new Vector3(0f, -1.75f, 0f);
                    break;
                case Direction.Down:
                    position += new Vector3(0f, 1.75f, 0f);
                    break;
            }

            return position;
        }
    }
}
