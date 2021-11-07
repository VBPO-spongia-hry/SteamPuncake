using System.Collections;
using System.Collections.Generic;
using Levels;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform player;
    public float lerpTime;

    private Vector3 _offset;
    private Vector3 _defaultPos;
    private Quaternion _defaultRot;
    private bool _following = true;
    
    private void Awake()
    {
        _offset = transform.position - player.position;
        if (LevelController.CurrentLocation == 1)
            _offset *= 2;
    }

    private void LateUpdate()
    {
        if (_following)
        {
            if (Physics.Raycast(player.position, _offset.normalized, out var hit, _offset.magnitude, LayerMask.GetMask("Environment")))
            {
                if (hit.distance > 2)
                    transform.position = Vector3.Lerp(transform.position,
                        player.position + _offset.normalized * hit.distance, 10 * Time.deltaTime);
                else
                    transform.position = player.position + _offset;
            }
            else
                transform.position = player.position + _offset;
        }
            
    }

    public IEnumerator LerpToPos(Vector3 targetPos, Quaternion targetRot)
    {
        _defaultPos = transform.position;
        _defaultRot = transform.rotation;
        _following = false;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / lerpTime; 
            transform.position = Vector3.Lerp(_defaultPos, targetPos, t);
            transform.rotation = Quaternion.Slerp(_defaultRot, targetRot, t);
            yield return null;
        }

        transform.position = targetPos;
        transform.rotation = targetRot;
    }

    public IEnumerator ReturnToNormal()
    {
        yield return LerpToPos(_defaultPos, _defaultRot);
        _following = true;
    }
}