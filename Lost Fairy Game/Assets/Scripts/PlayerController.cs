using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

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
        direction.z = forwardSpeed;
       // direction.y = -1;
       // if the player is grounded, when W key is pressed player jumps.
       //Else Gravity is on player 
        if (controller.isGrounded)

        {
            if (Input.GetKeyDown(KeyCode.W))
            {

                Jump();
            }
            
        }
        else
            {
                direction.y += gravity * Time.deltaTime;
            }
        //For these inputs change the position of the player in the lanes.
        if (Input.GetKeyDown(KeyCode.D))
        {
            desiredLane++;
            //stops player going outside the lane on the right.
            if(desiredLane==3)
            {
                desiredLane = 2;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
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

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.fixedDeltaTime);
        if (transform.position == targetPosition)
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
        
            
    }

    private void FixedUpdate()
    {
        controller.Move(direction*Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }


}
