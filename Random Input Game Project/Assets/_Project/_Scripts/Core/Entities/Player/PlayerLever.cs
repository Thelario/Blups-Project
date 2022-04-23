using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerLever : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private float _horizontal;
        private float _rotationLimit = 70f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            _horizontal = -InputManager.Instance.GetHorizontalInput();

            Rotate();
        }

        private void Rotate()
        {
            float zAngles = _horizontal * rotationSpeed * Time.deltaTime;

            /*
            if (_transform.rotation.eulerAngles.z >= _rotationLimit)
            {
                if (_horizontal < 0f)
                    return;
            }
            else if (_transform.rotation.eulerAngles.z <= -_rotationLimit)
            {
                if (_horizontal > 0f)
                    return;
            }
            */

            _transform.Rotate(zAngles * Vector3.forward);
        }
    }
}
