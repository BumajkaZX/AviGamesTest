namespace AviGamesTest.Services.Save
{
    using Newtonsoft.Json;
    using UnityEngine;
    
    public class MockSaveManager : ISaveManager
    {
        public T Load<T>(string loadKey) where T : new()
        {
            if (!PlayerPrefs.HasKey(loadKey))
            {
                return default;
            }
            
            var objectToLoad = PlayerPrefs.GetString(loadKey);
            return JsonConvert.DeserializeObject<T>(objectToLoad);
        }
        
        public bool Save<T>(string saveKey, T saveObject) where T : new()
        {
            var json = JsonConvert.SerializeObject(saveObject);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
            return true;
        }
    }
}
