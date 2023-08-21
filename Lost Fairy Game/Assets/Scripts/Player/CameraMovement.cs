using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {//Offset is the difference between the camera position and Player position
        offset = transform.position - target.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {// Create a new position for camera that changes z position, it will add the offset to the player position
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        //Transforms camera position to new position using Vector Lerp for smoothness
        transform.position = Vector3.Lerp(transform.position, newPosition, 10 * Time.deltaTime);
    }
}
