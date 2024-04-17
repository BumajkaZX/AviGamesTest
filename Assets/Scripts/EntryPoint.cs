namespace AviGamesTest
{
    using Game;
    using UnityEngine;
    using Zenject;

    public class EntryPoint : MonoBehaviour
    {
        [Inject]
        private IGameState _gameState;

        private void Awake()
        {
            _gameState.StartGame();
        }
    }
}
