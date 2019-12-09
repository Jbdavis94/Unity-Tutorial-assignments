using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerMode : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;
    private bool gameOver;


    [SerializeField] Text countdownText;

    void Start()
    {
        currentTime = startingTime;
        gameOver = false;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString ("0");
        if (currentTime <= 10)
        {
            countdownText.color = Color.red;
            currentTime = 0;
            gameOver = true;
        }
    }
}
