using Data;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public sealed class CharacterMoveController : ITickable
    {
        private readonly MoveUserInputData _moveUserInputData;
        private readonly Rigidbody _characterRb;
        private readonly float _power;

        public CharacterMoveController(MoveUserInputData moveUserInputData, Rigidbody characterRb, float speed)
        {
            _moveUserInputData = moveUserInputData;
            _characterRb = characterRb;
            _power = speed;
        }


        public void Tick()
        {
            var direction =
                new Vector3(_moveUserInputData.MoveInputData.x, 0f, _moveUserInputData.MoveInputData.y) *
                (_power * Time.deltaTime);
            _characterRb.AddForce(direction);
        }
    }
}