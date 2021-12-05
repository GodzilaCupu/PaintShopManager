using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private Transform npcPos;
    [SerializeField] private Transform spawnPos;

    [Header("Movement")]
    [SerializeField] private Animator npcAnimator;
    [SerializeField] private float speedWalk;
    private bool isMove;

    private void Start()
    {
        npcAnimator =this.GetComponent<Animator>();
    }
    
    private void Update()
    {
        if (this.transform.position != npcPos.position)
        {
            MoveToPoint(npcPos);
        }
        else
        {
            transform.rotation = npcPos.rotation;
            npcAnimator.SetBool("Walk", false);

        }
    }

    private void MoveToPoint(Transform pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos.position, speedWalk);

        npcAnimator.SetBool("Walk", true);

    }
}
