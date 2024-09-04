using Controllers;
using Data;
using DefaultNamespace;
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
            
            Container.Bind<HealthComponent>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<CharacterDeathObservable>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}