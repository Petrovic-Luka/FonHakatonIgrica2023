using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] LayerMask _bouncerLayer;
    [SerializeField] float _speed = 300;
    [SerializeField] float _jumpingPower = 600;
    [SerializeField] float jumpMulitplyer;
    [SerializeField] float wallJumpCdValue=1f;
    Rigidbody2D _rigidbody;
    Animator _animator;
    float _horizontal;
    bool _isFacingRight = true;
    bool _canDoubleJump = false;
    int _wallJumping = 0;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private float wallJumpingDirection;
    private Vector2 wallJumpingPower = new Vector2(9f, 20f);
    public float wallJumpCd = 0;

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
        WallSlide();
        WallJump2();
        Bouncer();
        Menu();
        if (wallJumpCd > 0)
        {
            wallJumpCd -= Time.deltaTime;

        }
        else {
            Move();
        }
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
    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
       //Move();
    }

    void Move()
    {
        //ako se drzi shift onda se sprintuje
        //if (Input.GetKey(KeyCode.LeftShift))
        //    _rigidbody.velocity = new Vector2(_horizontal * _speed * Time.fixedDeltaTime * 1.5f, _rigidbody.velocity.y);
        //else
            _rigidbody.velocity = new Vector2(_horizontal * _speed * Time.fixedDeltaTime , _rigidbody.velocity.y);
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

    private bool IsGrounded()
    {
        _wallJumping = 0;
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer) || Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _bouncerLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && _horizontal != 0f)
        {
            isWallSliding = true;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, Mathf.Clamp(_rigidbody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump2()
    {
        if(isWallSliding && Input.GetButtonDown("Jump") && wallJumpCd <= 0f)
        {
            _canDoubleJump = true;
            _wallJumping = 1;
            wallJumpingDirection = Mathf.Sign(transform.localScale.x);
            Debug.Log(wallJumpingDirection);
            Debug.Log(-wallJumpingDirection * wallJumpingPower.x);
            _rigidbody.velocity = new Vector2(-wallJumpingDirection * wallJumpingPower.x,
               wallJumpingPower.y);
            wallJumpCd = wallJumpCdValue;
        }
    }

    private void Bouncer()
    {
        if (Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _bouncerLayer))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,_jumpingPower*jumpMulitplyer * Time.fixedDeltaTime);
        }
    }

    //private void WallJump()
    //{
    //    if (isWallSliding)
    //    {
    //        isWallJumping = false;
    //        wallJumpingDirection = -transform.localScale.x;
    //        wallJumpingCounter = wallJumpingTime;

    //        CancelInvoke(nameof(StopWallJumping));
    //    }
    //    else
    //    {
    //        wallJumpingCounter -= Time.deltaTime;
    //    }

    //    if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
    //    {
    //        isWallJumping = true;
    //        _rigidbody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x,
    //            wallJumpingPower.y);
    //        wallJumpingCounter = 0f;

    //        _canDoubleJump = true;

    //        if (transform.localScale.x != wallJumpingDirection)
    //        {
    //            _isFacingRight = !_isFacingRight;
    //            Vector3 localeScale = transform.localScale;
    //            localeScale.x *= -1f;
    //            transform.localScale = localeScale;
    //        }

    //        Invoke(nameof(StopWallJumping), wallJumpingDuration);

    //    }

    //}

    //private void StopWallJumping()
    //{
    //    isWallJumping = false;
    //}
}
