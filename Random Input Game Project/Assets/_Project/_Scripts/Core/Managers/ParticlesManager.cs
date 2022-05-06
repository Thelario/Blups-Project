using UnityEngine;

namespace Game.Managers
{
    public class ParticlesManager : Singleton<ParticlesManager>
    {
        [SerializeField] private Particle[] particles;

        private Transform _transform;

        protected override void Awake()
        {
            base.Awake();

            _transform = transform;
        }

        public void CreateParticle(ParticleType type, Vector3 position)
        {
            Instantiate(SearchParticle(type), position, Quaternion.identity, _transform);
        }

        private GameObject SearchParticle(ParticleType type)
        {
            foreach (Particle p in particles)
            {
                if (p.type == type)
                    return p.particlePrefab;
            }

            Debug.LogError("No particle found with type: " + type);
            return null;
        }
    }

    [System.Serializable]
    public class Particle
    {
        public ParticleType type;
        public GameObject particlePrefab;
    }

    public enum ParticleType
    {
        CoinObtained,
        SecretObtained
    }
}
