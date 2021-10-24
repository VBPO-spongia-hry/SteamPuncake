using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogues
{
    public class DialogueCharacter : MonoBehaviour
    {
        public string characterName;
        public string job;
        public Canvas bubbleCanvas;
        public TextMeshProUGUI bubble;

        private Animation _showAnim;

        private void Start()
        {
            _showAnim = bubbleCanvas.GetComponent<Animation>();
            bubbleCanvas.gameObject.SetActive(false);
        }

        public IEnumerator ShowMessage(Message msg)
        {
            bubbleCanvas.gameObject.SetActive(true);
            _showAnim.Play("ShowBubble");
            bubble.text = "";
            yield return new WaitWhile(() => _showAnim.isPlaying);
            var displayed = "";
            
            foreach (var t in msg.msg)
            {
                displayed += t;
                bubble.text = displayed;
                yield return new WaitForSeconds(0.02f);
                if (!Input.anyKeyDown) continue;
                bubble.text = msg.msg;
                break;
            }

            yield return new WaitUntil(() => Input.anyKeyDown);
            _showAnim.Play("HideBubble");
            yield return new WaitWhile(() => _showAnim.isPlaying);
            bubbleCanvas.gameObject.SetActive(false);
        }
    }
}