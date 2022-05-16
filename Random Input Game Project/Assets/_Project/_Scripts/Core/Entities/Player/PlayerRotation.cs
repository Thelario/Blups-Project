using System.Collections;
using Game.Managers;
using UnityEngine;

namespace Game.Entities
{
    public class PlayerRotation : Player
    {
        [Header("Player Rotation Speed")]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float pcRotationSpeedModifier;

        [Header("Player Rotation References")]
        [SerializeField] protected SpriteRenderer baseRenderer;
        [SerializeField] protected SpriteRenderer middleRenderer;
        
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        private void Start()
        {
            rotationSpeed += pcRotationSpeedModifier;
        }
        #endif

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

        protected override IEnumerator PlayerDies()
        {
            GameManager.Instance.PlayerDies();
            invincible = true;
            particles.Stop();
            spRenderer.enabled = false;
            baseRenderer.enabled = false;
            middleRenderer.enabled = false;
            rb2D.velocity = Vector2.zero;

            yield return new WaitForSecondsRealtime(timePassedWhenHit);

            particles.Play();
            spRenderer.enabled = true;
            baseRenderer.enabled = true;
            middleRenderer.enabled = true;
            invincible = false;
            GameManager.Instance.PlayerRevive();
        }
    }
}
