using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {
        public TMP_Dropdown dropdown;
        private Vector2Int _resolution;
        public TextMeshProUGUI mainText;
        public AudioMixer audioMixer;
        public Slider musicSlider;
        public Slider sfxSlider;
        public Toggle fullscreen;
        private Animator _animator;
        private static readonly int SettingsShown = Animator.StringToHash("Settings Shown");

        private void Start()
        {
            SaveData saveData = GameSaver.LoadGame();
            _animator = GetComponent<Animator>();
            if (saveData != null)
            {
                LoadData(saveData);
            }
            Vector2Int[] resolutions = { new Vector2Int(3840, 2160), new Vector2Int(2560, 1440), new Vector2Int(1920, 1080), new Vector2Int(1280, 720), new Vector2Int(854,480)};
            _resolution = new Vector2Int(Display.main.systemWidth, Display.main.systemHeight);
            int height = Display.main.systemHeight;
            int width = Display.main.systemWidth;
            foreach (Vector2 res in resolutions)
            {
                if (res.x <= width && res.y <= height)
                {
                    dropdown.options.Add(new TMP_Dropdown.OptionData(res.x + "×" + res.y));
                }
            }
        }

        private void ChangeVolume()
        {
            audioMixer.SetFloat("SFX", sfxSlider.value);
            audioMixer.SetFloat("Music", musicSlider.value);
        }
    
        public void Exit()
        {
            Application.Quit();
        }

        public void Settings()
        {
            _animator.SetBool(SettingsShown, true);
            mainText.SetText("Settings");
        }
    
        public void BackSettings()
        {
            Screen.SetResolution(_resolution.x, _resolution.y, fullscreen.isOn);
            mainText.SetText("SteamPuncake");
            ChangeVolume();
            SaveData saveData = GetSaveData();
            GameSaver.SaveGame(saveData);
            _animator.SetBool(SettingsShown, false);
        }
    
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void DropDownChanged()
        {
            TMP_Dropdown.OptionData optionData = dropdown.options[dropdown.value];
            string options = optionData.text;
            string[] option = options.Split('×');
            _resolution.x = int.Parse(option[0]);
            _resolution.y = int.Parse(option[1]);
        }

        public void ResetProgress()
        {
            LevelController.CurrentLevel = 0;
            LevelController.CurrentLocation = 0;
            PlayerPrefs.DeleteKey("unlockedWeapons");
        }
        
        private SaveData GetSaveData()
        {
            var saveData = new SaveData
            {
                musicVol = musicSlider.value,
                sfxVol = sfxSlider.value,
                resX = _resolution.x,
                resY = _resolution.y,
                fullscreen = fullscreen.isOn
            };
            return saveData;
        }
        
        private void LoadData(SaveData data)
        {
            sfxSlider.value = data.sfxVol;
            musicSlider.value = data.musicVol;
            ChangeVolume();
            Screen.SetResolution(data.resX, data.resY, data.fullscreen);
            dropdown.value = dropdown.options.IndexOf(new TMP_Dropdown.OptionData(data.resX + "×" + data.resY));
        }
    }
}
