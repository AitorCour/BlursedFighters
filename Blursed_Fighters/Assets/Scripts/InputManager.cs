using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis_L = Vector2.zero;
        inputAxis_L.x = Input.GetAxis("Horizontal");
        inputAxis_L.y = Input.GetAxis("Vertical");

        Vector2 inputAxis_R = Vector2.zero;
        inputAxis_R.x = Input.GetAxis("Horizontal_2");
        inputAxis_R.y = Input.GetAxis("Vertical_2");

        playerController.MoveLeftLeg(inputAxis_L, inputAxis_R);
        playerController.MoveRightLeg(inputAxis_R, inputAxis_L);
        playerController.Crouch(inputAxis_L, inputAxis_R);
    }
}
