namespace AviGamesTest.Game
{
    using System.Threading.Tasks;
    
    public interface IGameState
    {
        public Task LoadGame(bool async);
        
        public void StartGame();

        public void RestartGame();
    }
}
