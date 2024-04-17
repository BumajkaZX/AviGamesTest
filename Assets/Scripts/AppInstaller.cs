namespace AviGamesTest
{
    using Game;
    using Game.Timer;
    using NaughtyAttributes;
    using Services.Ad;
    using Services.Save;
    using Services.Shop;
    using UI;
    using UnityEngine;
    using Zenject;

    public class AppInstaller : MonoInstaller
    {
        [InfoBox("Требуемые моноскрипты")]
        
        [SerializeField]
        private ClickObserver _clickObserver;

        [SerializeField]
        private TimerController _timerController;

        [SerializeField]
        private GameController _gameController;

        [SerializeField]
        private MockAdView _mockAdView;
        
        public override void InstallBindings()
        {
            RegisterMockAdView();
            
            RegisterShop();
            RegisterAdManager();
            
            RegisterInput();
            RegisterClickObserver();
            RegisterSaveManager();
            RegisterTimer();
            RegisterGame();
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

        private void RegisterGame()
        {
            Container.BindInterfacesTo<GameController>().FromInstance(_gameController).AsSingle();
        }

        private void RegisterAdManager()
        {
            Container.BindInterfacesTo<AdManager>().AsSingle();
        }

        private void RegisterShop()
        {
            Container.BindInterfacesTo<UnityIAPPurchaser>().AsSingle();
        }

        private void RegisterMockAdView()
        {
            Container.BindInstance(_mockAdView).AsSingle();
        }
    }
}
