using DataConfigs;
using Model;
using Sirenix.OdinInspector;
using UnityEngine;
using View;
using Zenject;

public sealed class HUDScreenPresenter : MonoBehaviour, ITickable
{
    [TitleGroup("Indicators", alignment: TitleAlignments.Centered, boldTitle: true)] 
    [SerializeField] private BaseHudIndicatorView indicatorWheat;
    [SerializeField] private BaseHudIndicatorView indicatorPeasants;
    [SerializeField] private BaseHudIndicatorView indicatorWarriors;
    [SerializeField] private BaseHudIndicatorView indicatorEatsWheat;
    [SerializeField] private BaseHudIndicatorView indicatorEnemy;

    [TitleGroup("Buttons", alignment: TitleAlignments.Centered, boldTitle: true)] 
    [SerializeField] private BaseButtonView buttonPeasant;
    [SerializeField] private BaseButtonView buttonWarriorOne;
    [SerializeField] private BaseButtonView buttonWarriorTwo;

    private GameManager _gameManager;
    private GameLoop _gameLoop;

    [Inject]
    public void Construct(GameManager gameManager,GameLoop gameLoop)
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
        _gameManager.OnWheatCycle += SetWheatLabel;
        _gameManager.OnEatsCycle += SetWheatLabel;
        _gameManager.OnAttackCycle += SetWarriorsLabel;
        _gameManager.OnAttackCycle += SetEnemyLabel;
    }

    private void SetWheatLabel() => indicatorWheat.SetLabel(_gameManager.AmountWheat.ToString());
    private void SetWarriorsLabel() => indicatorWarriors.SetLabel(_gameManager.AmountWarriors.ToString());
    private void SetEnemyLabel() => indicatorWarriors.SetLabel(_gameManager.AmountEnemy.ToString());
}