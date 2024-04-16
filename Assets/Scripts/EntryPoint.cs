namespace AviGamesTest
{
    using Game;
    using UnityEngine;

    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private GameController _gameController;

        private void Awake()
        {
            _gameController.StartGame();
        }
    }
}
