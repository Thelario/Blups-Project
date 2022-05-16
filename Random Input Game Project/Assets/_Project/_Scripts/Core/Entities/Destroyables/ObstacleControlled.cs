using Game.Entities.Helpers;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class ObstacleControlled : MonoBehaviour
    {
        [SerializeField] private Speed moveSpeed;
        [SerializeField] private Rigidbody2D rb2D;
        
        private float _verticalRaw;

        private void Awake()
        {
            DifficultyManager.OnDifficultyChange += ChangeDifficulty;
        }

        private void OnDestroy()
        {
            DifficultyManager.OnDifficultyChange -= ChangeDifficulty;
        }

        private void Update()
        {
            GetMoveInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void GetMoveInput()
        {
            _verticalRaw = InputManager.Instance.GetVerticalInput();
        }

        private void Move()
        {
            rb2D.velocity = Time.fixedDeltaTime * moveSpeed.moveSpeed * new Vector2(-1f, _verticalRaw).normalized;
        }

        private void ChangeDifficulty()
        {
            moveSpeed.ChangeDifficulty();
        }
    }
}
