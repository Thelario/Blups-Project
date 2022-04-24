using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class LaserIndicator : ObjectIndicator
    {
        [SerializeField] private float timeToWaitToSpawnLaser;

        [SerializeField] private SpriteRenderer spriteRendererLeft;
        [SerializeField] private SpriteRenderer spriteRendererRight;

        protected override void Awake()
        {
            base.Awake();

            spriteRendererLeft.color = new Color(Mathf.Clamp01(_renderer.color.r - 0.3f), Mathf.Clamp01(_renderer.color.g - 0.3f), Mathf.Clamp01(_renderer.color.b - 0.3f), _renderer.color.a);
            spriteRendererRight.color = new Color(Mathf.Clamp01(_renderer.color.r - 0.3f), Mathf.Clamp01(_renderer.color.g - 0.3f), Mathf.Clamp01(_renderer.color.b - 0.3f), _renderer.color.a);
        }

        protected override IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(timeToWaitToSpawnLaser);

            GameObject g = Instantiate(objectPrefab, _transform.position, _transform.rotation);
            g.GetComponent<Laser>().SetColor(_color);
            Destroy(gameObject, 0f);
        }
    }
}