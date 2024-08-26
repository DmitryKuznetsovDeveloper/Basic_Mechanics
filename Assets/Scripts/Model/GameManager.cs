using System;
using DataConfigs;
using UnityEngine;
using Zenject;

namespace Model
{
    public sealed class GameManager : IInitializable, ITickable
    {
        public event Action OnWheatCycle;
        public event Action OnAttackCycle;
        public event Action OnEatsCycle;
        public bool IsWin { get; private set; }
        public bool IsLose { get; private set; }

        public int AmountWheat { get; private set; }
        public int AmountPeasant { get; private set; }
        public int AmountWarriors { get; private set; }
        public int AmountEnemy { get; private set; }

        private float _wheatHarvestCycle;
        private float _attackCycle;
        private float _eatsCycle;
        private readonly GameLoop _gameLoop;

        public GameManager(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }

        public void Initialize()
        {
            AmountWheat = _gameLoop.startCountWheat;
            AmountPeasant = _gameLoop.startCountPeasant;
            AmountWarriors = _gameLoop.startCountWarriorsOne + _gameLoop.startCountWarriorsTwo;
            AmountEnemy = _gameLoop.addedCountEnemyNextWave;
        }

        public void Tick()
        {
            // Условие победы
            IsWin = AmountPeasant >= _gameLoop.countPeasantsToWin && AmountWarriors >= _gameLoop.countWarriorsToWin;

            // Цикл сбора урожая
            _wheatHarvestCycle = TimeCycleCountdown(_wheatHarvestCycle, _gameLoop.wheatHarvestCycle);
            if (_wheatHarvestCycle == 0)
            {
                AmountWheat += AmountPeasant * _gameLoop.peasantGivesAmountWheat;
                OnWheatCycle?.Invoke();
                Debug.Log(_wheatHarvestCycle);
                Debug.Log("AmountWheat " + AmountWheat);
            }

            // Цикл атаки волны
            _attackCycle = TimeCycleCountdown(_attackCycle, _gameLoop.attackCycle);
            if (_attackCycle == 0)
            {
                AmountWarriors -= AmountEnemy;
                IsLose = AmountWarriors < AmountEnemy;
                AmountEnemy += _gameLoop.addedCountEnemyNextWave;
                OnAttackCycle?.Invoke();
            }

            // Цикл траты еды
            _eatsCycle = TimeCycleCountdown(_eatsCycle, _gameLoop.eatsCycle);
            if (_eatsCycle == 0)
            {
                //TODO: посколько я решил не делать разгные потребление пищи у
                //TODO: воина 1 и воина 2, то и так сойдет, но по хорошему это то же нужно учитывать
                AmountWheat -= (AmountPeasant * _gameLoop.peasantEatsWheat) +
                               (AmountWarriors * _gameLoop.warriorOneEatsWheat);
                OnEatsCycle?.Invoke();
            }
        }

        /// <summary>
        /// Метод отсчета времени
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private float TimeCycleCountdown(float start, float end) => start <= end ? start += Time.deltaTime : start = 0f;
    }
}