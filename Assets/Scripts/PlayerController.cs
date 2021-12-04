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
    private int _tools;
    //[SerializeField] private BoxController box;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
        _tools = 0;
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

        Vector3 _direction = new Vector3(-x, 0f, -z).normalized;

        if(_direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smothingVelocityTurn, smothingTurn);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(_direction * runSpeed * Time.deltaTime);
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

        switch (_tools)
        {
            case 0:
                if (Input.GetKey(KeyCode.LeftShift) && isRunning == false)
                {
                    playerAnimator.SetTrigger("Pickup");
                    playerAnimator.ResetTrigger("Dropoff");
                    _tools = 1;
                }
                break;
            case 1:
                if (Input.GetKey(KeyCode.LeftShift) && isRunning == false)
                {
                    playerAnimator.SetTrigger("Dropoff");
                    playerAnimator.ResetTrigger("Pickup");
                    _tools = 0;
                }
                break;

            default:
                Debug.Log("Test");
                break;
        }
    }

    public bool _isRunning { get { return isRunning; } }
    public bool _isGrounded { get { return isGrounded;  } }

    public Animator _playerAnimator { get { return playerAnimator; } set { playerAnimator = value; } }
}
