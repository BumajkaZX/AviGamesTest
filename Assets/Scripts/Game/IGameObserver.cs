namespace AviGamesTest.Game
{
    using System;
    
    public interface IGameObserver
    {
        public event Action OnGameRestarted;

        public event Action OnGameBegun;

        /// <summary>
        /// true - win, false - loose
        /// </summary>
        public event Action<bool> OnGameEnd;

        /// <summary>
        /// level number
        /// </summary>
        public event Action<int> OnLevelChange;
    }
}
