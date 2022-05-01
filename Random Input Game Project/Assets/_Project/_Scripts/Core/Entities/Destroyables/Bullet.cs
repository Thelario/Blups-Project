using Game.Entities.Helpers;
using UnityEngine;

namespace Game.Entities
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;

        [SerializeField] private Rigidbody2D rb2D;

        private void FixedUpdate()
        {
            rb2D.velocity = moveSpeed * Time.fixedDeltaTime * Vector2.right;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Obstacle"))
            {
                collision.GetComponent<IDamageable>().TakeDamage();
                Destroy(gameObject);
            }
        }
    }
}
