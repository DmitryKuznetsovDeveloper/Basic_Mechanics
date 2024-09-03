using System;
using Data;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Input
{
    public sealed class MoveUserInputSystem : IInitializable,ITickable, IDisposable
    {
        private InputAction _moveActon;
        private float2 _moveInput;

        private readonly MoveUserInputData _inputData;
        public MoveUserInputSystem(MoveUserInputData inputData) => _inputData = inputData;

        void IInitializable.Initialize()
        {
            _moveActon = new InputAction("move", binding: "<Gamepad>/LeftStick");
            _moveActon.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            _moveActon.started += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveActon.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveActon.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
            _moveActon.Enable();
        }

        void ITickable.Tick() =>
            _inputData.MoveInputData = _moveInput;

        void IDisposable.Dispose() =>
            _moveActon?.Dispose();
    }
}