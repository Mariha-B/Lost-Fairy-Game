using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManager : MonoBehaviour
{
    public Transform playerPosition;
    private float tileDis = 500;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, playerPosition.position.z + tileDis);
        transform.position = newPosition;
    }
}
