namespace AviGamesTest.Game
{
    using Services.Ad;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    public class RestartButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        [Inject]
        private IGameState _gameState;

        [Inject]
        private IAdRequest _adRequest;

        private bool _isRequested;

        private void Awake()
        {
            _button.onClick.AddListener(RestartGameRequest);
            _adRequest.OnAdEnd += OnAdEndCallback;
        }
        private void OnAdEndCallback(bool obj)
        {
            if(!_isRequested)
            {
                return;
            }
            
            _gameState.RestartGame();
            _isRequested = false;
        }
        private void RestartGameRequest()
        {
            _isRequested = true;
            _adRequest.ShowAd(AdType.Interstitial);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(RestartGameRequest);
        }
    }
}
