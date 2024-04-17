namespace AviGamesTest.Game
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Services.Save;
    using Timer;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Zenject;

    /// <summary>
    /// Обработчик действий с игровых элементов
    /// </summary>
    public class GameController : MonoBehaviour, IGameState, IGameObserver
    {
        private const string LEVEL_SAVE_KEY = "CurrentLevelKey";

        public event Action OnGameRestarted = delegate { };
        public event Action OnGameBegun = delegate { };
        public event Action<bool> OnGameEnd = delegate { };
        public event Action<int> OnLevelChange = delegate { };

        [SerializeField]
        private List<AssetReference> _levels = new List<AssetReference>();

        [SerializeField]
        private Transform _spawnRoot;

        [Inject]
        private ISaveManager _saveManager;

        [Inject]
        private ClickObserver _clickObserver;

        [Inject]
        private ITimer _timer;

        private GameElement _loadedGameElement;

        private GameElement _spawnedGameElement;

        private int _currentLevel;

        /// <summary>
        /// Pre-load game
        /// </summary>
        public async Task LoadGame(bool async)
        {
            _currentLevel = _saveManager.Load<int>(LEVEL_SAVE_KEY);

            if (async)
            {
                var op = Addressables.LoadAssetAsync<GameObject>(_levels[0]); //Заглушка, по тз

                await op.Task;

                _loadedGameElement = op.Result.GetComponent<GameElement>();
                
                return;
            }

            _loadedGameElement = Addressables.LoadAssetAsync<GameObject>(_levels[0]).WaitForCompletion().GetComponent<GameElement>();
        }

        /// <summary>
        /// Starts game / automatically load game 
        /// </summary>
        public async void StartGame()
        {
            if (_loadedGameElement == null)
            {
               await LoadGame(false);
            }

            if (_spawnedGameElement != null)
            {
                _spawnedGameElement.OnStageClear -= OnWinCallback;
                _timer.OnTimerEnd -= OnLooseCallback;
            }

            _spawnedGameElement = Instantiate(_loadedGameElement, _spawnRoot);
            
            _spawnedGameElement.Init(_clickObserver);
            
            _spawnedGameElement.OnStageClear += OnWinCallback;
            
            _timer.StartTimer(_spawnedGameElement.TimeToSolve);

            _timer.OnTimerEnd += OnLooseCallback;

            _clickObserver.StopObserve(false);
            
            OnLevelChange(_currentLevel);
            
            OnGameBegun();
        }
  

        public void RestartGame()
        {
            OnGameRestarted();
           
            _currentLevel++; // По тз
            _saveManager.Save(LEVEL_SAVE_KEY, _currentLevel);
            Destroy(_spawnedGameElement.gameObject);
            StartGame();
        }
        
        private void OnWinCallback()
        {
            EndGame();
            OnGameEnd(true);
        }
        
        private void OnLooseCallback()
        {
            EndGame();
            OnGameEnd(false);
        }

        private void EndGame()
        {
            _clickObserver.StopObserve(true);
            _timer.Stop();
            _timer.OnTimerEnd -= OnLooseCallback;
        }

        private void OnDestroy()
        {
            if (_spawnedGameElement != null)
            {
                _spawnedGameElement.OnStageClear -= OnWinCallback;
                Destroy(_spawnedGameElement.gameObject);
            }
            
            _saveManager.Save(LEVEL_SAVE_KEY, _currentLevel);
        }

    }
}
