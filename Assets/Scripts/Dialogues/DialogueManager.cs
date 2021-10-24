using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Singleton;
        public Animator dialogueAnimator;
        public TextMeshProUGUI nameText1;
        public TextMeshProUGUI nameText2;
        public AudioClip[] characterClips;
        
        private DialogueCharacter _character1;
        private DialogueCharacter _character2;
        private CameraMover _mover;
        [NonSerialized] public Transform CameraPos;
        
        private static readonly int Skip = Animator.StringToHash("Skip");
        private static readonly int Next = Animator.StringToHash("Next");
        private static readonly int Isfirst = Animator.StringToHash("Isfirst");
        private static readonly int Viewing = Animator.StringToHash("Viewing");
        private static readonly int DialogueStart = Animator.StringToHash("DialogueStart");
        private bool _showingDialogue;
        private AudioSource _audioSource;
        
        private void OnEnable()
        {
            if(Singleton != null) Destroy(Singleton.gameObject);
            Singleton = this;
            _audioSource = GetComponent<AudioSource>();
            _mover = Camera.main.GetComponent<CameraMover>();
        }
        
        private void Update()
        {
            if (_showingDialogue)
            {
                if(Input.anyKeyDown) dialogueAnimator.SetTrigger(Skip);
            }
        }

        private DialogueCharacter GetCharacterByName(string characterName)
        {
            if (_character1 && _character1.characterName == characterName) return _character1;
            if (_character2 && _character2.characterName == characterName) return _character2;
            var characters = FindObjectsOfType<DialogueCharacter>();
            return characters.FirstOrDefault(character => character.characterName == characterName);
        }

        public void BeginDialogue(Dialogue dialogue)
        {
            dialogueAnimator.ResetTrigger(Skip);
            PlayerMovement.DisableInput = true;
            _character1 = null;
            _character2 = null;
            foreach (var msg in dialogue.messages)
            {
                var character = GetCharacterByName(msg.characterName);
                if (_character1 == null) _character1 = character;
                else if (character != _character1 && _character2 == null) _character2 = character;
                else if (character != _character2 && character != _character1)
                {
                    Debug.LogError("More than 2 characters in dialogue. Ignoring ... ");
                    return;
                }
            }
            if (_character2 != null)
            {
                nameText2.gameObject.SetActive(true);
                nameText2.SetText($"{_character2.characterName}, {_character2.job}");
            }
            else
            {
                nameText2.gameObject.SetActive(false);
            }
            
            if (_character1 != null)
            {
                nameText1.gameObject.SetActive(true);
                nameText1.SetText($"{_character1.characterName}, {_character1.job}");
            }
            else
            {
                nameText1.gameObject.SetActive(false);
            }
            
            StartCoroutine(Begin(dialogue));
        }

        private IEnumerator Begin(Dialogue dialogue)
        {
            _showingDialogue = true;
            yield return _mover.LerpToPos(CameraPos.position, CameraPos.rotation);
            dialogueAnimator.SetTrigger(DialogueStart);
            dialogueAnimator.SetBool(Viewing,true);
            dialogue.Reset();
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < dialogue.messages.Length; i++)
            {
                var msg = dialogue.ShowNext();
                var character = GetCharacterByName(msg.characterName);
                yield return ShowMessage(msg, character);
            }
            dialogueAnimator.SetBool(Viewing, false);
            yield return _mover.ReturnToNormal();
            PlayerMovement.DisableInput = false;
            _showingDialogue = false;
            _audioSource.Stop();
        }

        private IEnumerator ShowMessage(Message msg, DialogueCharacter character)
        {
            _audioSource.clip = characterClips[Random.Range(0, characterClips.Length)];
            _audioSource.Play();
            dialogueAnimator.SetBool(Isfirst, _character1 == character);
            dialogueAnimator.SetTrigger(Next);
            dialogueAnimator.ResetTrigger(Skip);
            yield return new WaitUntil(() => dialogueAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Shown"));
            yield return character.ShowMessage(msg);
            yield return new WaitUntil(() => dialogueAnimator.GetCurrentAnimatorStateInfo(0).IsName("UIShown"));
        }
    }
}
