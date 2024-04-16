namespace AviGamesTest
{
    using Game.Timer;
    using NaughtyAttributes;
    using Save;
    using UnityEngine;
    using Zenject;

    public class AppInstaller : MonoInstaller
    {
        [InfoBox("Требуемые моноскрипты")]
        
        [SerializeField]
        private ClickObserver _clickObserver;

        [SerializeField]
        private TimerController _timerController;
        
        public override void InstallBindings()
        {
            RegisterInput();
            RegisterClickObserver();
            RegisterSaveManager();
            RegisterTimer();
        }

        private void RegisterInput()
        {
            var playerInput = new PlayerInput();
            playerInput.Enable();

            Container.BindInstance(playerInput).AsSingle();
        }

        private void RegisterClickObserver()
        {
            Container.BindInstance(_clickObserver).AsSingle();
        }

        private void RegisterSaveManager()
        {
            Container.Bind<ISaveManager>().FromInstance(new MockSaveManager()).AsSingle();
        }

        private void RegisterTimer()
        {
            Container.Bind<ITimer>().FromInstance(_timerController).AsSingle();
        }
    }
}
