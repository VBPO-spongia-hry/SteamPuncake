using System;
using UnityEngine;

public class StoryImage : MonoBehaviour
{
    public Sprite image;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
        }
    }
}