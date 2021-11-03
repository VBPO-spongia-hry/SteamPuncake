using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Levels
{
    public class LevelController : MonoBehaviour
    {
        [Header("Levels")] public AudioClip defaultClip;
        public GameObject[] locations;
        public GameObject[] levelObjects;
        public Transform player;
        [Space] [Header("UI")] public GameObject levelInfo;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descText;
        public Button startButton;
        public Animator fadeAnimator;
        [NonSerialized] public Level PlayingLevel = null;
        public static int CurrentLocation
        {
            get => PlayerPrefs.GetInt("currentLocation", 0);
            set => PlayerPrefs.SetInt("currentLocation", value);
        }

        public static int CurrentLevel
        {
            get => PlayerPrefs.GetInt("currentLevel", 0);
            set => PlayerPrefs.SetInt("currentLevel", value);
        }

    public static LevelController Instance;
        private AudioSource _audioSource;
        private GameObject _currentLevel;
        
        private void Start()
        {
            if(Instance)
                Destroy(Instance);
            Instance = this;
            HideLevelInfo();
            _audioSource = GetComponent<AudioSource>();
            _currentLevel = Instantiate(locations[CurrentLocation], Vector3.zero, Quaternion.identity);
            // _currentLevel.GetComponent<NavMeshSurface>().BuildNavMesh();
            SetPlayerPosition();
            PlayAudio(defaultClip);
        }

        private void EnterLevel(Level level)
        {
            if (!level.Unlocked) return;
            StartCoroutine(EnterLevelRoutine(level));
        }

        private IEnumerator EnterLevelRoutine(Level level)
        {
            PlayingLevel = level;
            fadeAnimator.SetTrigger("Show");
            HideLevelInfo();
            yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"));
            Destroy(_currentLevel);
            _currentLevel = Instantiate(levelObjects[level.order], Vector3.zero, Quaternion.identity);
            yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            PlayAudio(level.clip);
        }
        
        private void PlayAudio(AudioClip clip)
        {
            // TODO: smooth audio blending
            _audioSource.Stop();
            _audioSource.clip = clip;
            _audioSource.Play();
            SetPlayerPosition();
        }

        public void ExitLevel()
        {
            Destroy(_currentLevel);
            _currentLevel = Instantiate(locations[CurrentLocation], Vector3.zero, Quaternion.identity);
            PlayAudio(defaultClip);
        }

        private void SetPlayerPosition()
        {
            var startPos = _currentLevel.transform.Find("StartPos");
            if (!startPos) return;
            player.position = startPos.position;
            player.rotation = startPos.rotation;
        }

        public void ShowLevelInfo(Level level)
        {
            startButton.onClick.RemoveAllListeners();
            nameText.text = level.levelName;
            descText.text = level.description;
            startButton.interactable = level.Unlocked;
            startButton.onClick.AddListener(() => EnterLevel(level));
            levelInfo.GetComponent<Animation>().Play("ShowLevelInfo");
            levelInfo.SetActive(true);
        }

        public void HideLevelInfo() => StartCoroutine(HideLevelInfoRoutine());

        private IEnumerator HideLevelInfoRoutine()
        {   
            var anim = levelInfo.GetComponent<Animation>();
            anim.Play("HideLevelInfo");
            yield return new WaitWhile(() => anim.isPlaying);
            levelInfo.SetActive(false);
        }
    }
}