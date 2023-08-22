using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SwipeManager swipeControls;
    public CharacterController controller;
    public GameObject player;
    private Vector3 moveDirection;

    public float forwardSpeed;
    public float maxSpeed;

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
        //Move the controller in 'moveDirection' every frame.
        controller.Move(moveDirection * Time.fixedDeltaTime);
        isGrounded = controller.isGrounded;

        if (!PlayerManager.gameStart)
            return;
        
        //Idle animation plays when isGame Started parameter is false (default at Start), and running animation plays when true.
        anim.SetBool("isGameStarted", true);
        //Sets the z value of the moveDirection Vector to the forwardSpeed float. Moves Player forward in the z direction at forwardSpeed, speeding up by 0.1 every frame.
        moveDirection.z = forwardSpeed;
        if (forwardSpeed < maxSpeed)
            {
                forwardSpeed += 0.1f * Time.deltaTime;
            }
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
            moveDirection.y += gravity * Time.deltaTime;

        }

        if(swipeControls.swipeDown)
        {
            StartCoroutine(Slide());
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

        // Check if the current position is not yet at the target position
        if (transform.position != targetPosition)
        {
            // Calculate the vector pointing from current position to target position
            Vector3 diff = targetPosition - transform.position;

            // Calculate a normalized movement direction vector
            Vector3 moveDir = diff.normalized * 50 * Time.deltaTime;

            // Check if the squared magnitude of the movement direction is smaller than
            // the magnitude of the difference vector (distance to target)
            if (moveDir.sqrMagnitude < diff.magnitude)
            {
                // Move the object smoothly using the controller's Move method
                controller.Move(moveDir);
            }
            else
            {
                // If the object is very close to the target, move it directly to the target
                controller.Move(diff);
            }
            controller.Move(moveDirection * Time.deltaTime);
        }

    }

    private void FixedUpdate()
    {
        /*if (!PlayerManager.gameStart)
            return;
        controller.Move(moveDirection * Time.fixedDeltaTime);*/
    }

    private void Jump()
    {//Player moves in the y direction by jumpForce
       moveDirection.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {   //if the player hits somthing tagged 'obstacle' the game is over and the Game Over screen displays
        if (hit.transform.tag == "Obstacle")
        {

            maxSpeed = 0;
            forwardSpeed = 0;
            anim.SetBool("isDead", true);
            controller.enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("Death");
            StartCoroutine(Delay());
            

        }



    }
    private IEnumerator Delay()
    {  
        yield return new WaitForSeconds(1.6f); 
        PlayerManager.gameOver = true;

    }
    private IEnumerator Slide()
    {
        anim.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.29f, 0);
        controller.height = 1;
        yield return new WaitForSeconds(1f);
        controller.center = new Vector3(0, 1.15f, 0);
        controller.height = 4.3f;
        anim.SetBool("isSliding", false);
    }

}
