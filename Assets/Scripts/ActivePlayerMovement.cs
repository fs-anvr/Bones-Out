using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerMovement : MonoBehaviour
{
    #region moveVariables

    [SerializeField] private float _horisontalSpeed = 5.0f;
    [SerializeField] private float velPower = 2.0f;
    [SerializeField] private float acceleration = 3.0f;
    [SerializeField] private float decceleration = 1.0f;
    private bool _facingRight;

    #endregion


    #region jumpVariables

    [SerializeField] private float _jumpForce = 3.0f;
    private float _checkScale = 0.5f;
    public bool _isGrounded;
    [SerializeField] public float frictionAmount = 0.3f;
    [SerializeField] private float _jumpCoyoteTime = 0.13f;
    private float _wolfJumpTime = 0.6f;
    //private float _wolfJumpTime = 0.2f;
    private float _lastGroundedTime = 0.0f;

    #endregion


    private float wolfTimer = 0.5f;

    [SerializeField] private LayerMask _groundLayerMask;
    private Rigidbody2D _rb;
    private Animator _anim;
    private LoadParameters parameters;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        //_groundLayerMask = LayerMask.GetMask("Ground");
        _facingRight = true;
        parameters = Resources.Load<LoadParameters>("LoadParameters");
    }


    private void FixedUpdate()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - (this.GetComponent<BoxCollider2D>().size.y / 2));
        Vector2 size = transform.lossyScale;
        size.x *= this.GetComponent<BoxCollider2D>().size.x;
        size.y *=  _checkScale;
        Collider2D coll = Physics2D.OverlapBox(pos, size, 0, _groundLayerMask);
        if (coll != null)
        {
            if (this.tag == "WolfAttack" && !_isGrounded && coll.gameObject.tag != "Wooden")
            {
                this.tag = "Wolf";
                _anim.SetBool("PowerJump", false);
            }
            _isGrounded = true;
            _anim.SetBool("isGrounded", true);
            _lastGroundedTime = _jumpCoyoteTime;
        }
    }


    private void Update()
    {
        if (parameters.currentCharacterName == name)
        {
            JumpLogic();
            MovementLogic();
            ReverseLogic();
        }
        Friction();
        Timer();
        Animation();

        if (_lastGroundedTime <= 0 && _isGrounded)
        {
            _isGrounded = false;
            _anim.SetBool("isGrounded", false);
        }
    }


    private void MovementLogic()
    {
        float targetSpeed = Input.GetAxis("Horizontal") * _horisontalSpeed;
        float speedDif = targetSpeed - _rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        _rb.AddForce(movement * Vector2.right);
    }


    private void Friction()
    {
        if (_isGrounded && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01f )
        {
            float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(_rb.velocity.x);

            _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }


    private void ReverseLogic()
    {
        if ((!_facingRight && Input.GetAxis("Horizontal") > 0) || (_facingRight && Input.GetAxis("Horizontal") < 0))
        {
            Reverse();
        }
    }


    private void Reverse()
    {
        _facingRight = !_facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }


    private void JumpLogic()
    {
        if (Input.GetButton("Jump") && _isGrounded && _lastGroundedTime > 0)
        {
            Jump();
            if (this.tag == "Wolf")
            {
                wolfTimer = _wolfJumpTime;
            }
        }
        else if (Input.GetButton("Jump") && !_isGrounded && this.tag == "Wolf" && wolfTimer < 0)
        {
            this.tag = "WolfAttack";
            _anim.SetBool("PowerJump", true);
            _rb.AddForce(Vector2.down * _jumpForce / 2, ForceMode2D.Impulse);

        }
    }


    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
        _anim.SetBool("isGrounded", false);
        _lastGroundedTime = 0;
    }



    private void Timer()
    {
        wolfTimer -= Time.deltaTime;
        _lastGroundedTime -= Time.deltaTime;
    }


    private void Animation()
    {
        if (Mathf.Abs(_rb.velocity.x) > 0.01f && !_anim.GetBool("Run"))
        {
            _anim.SetBool("Run", true);
        }
        else if (Mathf.Abs(_rb.velocity.x) < 0.01f && _anim.GetBool("Run"))
        {
            _anim.SetBool("Run", false);
        }
    }
}

