﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private Transform player_2;
    public Transform mesh;

    public float speed;
    public float deadZone;
    private float forceToGround = Physics.gravity.y;
    private float gravityMag = 5f;
    public float jumpSpeed;
    public float distance;

    public bool isMoving;
    private bool lastStepR;
    private bool movingPositive;
    private bool crouched;
    private bool jumping;

    private Vector2 stopped = new Vector2(0, 0);
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        player_2 = GameObject.FindGameObjectWithTag("Player_2").GetComponent<Transform>();
        isMoving = false;
        lastStepR = true;
        crouched = false;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if(movingPositive)
            {
                //transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
                characterController.Move(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            }
            else //transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            {
                //transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
                characterController.Move(new Vector3(-1, 0, 0) * speed * Time.deltaTime);
            }
        }
        //else return;
        if(characterController.isGrounded && !jumping)
        {
            moveDirection.y = forceToGround;
        }
        else
        {
            jumping = false;
            moveDirection.y += Physics.gravity.y * gravityMag * Time.deltaTime;
            Debug.Log("NotGrounded");
        }
        characterController.Move(moveDirection * Time.deltaTime);

        //Look at enemy
        distance = Vector3.Distance(transform.position, player_2.position);
        if(transform.position.x >  player_2.position.x)
        {
            Quaternion lookRotation = Quaternion.Euler(0, 180, 0);
            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, lookRotation, 5);
        }
        else if (transform.position.x < player_2.position.x)
        {
            Quaternion lookRotation = Quaternion.Euler(0, 0, 0);
            mesh.transform.rotation = Quaternion.RotateTowards(mesh.transform.rotation, lookRotation, 5);
        }
    }
    public void MoveLeftLeg(Vector2 axis, Vector2 axis_2)
    {
        if (axis == stopped || isMoving || !lastStepR || axis_2 != stopped || crouched) return;

        if (axis.x > deadZone)
        {
            movingPositive = true;
            SetWalkL();
        }
        else if (axis.x < -deadZone)
        {
            movingPositive = false;
            SetWalkL();
        }
        else return;
    }
    public void MoveRightLeg(Vector2 axis, Vector2 axis_2)
    {
        if (axis == stopped || isMoving ||lastStepR || axis_2 != stopped || crouched) return;
        if (axis.x > deadZone)
        {
            movingPositive = true;
            SetWalkR();
        }
        else if (axis.x < -deadZone)
        {
            movingPositive = false;
            SetWalkR();
        }
        else return;
    }
    public void Crouch(Vector2 axis, Vector2 axis_2)
    {
        if (isMoving) return;
        if (axis.y < -deadZone && axis_2.y < -deadZone)
        {
            crouched = true;
            Debug.Log("Crouched");
            animator.SetBool("Crouched", true);
        }
        if(axis == stopped || axis_2 == stopped)
        {
            StartCoroutine(JumpTime());
        }
        if (axis.y > deadZone && axis_2.y > deadZone && crouched)
        {
            moveDirection.y = jumpSpeed;
            jumping = true;
            crouched = false;
            animator.SetBool("Crouched", false);
        }
    }
    void SetWalkL()
    {
        if (isMoving) return;
        animator.SetBool("Right", false);
        animator.SetBool("Walking", true);
        lastStepR = false;
        isMoving = true;
    }
    void SetWalkR()
    {
        if (isMoving) return;
        animator.SetBool("Right", true);
        animator.SetBool("Walking", true);
        lastStepR = true;
        isMoving = true;
    }

    private IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(0.5f);
        crouched = false;
        animator.SetBool("Crouched", false);

        yield return null;
    }
}
