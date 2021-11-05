using System;
using System.Collections;
using Dialogues;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    public float attackRate;
    public Animator animator;
    public new Transform camera;
    
    private Rigidbody _rb;
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private bool _fighting;
    public static bool DisableInput { get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

  

    private void FixedUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (DisableInput)
        {
            animator.SetFloat(SpeedX, 0);
            animator.SetFloat(SpeedY, 0);
            return;
        }
        ;
        RotateToMouse();
        var movement = new Vector3(horizontal, 0, vertical);
        animator.SetFloat(SpeedX, 2*Vector3.Dot(movement, transform.right));
        animator.SetFloat(SpeedY, 2*Vector3.Dot(movement, transform.forward));
        _rb.MovePosition(_rb.position + movement.normalized * (speed * Time.deltaTime));
    }

    private void RotateToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        var targetRot = Quaternion.Euler(new Vector3(0,  -angle + 90,0));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
    }
}
