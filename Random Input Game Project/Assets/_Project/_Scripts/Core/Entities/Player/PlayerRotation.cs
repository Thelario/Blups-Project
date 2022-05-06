using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerRotation : Player
    {
        [Header("Speed")]
        [SerializeField] private float rotationSpeed;

        private void Rotate()
        {
            float zAngles = horizontalRaw * rotationSpeed * Time.fixedDeltaTime;

            thisTransform.Rotate(zAngles * Vector3.forward);
        }

        protected override void GetMoveInput()
        {
            horizontalRaw = -InputManager.Instance.GetHorizontalInput();
        }

        protected override void Move()
        {
            Rotate();
        }
    }
}
