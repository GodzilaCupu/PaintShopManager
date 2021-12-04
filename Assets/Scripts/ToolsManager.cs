using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform holdingPos;
    [SerializeField] private PlayerController player;

    [Header("Tools")]
    [SerializeField] private float distanceToPlayer;
    private int indexTools = 0;

    bool isAbleToGrab, isRunning, isGrounded, isCarried;

    void Start()
    {
        holdingPos = GameObject.Find("HoldingPos").GetComponent<Transform>();
        player = GameObject.Find("PainshopChar").GetComponent<PlayerController>();

        isAbleToGrab = false;
        isCarried = false;
    }

    private void Update()
    {
        if(indexTools == 0) 
            GrabTools();
        else 
            DropTools();
    }

    private void FixedUpdate()
    {
        CheckBool();
    }
    
    private void GrabTools()
    {
        distanceToPlayer = Vector3.Distance(this.gameObject.transform.position, holdingPos.position);

        if (distanceToPlayer <= 1.9f)
            isAbleToGrab = true;
        else
            isAbleToGrab = false;

        if (holdingPos.transform.childCount == 0)
        {
            if (isGrounded && !isRunning )
            {
                if (isAbleToGrab && Input.GetKeyDown(KeyCode.G))
                {
                    this.GetComponent<Rigidbody>().isKinematic = true;
                    this.transform.position = holdingPos.position;
                    this.transform.parent = holdingPos.transform;
                    player._playerAnimator.SetTrigger("Pickup");
                    player._playerAnimator.ResetTrigger("Dropoff");
                    isCarried = true;
                    indexTools = 1;
                }
            }
        }
    }


    private void DropTools()
    {
        if (isCarried && Input.GetKeyDown(KeyCode.G))
        {
            this.GetComponent<Rigidbody>().isKinematic = false;
            this.transform.parent = null;
            player._playerAnimator.SetTrigger("Dropoff");
            player._playerAnimator.ResetTrigger("Pickup");
            isCarried = false;
            indexTools = 0;
        }
    }

    private void CheckBool()
    {
        isGrounded = player._isGrounded;
        isRunning = player._isRunning;
    }
}
