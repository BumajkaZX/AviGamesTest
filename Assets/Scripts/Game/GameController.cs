namespace AviGamesTest.Game
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Timer;
    using UnityEngine;
    using UnityEngine.AddressableAssets;
    using Zenject;

    /// <summary>
    /// Обработчик действий с игровых элементов
    /// </summary>
    public class GameController : MonoBehaviour
    {
        private const string LEVEL_SAVE_KEY = "CurrentLevelKey";
        
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
                var op = Addressables.LoadAssetAsync<GameObject>(_levels[_currentLevel]);

                await op.Task;

                _loadedGameElement = op.Result.GetComponent<GameElement>();
                
                return;
            }
            
            Debug.LogError(_currentLevel);
            _loadedGameElement = Addressables.LoadAssetAsync<GameObject>(_levels[_currentLevel]).WaitForCompletion().GetComponent<GameElement>();
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

            _spawnedGameElement = Instantiate(_loadedGameElement, _spawnRoot);
            _spawnedGameElement.Init(_clickObserver);
            
            _timer.StartTimer(_spawnedGameElement.TimeToSolve);
        }

        public void RestartGame()
        {
            Destroy(_spawnedGameElement.gameObject);
            StartGame();
        }
    }
}
