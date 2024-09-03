using UnityEngine;
using Random = System.Random;

namespace Triggers
{
    public sealed class AnimationTrapTrigger : MonoBehaviour
    {
        private readonly string _player = "Player";
        private static readonly int MoveSide = Animator.StringToHash("MoveSide");
        private static readonly int Scale = Animator.StringToHash("Scale");
        private Animator _animator;
        private Random _rnd;

        private void Awake()
        {
            _animator = GetComponentInParent<Animator>();
            _rnd = new Random();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_player))
            {
                var temp = _rnd.Next(0, 2);
                if (temp > 0)
                {
                    _animator.SetBool(MoveSide, true);
                    _animator.SetBool(Scale, false);
                }
                else
                {
                    _animator.SetBool(Scale, true);
                    _animator.SetBool(MoveSide, false);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(_player))
            {
                _animator.SetBool(Scale, false);
                _animator.SetBool(MoveSide, false);
            }
        }
    }
}