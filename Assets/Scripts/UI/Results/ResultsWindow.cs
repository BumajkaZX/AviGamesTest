namespace AviGamesTest.UI
{
    using Game;
    using UnityEngine;
    using Zenject;
    
    public class ResultsWindow : MonoBehaviour
    {
        [SerializeField]
        private Transform _winWindow;

        [SerializeField]
        private Transform _looseWindow;

        [Inject]
        private IGameObserver _gameObserver;

        private bool _isOpen;

        private void Awake()
        {
            _gameObserver.OnGameEnd += ShowWindow;
            _gameObserver.OnGameBegun += Close;
            
            if (!_isOpen)
            {
                gameObject.SetActive(false);
            }
        }

        public void ShowWindow(bool isWin)
        {
            _isOpen = true;
            gameObject.SetActive(true);

            _winWindow.gameObject.SetActive(isWin);
            _looseWindow.gameObject.SetActive(!isWin);
        }
        public void Close()
        {
            _isOpen = false;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _gameObserver.OnGameEnd -= ShowWindow;
            _gameObserver.OnGameBegun -= Close;
        }
    }
}
