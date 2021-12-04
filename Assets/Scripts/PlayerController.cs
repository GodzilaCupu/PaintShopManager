using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator playerAnimator;

    [Header("Movement")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float smothingTurn = 1f;
    private bool isRunning;
    private float smothingVelocityTurn;

    [Header("Jump")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravity;

    [SerializeField] private Transform groundChecking;
    [SerializeField] private bool isGrounded;
    private Vector3 velocityJump;

    [Header("Box")]
    [SerializeField] private ToolsManager tools;

    void Start()
    {

        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Moving();
        Jump();
        AnimationController();
    }

    private void Moving()
    {
        isRunning = false;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _moveDirection = new Vector3(-x, 0f, -z).normalized;


        if (_moveDirection.magnitude >= 0.1f)
        {
            // Ubah Arah karakter jika di kontrol lewat keyboard
            float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smothingVelocityTurn, smothingTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(_moveDirection * runSpeed * Time.deltaTime);
            isRunning = true;
        }
    }

    private void Jump()
    {
        //check is it grounded
        isGrounded = Physics.CheckSphere(groundChecking.position, groundDistance, groundMask);

        if (isGrounded && velocityJump.y < 0)
            velocityJump.y = -2f;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocityJump.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocityJump.y += gravity * Time.deltaTime;
        controller.Move(velocityJump * Time.deltaTime);
    }

    private void AnimationController()
    {
        
        if (isRunning == true)
            playerAnimator.SetBool("Walking", true);
        else
            playerAnimator.SetBool("Walking", false);


    }

    public bool _isRunning { get { return isRunning; } }
    public bool _isGrounded { get { return isGrounded;  } }


    public Animator _playerAnimator { get { return playerAnimator; } set { playerAnimator = value; } }
}
