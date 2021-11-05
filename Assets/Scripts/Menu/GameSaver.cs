using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Menu
{
    [System.Serializable]
    public class SaveData
    {
        public float musicVol;
        public float sfxVol;
        public int resX;
        public int resY;
        public string name = "Player";
        public bool fullscreen;
    }

    [System.Serializable]
    public static class GameSaver
    {
        private static string Path => Application.persistentDataPath;
        private static SaveData _data;

        public static SaveData LoadGame()
        {
            if(_data != null)
            {
                return _data;
            }
            if (File.Exists(Path+"/gamesave.save"))
            {
                var bf = new BinaryFormatter();
                var file = File.Open(Path + "/gamesave.save", FileMode.Open);
                file.Position = 0;
                var saveData = (SaveData)bf.Deserialize(file);
                file.Close();
#if UNITY_EDITOR
                saveData.name = "test";
#endif
                _data = saveData;
                return saveData;
            }
            else
            {
                Debug.Log("No saved game!");
                return null;
            }
        }

        public static void SaveGame(SaveData data)
        {
            var bf = new BinaryFormatter();
            var file = File.Create(Path + "/gamesave.save");
            bf.Serialize(file, data);
            Debug.Log("Game Saved");
            _data = data;
            file.Close();
        }
    }
}