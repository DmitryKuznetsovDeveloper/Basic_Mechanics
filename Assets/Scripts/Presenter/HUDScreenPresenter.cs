using DataConfigs;
using Model;
using Sirenix.OdinInspector;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public sealed class HUDScreenPresenter : MonoBehaviour, ITickable
    {
        [TitleGroup("Indicators", alignment: TitleAlignments.Centered, boldTitle: true)] [SerializeField]
        private BaseHudIndicatorView indicatorWheat;

        [SerializeField] private BaseHudIndicatorView indicatorPeasants;
        [SerializeField] private BaseHudIndicatorView indicatorWarriors;
        [SerializeField] private BaseHudIndicatorView indicatorEatsWheat;
        [SerializeField] private BaseHudIndicatorView indicatorEnemy;
        
        [TitleGroup("Buttons", alignment: TitleAlignments.Centered, boldTitle: true)] 
        [SerializeField] private BaseButtonView buttonPeasant;
        [SerializeField] private BaseButtonView buttonWarriorOne;
        [SerializeField] private BaseButtonView buttonWarriorTwo;
        [SerializeField] private ButtonExitButtonView buttonExit;

        private GameManager _gameManager;
        private GameLoop _gameLoop;

        [Inject]
        public void Construct(GameManager gameManager, GameLoop gameLoop)
        {
            _gameManager = gameManager;
            _gameLoop = gameLoop;
        }

        public void Tick()
        {
            indicatorWheat.SetTimeFillImage(_gameLoop.wheatHarvestCycle);
            indicatorEatsWheat.SetTimeFillImage(_gameLoop.eatsCycle);
            indicatorEnemy.SetTimeFillImage(_gameLoop.attackCycle);
        }
        

        private void OnEnable()
        {
            _gameManager.OnChangeWheat += SetWheatLabel;
            _gameManager.OnChangeWheat += SetWheatEatsLabel;
            _gameManager.OnChangeWheat += CheckEnoughMoney;

            _gameManager.OnChangeWarriors += SetEnemyLabel;
            _gameManager.OnChangeWarriors += SetWarriorsLabel;
            _gameManager.OnChangeWarriors += SetWarriorsFill;

            _gameManager.OnChangePeasant += SetPeasantsLabel;
            _gameManager.OnChangePeasant += SetPeasantsFill;

            buttonPeasant.Button.onClick.AddListener(OnClickButtonPeasants);
            buttonPeasant.SetLabel(_gameLoop.currencyNewPeasant.ToString());
            buttonPeasant.SetImageCharacter(_gameLoop.peasantImage);

            buttonWarriorOne.Button.onClick.AddListener(OnClickButtonWarriorOne);
            buttonWarriorOne.SetLabel(_gameLoop.currencyNewWarriorOne.ToString());
            buttonWarriorOne.SetImageCharacter(_gameLoop.warriorOneImage);

            buttonWarriorTwo.Button.onClick.AddListener(OnClickButtonWarriorTwo);
            buttonWarriorTwo.SetLabel(_gameLoop.currencyNewWarriorTwo.ToString());
            buttonWarriorTwo.SetImageCharacter(_gameLoop.warriorTwoImage);
            
            buttonExit.Button.onClick.AddListener(OnClickButtonExit);
        }

        private void OnDisable()
        {
            _gameManager.OnChangeWheat -= SetWheatLabel;
            _gameManager.OnChangeWheat -= SetWheatEatsLabel;
            _gameManager.OnChangeWheat -= CheckEnoughMoney;

            _gameManager.OnChangeWarriors -= SetEnemyLabel;
            _gameManager.OnChangeWarriors -= SetWarriorsLabel;
            _gameManager.OnChangeWarriors -= SetWarriorsFill;

            _gameManager.OnChangePeasant -= SetPeasantsLabel;
            _gameManager.OnChangePeasant -= SetPeasantsFill;

            buttonPeasant.Button.onClick.RemoveListener(OnClickButtonPeasants);
            buttonWarriorOne.Button.onClick.RemoveListener(OnClickButtonWarriorOne);
            buttonWarriorTwo.Button.onClick.RemoveListener(OnClickButtonWarriorTwo);
            
            buttonExit.Button.onClick.RemoveListener(OnClickButtonExit);
        }

        private void SetWheatLabel() => indicatorWheat.SetLabel(_gameManager.AmountWheat.ToString());
        private void SetWheatEatsLabel() => indicatorEatsWheat.SetLabel(_gameManager.AmountEatsWheat.ToString());
        private void SetPeasantsLabel() => indicatorPeasants.SetLabel(_gameManager.AmountPeasant.ToString());

        private void SetPeasantsFill() =>
            indicatorPeasants.SetFillImage(_gameManager.AmountPeasant, _gameLoop.countPeasantsToWin);

        private void SetWarriorsLabel() => indicatorWarriors.SetLabel(_gameManager.AmountWarriors.ToString());

        private void SetWarriorsFill() =>
            indicatorWarriors.SetFillImage(_gameManager.AmountWarriors, _gameLoop.countWarriorsToWin);

        private void SetEnemyLabel() => indicatorEnemy.SetLabel(_gameManager.AmountEnemy.ToString());

        private void OnClickButtonPeasants() =>
            buttonPeasant.ButtonAnimOnClick(_gameLoop.currencyNewPeasant, _gameManager.BuyPeasant);

        private void OnClickButtonWarriorOne() =>
            buttonWarriorOne.ButtonAnimOnClick(_gameLoop.currencyNewWarriorOne, _gameManager.BuyWarriorOne);

        private void OnClickButtonWarriorTwo() =>
            buttonWarriorTwo.ButtonAnimOnClick(_gameLoop.currencyNewWarriorTwo, _gameManager.BuyWarriorTwo);

        private void OnClickButtonExit() =>  buttonExit.ButtonAnimOnClick(_gameManager.ExitGame);
        private void CheckEnoughMoney()
        {
            buttonPeasant.SwitchEnableButton(_gameManager.AmountWheat - _gameLoop.currencyNewPeasant >= 0);
            buttonWarriorOne.SwitchEnableButton(_gameManager.AmountWheat - _gameLoop.currencyNewWarriorOne >= 0);
            buttonWarriorTwo.SwitchEnableButton(_gameManager.AmountWheat - _gameLoop.currencyNewWarriorTwo >= 0);
        }
    }
}