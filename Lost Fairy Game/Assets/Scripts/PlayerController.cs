using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SwipeManager swipeControls;
    private CharacterController controller;
    public GameObject player;
    private Vector3 move;
    public float forwardSpeed;
    public Animator anim;
    public bool isGrounded;

    private int desiredLane = 1; //0:left 1:Middle 2:Right
    public float laneDistance = 3; //Distance between the lanes

    public float jumpForce;
    public float gravity = -10;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = controller.isGrounded;

        if (!PlayerManager.gameStart)
            return;
        anim.SetBool("isGameStarted", true);
        move.z = forwardSpeed;

        // if the player is grounded, when W key is pressed player jumps.
        //Else Gravity is on player

        anim.SetBool("isGrounded", controller.isGrounded);

        if (controller.isGrounded)
        {   
            
            if (swipeControls.swipeUp)
            {

                Jump();
                

            }

        }
        else
        {
            move.y += gravity * Time.deltaTime;
            
        }
        //For these inputs change the position of the player in the lanes.
        if (swipeControls.swipeRight)
        {
            desiredLane++;
            //stops player going outside the lane on the right.
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (swipeControls.swipeLeft)
        {
            desiredLane--;
            //stops player going outside the lane on the left.
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //Calculates where player position will be

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;

        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        controller.center = controller.center;
        /*if (transform.position == targetPosition)
        {
            return;
        }
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
        {
            controller.Move(diff);
        }
        */

    }

    private void FixedUpdate()
    {
        if (!PlayerManager.gameStart)
            return;
        controller.Move(move * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        // move.y += Mathf.Sqrt(jumpForce * -2f * gravity);
        //move.y += gravity * Time.deltaTime;
        // controller.Move(move* Time.deltaTime);
        move.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }

    }

    


}
