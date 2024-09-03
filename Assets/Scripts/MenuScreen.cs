using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button startBtn;
    [SerializeField] private Button exitBtn;

    private GameLoader _gameLoader;
    
    [Inject]
    public void Construct(GameLoader gameLoader) => _gameLoader = gameLoader;

    private void OnEnable()
    {
        startBtn.onClick.AddListener(_gameLoader.LoadNextScene);
        exitBtn.onClick.AddListener(_gameLoader.GameExit);
    }
    private void OnDestroy()
    {
        startBtn.onClick.RemoveListener(_gameLoader.LoadNextScene);
        exitBtn.onClick.RemoveListener(_gameLoader.GameExit);
    }
}
