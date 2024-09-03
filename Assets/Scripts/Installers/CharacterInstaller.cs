using Controllers;
using Data;
using Input;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private float speedMove;
        [SerializeField] private Rigidbody rigidBodyCharacter;
        public override void InstallBindings()
        {
            //Data
            Container.Bind<MoveUserInputData>().AsCached().NonLazy();
            
            //Input
            Container.BindInterfacesTo<MoveUserInputSystem>().AsSingle().NonLazy();
            
            //Controller
            Container.BindInterfacesAndSelfTo<CharacterMoveController>().AsSingle().WithArguments(rigidBodyCharacter,speedMove).NonLazy();
        }
    }
}