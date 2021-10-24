using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform player;
    public float lerpTime;

    private Vector3 _offset;
    private Vector3 _defaultPos;
    private Quaternion _defaultRot;
    private bool _following = true;
    
    private void Start()
    {
        _offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if(_following)
            transform.position = player.position + _offset;
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