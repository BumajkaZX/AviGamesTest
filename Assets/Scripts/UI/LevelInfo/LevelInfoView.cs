namespace AviGamesTest.UI
{
    using Game;
    using TMPro;
    using UnityEngine;
    using Zenject;

    public class LevelInfoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        [Inject]
        private IGameObserver _gameObserver;

        private void Awake()
        {
            _gameObserver.OnLevelChange += OnLevelChange;
            _gameObserver.OnGameEnd += Disable;
            _gameObserver.OnGameBegun += Enable;
        }
        private void Enable() => gameObject.SetActive(true);
        private void Disable(bool obj) => gameObject.SetActive(false);
        
        private void OnLevelChange(int level)
        {
            SetText($"Level {level + 1}"); //Normalize
        }

        private void SetText(string text) => _text.text = text;

        private void OnDestroy()
        {
            _gameObserver.OnLevelChange -= OnLevelChange;
            _gameObserver.OnGameEnd -= Disable;
            _gameObserver.OnGameBegun -= Enable;
        }
    }
}
