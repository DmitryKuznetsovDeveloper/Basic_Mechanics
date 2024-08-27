using System;
using DataConfigs;
using UnityEngine;
using Zenject;

namespace Model
{
    public sealed class GameManager : IInitializable, ITickable
    {
        public event Action OnWin;
        public event Action OnLose;
        public event Action OnChangeWheat;
        public event Action OnChangePeasant;
        public event Action OnChangeWarriors;
        public bool IsWin { get; private set; }
        public bool IsLose { get; private set; }

        public int AmountWheat { get; private set; }
        public int AmountEatsWheat { get; private set; }
        public int AmountPeasant { get; private set; }
        public int AmountWarriors { get; private set; }
        public int AmountEnemy { get; private set; }

        private float _wheatHarvestCycle;
        private float _attackCycle;
        private float _eatsCycle;

        private TimerAction _timerActionWheat;
        private TimerAction _timerActionAttack;
        private TimerAction _timerActionEats;

        private readonly GameLoop _gameLoop;

        public GameManager(GameLoop gameLoop) =>
            _gameLoop = gameLoop;

        public void Initialize()
        {
            _timerActionWheat = new TimerAction(WheatCycle, _gameLoop.wheatHarvestCycle);
            _timerActionAttack = new TimerAction(AttackCycle, _gameLoop.attackCycle);
            _timerActionEats = new TimerAction(EatsCycle, _gameLoop.eatsCycle);

            AmountWheat = _gameLoop.startCountWheat;
            AmountPeasant = _gameLoop.startCountPeasant;
            AmountWarriors = _gameLoop.startCountWarriorsOne + _gameLoop.startCountWarriorsTwo;
            AmountEatsWheat = (AmountPeasant * _gameLoop.peasantEatsWheat) +
                              (AmountWarriors * _gameLoop.warriorOneEatsWheat);
            AmountEnemy = _gameLoop.addedCountEnemyNextWave;

            OnChangeWheat?.Invoke();
            OnChangePeasant?.Invoke();
            OnChangeWarriors?.Invoke();
        }

        public void Tick()
        {
            CheckWin();
            CheckLose();
            //Таймеры
            _timerActionWheat.IsTimerFinished(Time.deltaTime);
            _timerActionAttack.IsTimerFinished(Time.deltaTime);
            _timerActionEats.IsTimerFinished(Time.deltaTime);
        }

        /// <summary>
        /// Что происходит после завершения цикла сбора урожая.
        /// </summary>
        private void WheatCycle()
        {
            AmountWheat += AmountPeasant * _gameLoop.peasantGivesAmountWheat;
            OnChangeWheat?.Invoke();
        }

        /// <summary>
        /// Что происходит после завершения цикла еды. поскольку я решил не делать разное потребление пищи у
        /// воина 1 и воина 2, то и так сойдет, но по хорошему это то же нужно учитывать
        /// </summary>
        private void EatsCycle()
        {
            AmountWheat -= AmountEatsWheat;
            HowMuchEats();
            OnChangeWheat?.Invoke();
        }


        /// <summary>
        /// Расчет сколько съедят пшеницы
        /// </summary>
        private void HowMuchEats() =>
            AmountEatsWheat = (AmountPeasant * _gameLoop.peasantEatsWheat) +
                              (AmountWarriors * _gameLoop.warriorOneEatsWheat);

        /// <summary>
        /// Что происходит после завершения цикла атаки.
        /// </summary>
        private void AttackCycle()
        {
            if (AmountWarriors < AmountEnemy) 
                OnLose?.Invoke();
            AmountWarriors -= AmountEnemy;
            AmountEnemy += _gameLoop.addedCountEnemyNextWave;
            OnChangeWarriors?.Invoke();
        }

        /// <summary>
        /// Добавление Крестьян
        /// </summary>
        public void BuyPeasant()
        {
            AmountPeasant++;
            AmountWheat -= _gameLoop.currencyNewPeasant;
            HowMuchEats();
            OnChangeWheat?.Invoke();
            OnChangePeasant?.Invoke();
        }

        /// <summary>
        /// Добавление Воинов
        /// </summary>
        public void BuyWarriorOne()
        {
            AmountWarriors++;
            AmountWheat -= _gameLoop.currencyNewWarriorOne;
            HowMuchEats();
            OnChangeWheat?.Invoke();
            OnChangeWarriors?.Invoke();
        }

        public void BuyWarriorTwo()
        {
            AmountWarriors++;
            AmountWheat -= _gameLoop.currencyNewWarriorTwo;
            HowMuchEats();
            OnChangeWheat?.Invoke();
            OnChangeWarriors?.Invoke();
        }

        /// <summary>
        /// Проверка на победу
        /// </summary>
        private void CheckWin()
        {
            if (AmountPeasant >= _gameLoop.countPeasantsToWin && AmountWarriors >= _gameLoop.countWarriorsToWin)
                OnWin?.Invoke();
        }

        /// <summary>
        /// Проверка на проигрыш
        /// </summary>
        private void CheckLose()
        {
            if (AmountWheat < 0)
                OnLose?.Invoke();
        }
    }
}