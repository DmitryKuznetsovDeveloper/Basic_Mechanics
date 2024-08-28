using System;

namespace Model
{
    public class TimerAction
    {
        private readonly Action _action;
        private readonly float _timerTime;
        private float _currentTime;

        public TimerAction(Action action, float timerTime)
        {
            _action = action;
            _timerTime = timerTime;
            _currentTime = _timerTime;
        }

        /// <summary>
        /// Отсчитывает по заданному в конструкторе параметру времени и по
        /// истечению таймера выполняет action и перезапускается
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool IsTimerFinished(float deltaTime)
        {
            if (_currentTime > 0)
            {
                _currentTime -= deltaTime;
                return false;
            }
            _action?.Invoke();
            _currentTime = _timerTime;
            return true;
        }
    }
}