using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerRotation : Player
    {
        [SerializeField] private float rotationSpeed;

        private float _rotationLimit = 70f;

        private void Rotate()
        {
            float zAngles = _horizontal * rotationSpeed * Time.deltaTime;

            _transform.Rotate(zAngles * Vector3.forward);
        }

        protected override void GetMoveInput()
        {
            _horizontal = -InputManager.Instance.GetHorizontalInput();
        }

        protected override void Move()
        {
            Rotate();
        }
    }
}
