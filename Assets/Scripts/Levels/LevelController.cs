using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public delegate IEnumerator Loading();
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
        public GameObject completeUI;
        public GameObject deathUI;
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
            GameController.Instance.baseBpm = level.bpm;
            GameController.Instance.bpm = level.bpm;
            StartCoroutine(EnterLevelRoutine(level));
        }

        private IEnumerator EnterLevelRoutine(Level level)
        {
            PlayingLevel = level;
            yield return FadeTransition(() =>
            {
                levelInfo.SetActive(false);
                Destroy(_currentLevel);
                _currentLevel = Instantiate(levelObjects[level.order], Vector3.zero, Quaternion.identity);
                return null;
            });
            PlayAudio(level.clip);
        }
        
        public IEnumerator FadeTransition(Loading loading)
        {
            fadeAnimator.SetTrigger("Show");
            yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"));
            yield return loading();
            yield return new WaitUntil(() => fadeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
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
            deathUI.SetActive(false);
            completeUI.SetActive(false);
            PlayerMovement.DisableInput = false;
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

        public void ToMenu()
        {
            SceneManager.LoadScene("Menu");
            PlayerMovement.DisableInput = false;
        }

        public void ReturnHome()
        {
            SceneManager.LoadScene("SampleScene");
            PlayerMovement.DisableInput = false;
        }
        
        public void ShowLevelInfo(Level level)
        {
            StopCoroutine(_levelInfoRoutine);
            startButton.onClick.RemoveAllListeners();
            nameText.text = level.levelName;
            descText.text = level.description;
            startButton.interactable = level.Unlocked;
            startButton.onClick.AddListener(() => EnterLevel(level));
            levelInfo.GetComponent<Animation>().Play("ShowLevelInfo");
            levelInfo.SetActive(true);
        }

        private Coroutine _levelInfoRoutine;

        public void HideLevelInfo()
        {
            _levelInfoRoutine = StartCoroutine(HideLevelInfoRoutine());
        }

        private IEnumerator HideLevelInfoRoutine()
        {   
            var anim = levelInfo.GetComponent<Animation>();
            anim.Play("HideLevelInfo");
            yield return new WaitWhile(() => anim.isPlaying);
            levelInfo.SetActive(false);
        }
    }
}