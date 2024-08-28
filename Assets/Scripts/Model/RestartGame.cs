using DataConfigs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Model
{
    public sealed class RestartGame : IInitializable, ITickable
    {
        private readonly GameManager _gameManager;
        private readonly GameLoop _gameLoop;

        private float _reloadTime;
        private float _time;

        public RestartGame(GameManager gameManager, GameLoop gameLoop)
        {
            _gameManager = gameManager;
            _gameLoop = gameLoop;
        }

        public void Initialize()
        {
            _reloadTime = _gameLoop.restartTime;
        }

        public void Tick()
        {
            if (_gameManager.IsLose || _gameManager.IsWin)
            {
                _time += Time.unscaledDeltaTime;
                if (_time >= _reloadTime)
                {
                    ReloadScene();
                    _time = 0;
                }   
            }
        }

        private void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}