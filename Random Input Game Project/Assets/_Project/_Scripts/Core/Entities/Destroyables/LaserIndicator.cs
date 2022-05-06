using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class LaserIndicator : ObjectIndicator
    {
        [SerializeField] private float timeToWaitToSpawnLaser;

        [SerializeField] private ParticleSystem particles;

        protected override void Awake()
        {
            base.Awake();

            _renderer.color = new Color(_color.r, _color.g, _color.b, 0.25f);
            particles.startColor = _renderer.color;
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