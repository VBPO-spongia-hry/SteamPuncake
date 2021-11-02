using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "level", menuName = "Level", order = 2)]
    public class Level : ScriptableObject
    {
        public string levelName;
        [TextArea(2,5)]
        public string description;
        public int order;
        public AudioClip clip;
        public bool unlocksNextLocation;
        public bool Unlocked => PlayerPrefs.GetInt("currentLevel", 0) >= order;
    }
}