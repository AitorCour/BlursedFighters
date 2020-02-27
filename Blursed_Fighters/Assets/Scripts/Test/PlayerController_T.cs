using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_T : MonoBehaviour
{
    private Vector2 stopped = new Vector2(0, 0);

    public float stepTime;
    public float speed;

    public bool canMove;
    public bool canMakeOtherStep;
    private bool leftLegMoved;
    private bool rightLegMoved;

    // Start is called before the first frame update
    void Start()
    {
        canMakeOtherStep = true;
        leftLegMoved = false;
        rightLegMoved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector2 inputAxis, Vector2 inputAxis_2)
    {
        if (inputAxis != stopped && canMakeOtherStep && !leftLegMoved && !canMove)
        {
            canMove = true;
            canMakeOtherStep = false;
            leftLegMoved = true;
        }
        if(canMove && leftLegMoved && !rightLegMoved)
        {
            StartMovingLegL();
        }
        if (inputAxis_2 != stopped && canMakeOtherStep && !rightLegMoved && !canMove)
        {
            canMove = true;
            canMakeOtherStep = false;
            rightLegMoved = true;
        }
        if (canMove && rightLegMoved && !leftLegMoved)
        {
            StartMovingLegR();
        }
    }
    private void StartMovingLegR()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            StartCoroutine(WaitStep(false, true));
            Debug.Log("Moving_l");
    }
    private void StartMovingLegL()
    {
        transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
            StartCoroutine(WaitStep(true, false));
            Debug.Log("Moving_l");
    }
    private IEnumerator WaitStep(bool left, bool right)
    {
        yield return new WaitForSeconds(stepTime);
        canMakeOtherStep = true;
        canMove = false;
        if (left)
        {
            rightLegMoved = false;
        }
        else leftLegMoved = false;
        yield return null;
    }
}
