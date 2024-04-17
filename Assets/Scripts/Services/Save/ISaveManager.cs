namespace AviGamesTest.Services.Save
{
    public interface ISaveManager
    {
        public T Load<T>(string loadKey) where T : new();

        public bool Save<T>(string saveKey, T saveObject) where T : new();
    }
}
