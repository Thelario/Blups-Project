using Game.Entities.Helpers;
using UnityEngine;

namespace Game.Entities
{
    public class Sword : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Obstacle"))
            {
                other.GetComponent<IDamageable>().TakeDamage();
            }
        }
    }
}
