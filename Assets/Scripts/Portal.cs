using System;
using System.Collections;
using Levels;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private static Portal _teleportingTo;
    public Portal target;
    public int levelToUnlock;

    private void Start()
    {
        if (LevelController.CurrentLevel < levelToUnlock)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_teleportingTo == this) return;
        if (other.CompareTag("Player") && target)
        {
            _teleportingTo = target;
            PlayerMovement.DisableInput = true;
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
        PlayerMovement.DisableInput = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _teleportingTo == this)
        {
            _teleportingTo = null;
        }
    }
}