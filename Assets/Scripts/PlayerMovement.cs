using System;
using System.Collections;
using Dialogues;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float attackRate;
    public Animator animator;
    public new Transform camera;
    
    private Rigidbody _rb;
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private bool _fighting;
    private static readonly int Fighting = Animator.StringToHash("Fighting");
    public static bool DisableInput { get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private IEnumerator Attack()
    {
        _fighting = true;
        animator.SetTrigger(Fighting);
        yield return new WaitForSeconds(attackRate);
        _fighting = false;
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
        if (Input.GetButtonDown("Fire1")) StartCoroutine(Attack());
        var movement = new Vector3(horizontal, 0, vertical);
        animator.SetFloat(SpeedX, horizontal * speed);
        animator.SetFloat(SpeedY, vertical * speed);
        _rb.MovePosition(_rb.position + movement.normalized * (speed * Time.deltaTime));
    }
}
