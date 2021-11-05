using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;
	
    // How long the object should shake for.
    public float shakeDuration;
    private float _shakingTime = 0f;
	
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 _originalPos;

    public void Shake()
    {
        _shakingTime = shakeDuration;
    }
    
    private void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    private void OnEnable()
    {
        _originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (_shakingTime > 0)
        {
            camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
            _shakingTime -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            _shakingTime = 0f;
            camTransform.localPosition = _originalPos;
        }
    }
}