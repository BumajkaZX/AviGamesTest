namespace AviGamesTest.Save
{
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
            
            return JsonUtility.FromJson<T>(objectToLoad);
        }
        
        public bool Save<T>(string saveKey, T saveObject) where T : new()
        {
            var json = JsonUtility.ToJson(saveObject);
            PlayerPrefs.SetString(saveKey, json);
            PlayerPrefs.Save();
            return true;
        }
    }
}
