using UnityEngine;
using Zenject;

namespace Triggers
{
    public sealed class ResetSceneTrigger : MonoBehaviour
    {

        private GameLoader _gameLoader;

        [Inject]
        public void Construct(GameLoader gameLoader) => _gameLoader = gameLoader;

        private readonly string _player = "Player";
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_player)) 
                _gameLoader.ResetCurrentScene();
        }
    }
}
