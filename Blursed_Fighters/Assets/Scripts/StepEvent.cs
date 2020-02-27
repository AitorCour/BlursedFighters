using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepEvent : MonoBehaviour
{
    private PlayerController player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetIdleL()
    {
        player.isMoving = false;
        animator.SetBool("Walking", false);

    }
    public void SetIdleR()
    {
        player.isMoving = false;
        animator.SetBool("Walking", false);
    }
}
