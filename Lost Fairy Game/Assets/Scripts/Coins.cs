using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coinValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(40 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider collision)
    {   //If Obj with player tag collides with coin, increases Score
        if (collision.CompareTag("Player"))
        {
            
            collision.GetComponent<ScoreManager>().AddScore(coinValue);

           
            gameObject.SetActive(false);
        }
    }

}
