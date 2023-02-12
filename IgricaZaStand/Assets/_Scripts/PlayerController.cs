using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _speed = 300;
    [SerializeField] float _jumpingPower = 600;

    Rigidbody2D _rigidbody;
    Animator _animator;
    float _horizontal;
    bool _isFacingRight = true;
    bool _canDoubleJump = false;

    void Awake()
    {
        //GetComponent se koristi da se uzme komponenta sa objekta
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        Flip();
        HandleAnimations();
        Jump();
    }

    private void HandleAnimations()
    {
        if(_horizontal == 0)
            _animator.SetBool("isRunning", false);
        else
            _animator.SetBool("isRunning", true);

        if (IsGrounded())
            _animator.SetBool("isJumping", false);
        else
            _animator.SetBool("isJumping", true);

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //ako se drzi shift onda se sprintuje
        if (Input.GetKey(KeyCode.LeftShift))
            _rigidbody.velocity = new Vector2(_horizontal * _speed * Time.fixedDeltaTime * 1.5f, _rigidbody.velocity.y);
        else
            _rigidbody.velocity = new Vector2(_horizontal * _speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        if(!Input.GetButton("Jump") && IsGrounded())
        {
            _canDoubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && (IsGrounded() || _canDoubleJump))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower * Time.fixedDeltaTime);
            _canDoubleJump = !_canDoubleJump;
        }

        //ceo ovaj sledeci deo je dodat da bi skok bio varijabilan odnosno razlicit u odnosu na to koliko se drzi space
        if (Input.GetButtonUp("Jump") && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f * Time.fixedDeltaTime);
        }
    }


    private void Flip()
    {
        if(_isFacingRight && _horizontal < 0 || !_isFacingRight && _horizontal > 0)
        {
            _isFacingRight = !_isFacingRight;

            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        }
    }

    // znak => je isto kao da smo napisali: { return... }
    private bool IsGrounded() => Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
}
