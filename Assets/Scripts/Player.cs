using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _charController;
    [SerializeField] private Transform cam;
    [SerializeField] private AudioSource _runSource;


    public int _health = 10;
    public float moveSpeed = 8.0f; // normal: 8f
    private float gravitation = -15f; // normal: -15f
    float turnSmoothVelocity;
    private float horizontal, vertical;
    private Vector3 direction;
    private float targertAngle;
    private Vector3 moveDirection;
    private float _verticalVelocity;
    public float JumpHeight = 1.2f; // normal: 1.2f
    private float _terminalVelocity = 53.0f;
    private float _slowMouseX;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        float _mouseX = Input.GetAxis("Mouse X");
        _slowMouseX = Mathf.Lerp(_slowMouseX, _mouseX, 10 * Time.deltaTime);
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        JumpAndGravity();
        Move();
    }

    private void JumpAndGravity()
    {
        if (_charController.isGrounded)
        {
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            if (Input.GetButtonDown("Jump"))
            {
                _animator.SetTrigger("isJump");
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * gravitation);
            }
        }
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravitation * Time.deltaTime;
        }
    }
    private void Move()
    {
        float _speed = 0;
        if (direction.magnitude >= 0.1f) // Give speed to move
        {
            if (!_runSource.isPlaying)
                _runSource.Play();
            _speed = moveSpeed;
        }
        else
        {
            _runSource.Stop();
            _speed = 0;
        }

        if (direction.magnitude >= 0.1f) // Move
        {
            Rotate();
            _animator.SetBool("isRun", true);
        }
        else // State
        {
            RotateAnimation();
            _animator.SetBool("isRun", false);
            Rotate();
        }
        moveDirection = Quaternion.Euler(0f, targertAngle, 0f) * Vector3.forward;
        _charController.Move(moveDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void RotateAnimation()
    {
        // Rotate Ainmation
        if (_slowMouseX > 0.03)
        {
            _animator.SetBool("strafeRight", true);
        }
        else
        {
            _animator.SetBool("strafeRight", false);
        }
        if (_slowMouseX < -0.03)
        {
            _animator.SetBool("strafeLeft", true);
        }
        else
        {
            _animator.SetBool("strafeLeft", false);
        }
    }
    public void TakeDamage()
    {
        _health -= 1;
    }
    private void Rotate()
    {
        targertAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targertAngle, ref turnSmoothVelocity, 0.1f);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}