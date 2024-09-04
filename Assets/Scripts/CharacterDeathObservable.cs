using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CharacterDeathObservable : MonoBehaviour
    {
        [SerializeField] private  ParticleSystem vfx;
        private  HealthComponent _healthComponent;
        private  GameLoader _gameLoader;

        [Inject]
        public void Construct(HealthComponent healthComponent, GameLoader gameLoader)
        {
            _healthComponent = healthComponent;
            _gameLoader = gameLoader;
        }


        public void OnEnable() => 
            _healthComponent.OnDeath += CharacterDeath;

        public void OnDisable() => 
            _healthComponent.OnDeath -= CharacterDeath;

        private void CharacterDeath()
        {
            vfx.Play();
            StartCoroutine(_gameLoader.ResetCurrentSceneByTime(2f));
        }
    }
}