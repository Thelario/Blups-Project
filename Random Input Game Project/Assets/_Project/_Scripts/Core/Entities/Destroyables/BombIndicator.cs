using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class BombIndicator : ObjectIndicator
    {
        private Vector3 _scale;

        protected override void Awake()
        {
            base.Awake();

            float f = Random.Range(0.9f, 1.2f);
            _scale = new Vector3(f, f, f);
            transform.localScale = Vector3.zero;

            AnimateBombIndicator();
        }

        protected override IEnumerator SpawnObject()
        {
            yield return new WaitForSeconds(3f);

            GameObject g = Instantiate(objectPrefab, _transform.position, _transform.rotation);
            g.GetComponent<Bomb>().SetupBomb(_color, _scale);
        }

        private void AnimateBombIndicator()
        {
            LeanTween.scale(gameObject, _scale, 1f);
        }
    }
}
