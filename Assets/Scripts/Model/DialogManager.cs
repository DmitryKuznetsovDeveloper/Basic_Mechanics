using DataConfigs;
using Sirenix.OdinInspector;
using UnityEngine;
using View;
using Zenject;
using Random = System.Random;

namespace Model
{
    public class DialogManager : MonoBehaviour, ITickable
    {
        [SerializeField] private float timeChangeDialog;

        [FoldoutGroup("Welcome Dialog Configs")] [SerializeField]
        private DialogsData[] welcomePlayer;

        [FoldoutGroup("Attack Dialog Configs")] [SerializeField]
        private DialogsData[] attackEnemy;

        [FoldoutGroup("Dialog Configs")] [SerializeField]
        private DialogsData winPlayer;

        [FoldoutGroup("Dialog Configs")] [SerializeField]
        private DialogsData losePlayer;

        private float _time;
        private int _indexWelcome;
        private GameManager _gameManager;
        private BasePopupView _basePopup;

        [Inject]
        public void Construct(GameManager gameManager, BasePopupView basePopupView)
        {
            _gameManager = gameManager;
            _basePopup = basePopupView;
        }

        private void OnEnable()
        {
            _basePopup.ButtonOk.onClick.AddListener(_basePopup.HideBasePopup);
            _gameManager.OnWin += WinPopup;
            _gameManager.OnLose += LosePopup;
            _gameManager.OnAttack += AttackPopup;
        }

        public void Tick()
        {
            if (_indexWelcome < welcomePlayer.Length)
            {
                _time += Time.unscaledDeltaTime;
                if (_indexWelcome == 0)
                {
                    _basePopup.ShowBasePopup(welcomePlayer[_indexWelcome].titleLabel, welcomePlayer[_indexWelcome].descriptionLabel,
                        welcomePlayer[_indexWelcome].characterImage);
                    _indexWelcome++;
                }

                if (_time >= timeChangeDialog)
                {
                    _basePopup.DialogChangeBasePopup(welcomePlayer[_indexWelcome].titleLabel, welcomePlayer[_indexWelcome].descriptionLabel,
                        welcomePlayer[_indexWelcome].characterImage);
                    _indexWelcome++;
                    _time = 0;
                }

                if (_indexWelcome == welcomePlayer.Length -1) 
                    _basePopup.ShowButtonContinue();
            }
        }

        private void WinPopup()
        {
            _basePopup.HideButtonContinue();
            _basePopup.ShowBasePopup(winPlayer.titleLabel, winPlayer.descriptionLabel, winPlayer.characterImage);
        }

        private void LosePopup()
        {
            _basePopup.HideButtonContinue();
            _basePopup.ShowBasePopup(losePlayer.titleLabel, losePlayer.descriptionLabel, losePlayer.characterImage);
        }

        private void AttackPopup()
        {
            if (!_gameManager.IsLose)
            {
                Random rnd = new Random();
                int index = rnd.Next(0, attackEnemy.Length);
                _basePopup.ShowBasePopup(attackEnemy[index].titleLabel, attackEnemy[index].descriptionLabel, attackEnemy[index].characterImage);
                _basePopup.ShowButtonContinue();
            }
        }

        private void OnDisable()
        {
            _basePopup.ButtonOk.onClick.RemoveListener(_basePopup.HideBasePopup);
            _gameManager.OnWin -= WinPopup;
            _gameManager.OnLose -= LosePopup;
            _gameManager.OnAttack -= AttackPopup;
        }
    }
}