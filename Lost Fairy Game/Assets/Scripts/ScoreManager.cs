using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreNum;
    public int startingScore;
    public int currentScore { get; private set; }

    private void Awake()
    {
        currentScore = startingScore;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Score takes number and converts to string
        scoreNum.text = currentScore.ToString();
    }

    public void AddScore(int _value)
    {
        currentScore += _value;

    }


}
