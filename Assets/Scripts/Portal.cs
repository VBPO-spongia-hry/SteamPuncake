using System;
using System.Collections;
using Levels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Portal : MonoBehaviour
    {
        public static Portal TeleportingTo;
        public Portal target;

        private void OnTriggerEnter(Collider other)
        {
            if(TeleportingTo == this) return;
            if (other.CompareTag("Player"))
            {
                TeleportingTo = target;
                Debug.Log("Teleport to:" + target);
                StartCoroutine(Teleport(other.transform));
            }
        }

        private IEnumerator Teleport(Transform obj)
        {
            yield return LevelController.Instance.FadeTransition(() =>
            {
                obj.position = target.transform.position;
                obj.rotation = target.transform.rotation;
                Debug.Log("Teleporting to:" + target);
                return null;
            });
            yield return new WaitForSeconds(5);
            Debug.Log("Can move");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && TeleportingTo == this)
            {
                TeleportingTo = null;
            }
        }
    }
}