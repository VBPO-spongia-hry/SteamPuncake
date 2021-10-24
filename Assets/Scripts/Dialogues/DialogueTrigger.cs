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
    }
}