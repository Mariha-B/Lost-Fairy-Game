using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float moveSpeed = 5f;
    private Vector3 move;

    private float laneSpace = 40f;
    private int desiredLane = 1; //0:left 1:Middle 2:Right

    public SwipeManager swipeControls;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector3(0, 0, 1);
        controller.Move(move * moveSpeed * Time.deltaTime);
        Vector3 newPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 2)
        {
            //newPosition

        }

        if (desiredLane == 0)
        {
            controller.Move(Vector3.left * laneSpace * Time.deltaTime);

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

        

        if (desiredLane == 2)
        {
            controller.Move(Vector3.right * laneSpace * Time.deltaTime);
        }



    }
}
