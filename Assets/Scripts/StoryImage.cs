using System;
using UnityEngine;

public class StoryImage : MonoBehaviour
{
    public GameObject canvas;
    public Sprite image;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animation>().Play("ShowStory");
            PlayerMovement.DisableInput = true;
        }
    }

    public void Close()
    {
        PlayerMovement.DisableInput = false;
        GetComponent<Animation>().Play("HideStory");
    }
}