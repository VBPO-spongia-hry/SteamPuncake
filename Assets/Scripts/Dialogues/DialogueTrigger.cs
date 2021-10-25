using System;
using System.Collections;
using UnityEngine;

namespace Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue dialogue;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.Singleton.CameraPos = transform.GetChild(0);
                DialogueManager.Singleton.BeginDialogue(dialogue);
                StartCoroutine(LerpToPos(other.transform, transform.position, transform.rotation));
            }
        }

        private void OnDrawGizmos()
        {
            var cam = Camera.main;
            if (cam)
            {
                Gizmos.matrix = Matrix4x4.Translate(transform.GetChild(0).position) * Matrix4x4.Rotate(transform.GetChild(0).rotation);
                Gizmos.DrawFrustum(transform.GetChild(0).position, cam.fieldOfView, cam.nearClipPlane, cam.farClipPlane, cam.aspect);
            }
                
        }
        
        public IEnumerator LerpToPos(Transform transform, Vector3 targetPos, Quaternion targetRot)
        {
            var defaultPos = transform.position;
            var defaultRot = transform.rotation;
            var t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / Camera.main.GetComponent<CameraMover>().lerpTime; 
                transform.position = Vector3.Lerp(defaultPos, targetPos, t);
                transform.rotation = Quaternion.Slerp(defaultRot, targetRot, t);
                yield return null;
            }

            transform.position = targetPos;
            transform.rotation = targetRot;
        }
    }
}