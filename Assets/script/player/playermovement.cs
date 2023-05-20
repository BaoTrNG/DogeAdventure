using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public FloatingJoystick LeftTouchInput;
    public FloatingJoystick RightTouchInput;

    public FixedButton JumpBtn;
    public FixedButton DashBtn;
    public float MoveSpeed;
    public float BaseSpeed = 5f;
    public float _rotateSpeed;

    //private GameObject cam;
    public bool IsMount;
    public bool CanMove = true;
    private bool CanDash = true;
    private int DashCooldownTime = 3;
    private AudioSource audioSource;

    public AudioClip walksound;

    public Rigidbody _rigidbody;
    private AnimationController anim;
    public AnimationController MountAnim;
    private PlayerProperties playerProperties;
    public Vector3 _moveVector;


    public LayerMask groundLayer;

    // private bool jumped = false;
    public bool isGrounded;





    private void Awake()
    {
        //cam = GameObject.FindWithTag(Tags.MAIN_CAMERA);
        _rigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<AnimationController>();
        audioSource = GetComponent<AudioSource>();
        playerProperties = GetComponent<PlayerProperties>();
    }
    private void Start()
    {


    }
    private void Update()
    {

        CheckIfGrounded();
        Move();

        // Jump();
    }

    private void Move()
    {
        _moveVector = Vector3.zero;
        //  _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        //_moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;
        _moveVector.x = LeftTouchInput.Horizontal * MoveSpeed * Time.deltaTime;
        _moveVector.z = LeftTouchInput.Vertical * MoveSpeed * Time.deltaTime;

        if (LeftTouchInput.Horizontal != 0 || LeftTouchInput.Vertical != 0)
        {

            if (CanMove)
            {

                Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed, 0.0f);
                Quaternion targetRotation = Quaternion.LookRotation(_moveVector);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
                // transform.rotation = Quaternion.LookRotation(_moveVector);



                if ((DashBtn.Pressed || Input.GetKeyDown(KeyCode.J)) && CanDash && isGrounded && playerProperties.GetMana() > 10)
                {
                    playerProperties.MinusMana(10);
                    _rigidbody.AddForce(_moveVector * 50f, ForceMode.VelocityChange);
                    CanDash = false;
                    playerProperties.playerUi.IsCooldown = false;
                    Invoke("ResetCanDash", 3f);
                }
                else
                {
                    _rigidbody.MovePosition(_rigidbody.position + _moveVector);
                    if (IsMount)
                    {
                        anim.SetRun(false);
                        MountAnim.SetRun(true);
                    }
                    else
                    {
                        anim.SetRun(true);
                    }

                }

            }
        }

        else if (LeftTouchInput.Horizontal == 0 && LeftTouchInput.Vertical == 0)
        {
            if (IsMount)
            {
                MountAnim.SetRun(true);

            }
            else
            {
                anim.SetRun(false);

            }

        }

    }

    void ResetCanDash()
    {
        CanDash = true;
    }

    void Jump()
    {
        if (isGrounded)
        {
            //print("canmove");
            //  if (!jumped)
            //  {
            if (CanMove)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _rigidbody.AddForce(Vector3.up * 200f, ForceMode.Impulse);
                    anim.SetJump(true);
                }
                else if (JumpBtn.Pressed)
                {
                    JumpBtn.Pressed = false;

                    _rigidbody.AddForce(Vector3.up * 6f, ForceMode.VelocityChange);
                    anim.SetJump(true);
                }
            }
            //  }
        }
    }
    void CheckIfGrounded()
    {
        float radius = 1f * 0.9f;

        //get the position (assuming its right at the bottom) and move it up by almost the whole radius
        Vector3 pos = transform.position + Vector3.up * (radius * 0.9f);
        //returns true if the sphere touches something on that layer
        isGrounded = Physics.CheckSphere(pos, radius, groundLayer);


    }

    /*private void OnDrawGizmosSelected()
    {
        // Draw a sphere at the transform's position with the specified radius and color
        float radius = 1f;

        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawSphere(groundCheckPosition.position, radius);

        // Perform a sphere cast and draw the result using Gizmos
        bool hit = Physics.CheckSphere(transform.position, radius, LayerMask.GetMask("Default"), QueryTriggerInteraction.Ignore);
        Gizmos.color = hit ? UnityEngine.Color.green : UnityEngine.Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * 1f, radius);
    }*/

    /*  void Resetjumped()
      {
          jumped = false;
      }*/

}
