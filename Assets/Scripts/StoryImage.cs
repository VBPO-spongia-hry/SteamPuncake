using System;
using UnityEngine;
using UnityEngine.UI;

public class StoryImage : MonoBehaviour
{
    public GameObject canvas;
    public Sprite image;
    public Image preview;

    private void Start()
    {
        canvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            preview.sprite = image;
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